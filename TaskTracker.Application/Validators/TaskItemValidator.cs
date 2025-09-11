using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Validators
{
    public class TaskItemValidator : AbstractValidator<TaskItem>
    {
        public TaskItemValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x=>x.Title).NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");
            RuleFor(x=>x.Description).MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
            RuleFor(x=>x.CreatedAt).NotEmpty().WithMessage("CreatedAt is required.");
            RuleFor(x=>x.DueDate).GreaterThanOrEqualTo(x=>x.CreatedAt).When(x=>x.DueDate.HasValue)
                .WithMessage("DueDate must be later than CreatedAt.");
        }
    }
}
