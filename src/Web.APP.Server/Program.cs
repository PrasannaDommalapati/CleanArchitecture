using Microsoft.OpenApi.Models;
using Serilog;
using Web.API.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
// Add services to the container.
var apiSectionConfig = builder.Configuration.GetSection("API:BaseUrl");
AddHttpClients(builder.Services, apiSectionConfig);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
 {
     options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
     {
         Name = "Authorization",
         Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
         In = ParameterLocation.Header,
         Type = SecuritySchemeType.ApiKey,
         Scheme = "Bearer"
     });
     options.CustomSchemaIds(type => type.ToString());
 });

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

void AddHttpClients(IServiceCollection services, IConfigurationSection configurationSection)
{
    var apiBaseUrl = new Uri(configurationSection.Value);
    services.AddHttpClient("APIClient", ConfigureHttpClient);

    AddWithAPIHttpClient<IClient, Client>();

    void AddWithAPIHttpClient<TClient, TImplementation>() where TClient : class where TImplementation : class, TClient
    {
        services.AddHttpClient<TClient, TImplementation>(ConfigureHttpClient);
    }

    void ConfigureHttpClient(IServiceProvider serviceProvider, HttpClient httpClient) => httpClient.BaseAddress = apiBaseUrl;
}
