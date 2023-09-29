namespace Entities.Responses
{
    public sealed class TaskNotFoundResponse : ApiNotFoundResponse
    {
        public TaskNotFoundResponse(string message) : base(message)
        {}
    }
}
