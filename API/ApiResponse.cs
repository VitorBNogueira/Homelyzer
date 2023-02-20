using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    public static class ApiResponse
    {
        public static IActionResult Response(IResponse response)
        {
            switch (response)
            {
                case ISuccess _:
                    return new OkObjectResult(response);

                case IClientFailure failure:
                    return new BadRequestObjectResult(failure.ErrorCode);

                case IServerFailure failure:
                    // Implement later
                    //_logger.LogError(ex, "Error retrieving product with id {id}", id);

                    return new ObjectResult("An internal error has occured") { StatusCode = StatusCodes.Status500InternalServerError };

                default:
                    throw new ArgumentException("Unknown response type.", nameof(response));
            }
        }
    }
}
