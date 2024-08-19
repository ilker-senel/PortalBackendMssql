using Business;
using Infrastructure;
using NLog.Web;
using Web.Middlewares;
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", cBuilder =>
    {
        cBuilder.AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials();
    }));

    NLog.LogManager.Setup().LoadConfigurationFromAppSettings();
    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddDataLayerExtensions(builder.Configuration);
    builder.Services.AddBusinessLayerExtensions(builder.Configuration);
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();



    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseMiddleware<GlobalErrorHandlingMiddleware>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception)
{
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}

