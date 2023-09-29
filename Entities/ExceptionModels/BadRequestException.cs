namespace Entities.ExceptionModels
{
    public class BadRequestException : Exception
    {
        protected BadRequestException(string message) : base(message)
        { }
    }
}
