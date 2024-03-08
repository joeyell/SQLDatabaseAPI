using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SQLDatabaseAPI
{
    public class Sum
    {
        private readonly ILogger<Sum> _logger;

        public Sum(ILogger<Sum> logger)
        {
            _logger = logger;
        }

        [Function("Sum")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            if (!req.Query.TryGetValue("x", out var xValue) || !int.TryParse(xValue, out int x))
            {
                return new BadRequestObjectResult("Query parameter 'x' is missing or invalid.");
            }

            if (!req.Query.TryGetValue("y", out var yValue) || !int.TryParse(yValue, out int y))
            {
                return new BadRequestObjectResult("Query parameter 'y' is missing or invalid.");
            }

            int result = x + y;

            return new OkObjectResult(result);
        }
    }
}