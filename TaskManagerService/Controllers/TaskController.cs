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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Query query)
        {
            return Ok(await _service.Task.GetAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var baseResult = await _service.Task.GetAsync(id);
            if(!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<TaskInfoDto>());
        }

        [HttpPost]
        public async Task<IActionResult> Post(TaskEntity request)
        {
            var baseResult = await _service.Task.CreateAsync(request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var baseResult = await _service.Task.DeleteAsync(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }
    }
}
