using FluentValidation;

namespace WebApi.Application.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(command => command.Model.PaintingId).GreaterThan(0);
            RuleFor(command => command.Model.CustomerId).GreaterThan(0);
        }
    }
}