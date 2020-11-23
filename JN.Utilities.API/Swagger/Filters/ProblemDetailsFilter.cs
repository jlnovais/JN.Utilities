using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JN.Utilities.API.Swagger.Filters
{
    public class ProblemDetailsFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var operationResponse in operation.Responses)
            {
                if (operationResponse.Key.StartsWith("2"))
                {
                    // for 2xx codes, remove problems media type from returned type
                    operationResponse.Value.Content =
                        operationResponse.Value.Content.Where(pair =>
                                !(pair.Key == "application/problem+json" || pair.Key == "application/problem+xml"))
                            .ToDictionary(r => r.Key, r => r.Value);
                }

                if (operationResponse.Key.StartsWith("4") || operationResponse.Key.StartsWith("5"))
                {
                    // for 4xx and 5xx codes, keep only problems media type in returned type
                    operationResponse.Value.Content =
                        operationResponse.Value.Content.Where(pair =>
                                (pair.Key == "application/problem+json" || pair.Key == "application/problem+xml"))
                            .ToDictionary(r => r.Key, r => r.Value);

                }
            }
        }
    }
}