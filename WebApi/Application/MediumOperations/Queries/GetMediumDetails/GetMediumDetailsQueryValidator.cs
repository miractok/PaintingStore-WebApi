using FluentValidation;

namespace WebApi.Application.MediumOperations.Queries.GetMediumDetails
{
    public class GetMediumDetailsQueryValidator : AbstractValidator<GetMediumDetailsQuery>
    {
        public GetMediumDetailsQueryValidator()
        {
            RuleFor(query => query.MediumId).GreaterThan(0);
        }
    }
}