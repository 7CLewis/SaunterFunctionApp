using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Saunter;

[assembly: FunctionsStartup(typeof(SaunterFunctionApp.Startup))]
namespace SaunterFunctionApp;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddAsyncApiSchemaGeneration();

        builder.Services.AddOptions<AsyncApiOptions>()
            .Configure(c =>
            {
                c.AssemblyMarkerTypes = new[] { typeof(FunctionTest) };
            }
         );
    }
}
