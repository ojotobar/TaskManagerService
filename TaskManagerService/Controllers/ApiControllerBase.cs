using Entities.ExceptionModels;
using Entities.Responses;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerService.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// Method to process errors if the service returns
        /// non-success result.
        /// </summary>
        /// <param name="baseResponse">Base response</param>
        /// <returns>Exception details</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="ApiNotFoundResponse"></exception>
        /// <exception cref="ApiBadRequestResponse"></exception>
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ProcessError(ApiBaseResponse baseResponse)
        {
            return baseResponse switch
            {
                ApiNotFoundResponse => NotFound(new ExceptionDetails
                {
                    Message = ((ApiNotFoundResponse)baseResponse).Message,
                    StatusCode = StatusCodes.Status404NotFound
                }),
                ApiBadRequestResponse => BadRequest(new ExceptionDetails
                {
                    Message = ((ApiBadRequestResponse)baseResponse).Message,
                    StatusCode = StatusCodes.Status400BadRequest
                }),
                _ => throw new NotImplementedException()
            };
        }
    }
}
