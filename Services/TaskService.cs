using Entities.DTOs;
using Entities.Models;
using Microsoft.Extensions.Options;
using Service.Contracts;
using Services.Helpers;
using System.Net;
using FluentValidation.Results;
using System.Net.Http.Json;
using System.Text.Json;
using Entities.Responses;

namespace Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskServiceUrl _urlOptions;
        private static readonly HttpClient _client = new HttpClient();
        private readonly string _baseUrl;
        private readonly ILoggerManager _logger;

        public TaskService(IOptions<TaskServiceUrl> urlOptions, ILoggerManager logger)
        {
            _logger = logger;
            _urlOptions = urlOptions.Value;
            _baseUrl = _urlOptions.BaseUrl;
        }

        /// <summary>
        /// Creates new Task entity
        /// </summary>
        /// <param name="task"></param>
        /// <returns>True if everything goes well, error otherwise.</returns>
        public async Task<ApiBaseResponse> CreateAsync(TaskEntity task)
        {
            TaskValidator validator = new TaskValidator();
            ValidationResult results = validator.Validate(task);
            if (!results.IsValid)
            {
                return new TaskBadRequestResponse(results.Errors?.FirstOrDefault()?.ToString());
            }

            _client.SetHeaders(_baseUrl);
            var path = string.Join("", _urlOptions.Create);
            HttpResponseMessage response = await _client.PostAsJsonAsync(path, task);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = response.StatusCode;
                return statusCode == HttpStatusCode.BadRequest ?
                    new TaskBadRequestResponse($"{(int)statusCode}: {response.ReasonPhrase}.") :
                    new TaskNotFoundResponse($"{(int)statusCode}: {response.ReasonPhrase}.");
            }

            return new ApiOkResponse<bool>(response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Gets Task entity by id
        /// </summary>
        /// <param name="id">Id of the resource to get</param>
        /// <returns>A task object</returns>
        public async Task<ApiBaseResponse> GetAsync(string id)
        {
            _client.SetHeaders(_baseUrl);
            var path = string.Format(_urlOptions.GetById, id);
            using HttpResponseMessage response = await _client.GetAsync(path);
            var taskInfoDto = new TaskInfoDto();

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return new ApiOkResponse<TaskInfoDto?>(await JsonSerializer.DeserializeAsync<TaskInfoDto>(stream));
            }
            
            var statusCode = response.StatusCode;
            return statusCode == HttpStatusCode.BadRequest ?
                new TaskBadRequestResponse($"{(int)statusCode}: {response.ReasonPhrase}.") :
                new TaskNotFoundResponse($"{(int)statusCode}: {response.ReasonPhrase}.");
        }

        /// <summary>
        /// Gets all tasks
        /// </summary>
        /// <param name="query">Search property to filter tasks by texts, Page and Limit properties for paginations. 
        /// 1 and 10 by default for Page and Limit properies respectively</param>
        /// <returns>List of task objects</returns>
        public async Task<List<TaskInfoDto>?> GetAsync(Query query)
        {
            _client.SetHeaders(_baseUrl);
            var path = string.Format(_urlOptions.GetAll, query.Search, query.Page, query.Limit);
            using HttpResponseMessage response = await _client.GetAsync(path);
            var taskInfoDto = new List<TaskInfoDto>();

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                taskInfoDto = await JsonSerializer.DeserializeAsync<List<TaskInfoDto>>(stream);
            }

            return taskInfoDto;
        }

        /// <summary>
        /// Delete a task object by id
        /// </summary>
        /// <param name="id">The id of the resource to delete</param>
        /// <returns>True for successful deletion, error otherwise.</returns>
        public async Task<ApiBaseResponse> DeleteAsync(string id)
        {
            _client.SetHeaders(_baseUrl);
            var path = string.Format(_urlOptions.Delete, id);
            HttpResponseMessage response = await _client.DeleteAsync(path);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = response.StatusCode;
                return statusCode == HttpStatusCode.BadRequest ?
                    new TaskBadRequestResponse($"{(int)statusCode}: {response.ReasonPhrase}.") :
                    new TaskNotFoundResponse($"{(int)statusCode}: {response.ReasonPhrase}.");
            }

            return new ApiOkResponse<bool>(response.IsSuccessStatusCode);
        }
    }
}
