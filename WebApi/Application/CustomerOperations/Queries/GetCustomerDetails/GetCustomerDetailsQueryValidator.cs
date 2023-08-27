using FluentValidation;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomerDetails
{
    public class GetCustomerDetailQueryValidator : AbstractValidator<GetCustomerDetailsQuery>
    {
        public GetCustomerDetailQueryValidator()
        {
            RuleFor(query => query.CustomerId).GreaterThan(0);
        }
    }
}