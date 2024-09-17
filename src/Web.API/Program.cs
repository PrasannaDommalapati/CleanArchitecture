using System.Net;
using System.Net.Mail;
using Application;
using Domain;
using Domain.Dto;
using Infrastructure;
using Infrastructure.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

// For IdentityUser
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddTransient<SmtpClient>(provider =>
{
    return new SmtpClient("smtp.example.com")
    {
        Port = 587,
        Credentials = new NetworkCredential("your-username", "your-password"),
        EnableSsl = true
    };
});

// For EF
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("The 'DefaultConnection' is missing from the configuration.");
}
var identityConfigSection = builder.Configuration.GetSection(nameof(IdentityConfiguration));

if (!identityConfigSection.Exists())
{
    throw new InvalidOperationException($"The '{nameof(IdentityConfiguration)}' is missing from the configuration.");
}

builder.Services.Configure<IdentityConfiguration>(identityConfigSection);
builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Infrastructure")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
     {
         Name = "Authorization",
         Description = "Enter the Bearer Authorization string as following: `Bearer generated AccessToken`",
         In = ParameterLocation.Header,
         Type = SecuritySchemeType.ApiKey,
         Scheme = "Bearer"
     });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

// DB Migration when there is any pending migration
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var roleSeeder = scope.ServiceProvider.GetRequiredService<RoleSeedService>();
    await roleSeeder.SeedRolesAsync();

    await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var pendingTransactions = await dbContext.Database.GetPendingMigrationsAsync();

    if (pendingTransactions.Any())
    {
        await dbContext.Database.MigrateAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
//app.UseMiddleware<RequestContextLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapIdentityApi<IdentityUser>();

app.MapControllers();

await SeedRolesAndAdminUsersAsync(app.Services);

app.Run();

static async Task SeedRolesAndAdminUsersAsync(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Manager", "Member" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var identityConfig = scope.ServiceProvider.GetRequiredService<IOptions<IdentityConfiguration>>().Value;
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    foreach (var user in identityConfig.UserAccounts)
    {
        var email = user.Email;
        if (await userManager.FindByEmailAsync(email) is null)
        {
            var identityUser = new IdentityUser
            {
                Email = email,
                UserName = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(identityUser, user.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(identityUser, "Admin");
            }
        }
    }
}
