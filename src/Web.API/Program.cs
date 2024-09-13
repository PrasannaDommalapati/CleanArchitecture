using System.Net;
using System.Net.Mail;
using Application;
using Domain;
using Domain.Dto;
using Infrastructure;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NSwag;
using NSwag.Generation.Processors.Security;
using Serilog;
using Web.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

builder.Services.AddAuthorization();

// For IdentityUser
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// For Autherntication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});

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

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// For EF
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.Configure<IdentityConfiguration>(builder.Configuration.GetSection("IdentityConfiguration"));
builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Infrastructure")));

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument(configure =>
{
    configure.Title = "Your API";
    configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Type into the textbox: 'Bearer {your JWT token}'."
    });

    configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseMiddleware<RequestContextLoggingMiddleware>();

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
