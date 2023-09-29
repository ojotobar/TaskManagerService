using Entities.Models;
using FluentValidation;

namespace Services.Helpers
{
    public class TaskValidator : AbstractValidator<TaskEntity>
    {
        public TaskValidator()
        {
            RuleFor(task => task.Name).NotEmpty();
            RuleFor(task => task.Description).NotEmpty();
            RuleFor(task => task.StartDate).GreaterThan(DateTime.MinValue);
            RuleFor(task => task.ElapsedTime).GreaterThanOrEqualTo(0);
            RuleFor(task => task.AllottedTime).GreaterThanOrEqualTo(0);
        }
    }
}
