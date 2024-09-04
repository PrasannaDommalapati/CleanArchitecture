using Application;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSwag;
using NSwag.Generation.Processors.Security;
using Serilog;
using Web.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

// For IdentityUser
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

// For Autherntication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// For EF
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Infrastructure"));
});

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

app.MapControllers();

app.Run();
