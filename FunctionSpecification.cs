using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Options;
using Saunter;
using Saunter.Serialization;
using System.Threading.Tasks;

namespace SaunterFunctionApp;
public class FunctionSpecification
{
    private readonly IAsyncApiDocumentProvider provider;
    private readonly IAsyncApiDocumentSerializer serializer;
    private readonly AsyncApiOptions options;

    public FunctionSpecification(
        IAsyncApiDocumentProvider provider, 
        IAsyncApiDocumentSerializer serializer, 
        IOptions<AsyncApiOptions> options
    )
    {
        this.provider = provider;
        this.serializer = serializer;
        this.options = options.Value;
    }

    [FunctionName(nameof(AsyncApiSpec))]
    public async Task<IActionResult> AsyncApiSpec(
        [HttpTrigger(AuthorizationLevel.Anonymous, Route = "specifications")] HttpRequest req    
    )
    {
        var schema = provider.GetDocument(options, options.AsyncApi);

        return new OkObjectResult(serializer.Serialize(schema));
    }
}
