using NLog;
using NLog.Web;
using Serilog;
using Web.API.Client;

// Early init of NLog to allow startup and exception logging, before host is built
//var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
//logger.Debug("init main");
//try
//{
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
     options.CustomSchemaIds(type => type.ToString());
 });

builder.Logging.ClearProviders().AddConsole();
builder.Host.UseNLog();
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
//}
//catch (Exception exception)
//{
//    logger.Error(exception, "Stopped program because of exception");
//    throw;
//}
//finally
//{
//     // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
//    LogManager.Shutdown();
//}
