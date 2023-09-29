using Entities.Responses;

namespace TaskManagerService.Controllers.Extensions
{
    public static class ApiBaseResponseExtensions
    {
        /// <summary>
        /// Generic method to return result from the service response
        /// </summary>
        /// <typeparam name="TResultType">Generic result type</typeparam>
        /// <param name="apiBaseResponse">Base response</param>
        /// <returns>Result type</returns>
        public static TResultType GetResult<TResultType>(this ApiBaseResponse apiBaseResponse) =>
            ((ApiOkResponse<TResultType>)apiBaseResponse).Result;
    }
}
