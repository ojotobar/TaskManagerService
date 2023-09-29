using Entities.DTOs;
using Entities.Models;
using Entities.Responses;
using System.Net;

namespace Service.Contracts
{
    public interface ITaskService
    {
        Task<ApiBaseResponse> CreateAsync(TaskEntity task);
        Task<ApiBaseResponse> DeleteAsync(string id);
        Task<ApiBaseResponse> GetAsync(string id);
        Task<List<TaskInfoDto>?> GetAsync(Query query);
    }
}
