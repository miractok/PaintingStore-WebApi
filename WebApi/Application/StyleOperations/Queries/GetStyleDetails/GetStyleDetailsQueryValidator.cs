using FluentValidation;

namespace WebApi.Application.StyleOperations.Queries.GetStyleDetails
{
    public class GetStyleDetailsQueryValidator : AbstractValidator<GetStyleDetailsQuery>
    {
        public GetStyleDetailsQueryValidator()
        {
            RuleFor(query => query.StyleId).GreaterThan(0);
        }
    }
}