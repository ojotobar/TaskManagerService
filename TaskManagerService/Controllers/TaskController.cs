using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using TaskManagerService.Controllers.Extensions;

namespace TaskManagerService.Controllers
{
    [Route("api/v1/task")]
    [ApiController]
    public class TaskController : ApiControllerBase
    {
        private readonly IServiceManager _service;
        public TaskController(IServiceManager service)
            => _service = service;

        /// <summary>
        /// Endpoint to get a list of tasks
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Requst</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromQuery] Query query)
        {
            return Ok(await _service.Task.GetAsync(query));
        }

        /// <summary>
        /// Endpoint to get task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Requst</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(string id)
        {
            var baseResult = await _service.Task.GetAsync(id);
            if(!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<TaskInfoDto>());
        }

        /// <summary>
        /// Endpoint to create a task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Requst</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post(TaskEntity request)
        {
            var baseResult = await _service.Task.CreateAsync(request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }

        /// <summary>
        /// Endpoint to delete a task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Requst</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(string id)
        {
            var baseResult = await _service.Task.DeleteAsync(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }
    }
}
