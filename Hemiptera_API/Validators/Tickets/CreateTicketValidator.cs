using FluentValidation;
using Hemiptera_API.Models.Enums;
using Hemiptera_API.Utilitys;
using Hemiptera_Contracts.Tickets.Requests;

namespace Hemiptera_API.Validators.Tickets;

public class CreateTicketValidator : AbstractValidator<CreateTicketRequest>
{
    public CreateTicketValidator()
    {
        RuleFor(x => x.title).NotEmpty();
        RuleFor(x => x.description).NotEmpty();
        RuleFor(x => x.summary).NotEmpty();
        RuleFor(x => x.reporterId).NotEmpty();
        RuleFor(x => x.assignedToId).NotEmpty();

        // Priority validation message
        var priorityMessage = EnumValidationMessageUtility.GetEnumValidationMessage<TicketPriority>();

        // Priority must be one of the enum values
        RuleFor(x => x.priority)
            .NotEmpty().WithMessage(priorityMessage).WithErrorCode("Priority")
            .GreaterThanOrEqualTo(0).WithMessage(priorityMessage).WithErrorCode("Priority")
            .LessThanOrEqualTo(sizeof(TicketPriority)).WithMessage(priorityMessage).WithErrorCode("Priority");

        // Status validation message
        var statusMessage = EnumValidationMessageUtility.GetEnumValidationMessage<TicketStatus>();

        // Status must be one of the enum values
        RuleFor(x => x.status)
            .NotEmpty().WithMessage(statusMessage).WithErrorCode("Status")
            .GreaterThanOrEqualTo(0).WithMessage(statusMessage).WithErrorCode("Status")
            .LessThanOrEqualTo(sizeof(TicketStatus)).WithMessage(statusMessage).WithErrorCode("Status");
    }
}