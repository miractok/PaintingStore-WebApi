using FluentValidation;

namespace WebApi.Application.OrderOperations.Queries.GetOrderDetails
{
    public class GetOrderDetailsQueryValidator : AbstractValidator<GetOrderDetailsQuery>
    {
        public GetOrderDetailsQueryValidator()
        {
            RuleFor(query => query.OrderId).GreaterThan(0);
        }
    }
}