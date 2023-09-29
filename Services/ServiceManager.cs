using Entities.Models;
using Microsoft.Extensions.Options;
using Service.Contracts;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITaskService> _taskService;

        public ServiceManager(ILoggerManager logger, IOptions<TaskServiceUrl> options)
        {
            _taskService = new Lazy<ITaskService>(() => new
                TaskService(options, logger));
        }

        public ITaskService Task => _taskService.Value;
    }
}
