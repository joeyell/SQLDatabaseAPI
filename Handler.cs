using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;

namespace SQLDatabaseAPI
{
    public class GetUserFunction
    {
        private readonly ILogger<GetUserFunction> _logger;

        public GetUserFunction(ILogger<GetUserFunction> logger)
        {
            _logger = logger;
        }

        [Function("GetUser")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{userId}")] HttpRequest req,
            int userId)
        {
            try
            {
                _logger.LogInformation($"Retrieving user with ID: {userId}");

                // Add handling to retrieve user from db

                EnterpriseComplement user = new EnterpriseComplement
                {
                    UserId = userId,
                    UserInfo = "Sample User Info",
                    DataEntered = DateTime.UtcNow
                };

                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving user: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
