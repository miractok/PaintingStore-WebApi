using FluentValidation;

namespace WebApi.Application.PaintingOperations.Queries.GetPaintingDetails
{
    public class GetPaintingDetailsQueryValidator : AbstractValidator<GetPaintingDetailsQuery>
    {
        public GetPaintingDetailsQueryValidator()
        {
            RuleFor(query => query.PaintingId).GreaterThan(0);
        }
    }
}