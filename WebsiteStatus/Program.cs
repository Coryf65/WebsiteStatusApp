using WebsiteStatus;
using Serilog;
using Serilog.Events;


// Setup Serilog, simple way
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.File(@"D:\_TEMP\WebsiteStatusApp\serilog.txt")
    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog() // Using Serilog
    .Build();

try
{
    Log.Information("Starting up the Service");
    await host.RunAsync();
}
catch (Exception error)
{
    Log.Fatal(error, "There was an error starting the service");
} finally
{
    // end either way,
    // if any messages are in the buffer we close out the app
    Log.Information("Service Ended");
    Log.CloseAndFlush();
}


