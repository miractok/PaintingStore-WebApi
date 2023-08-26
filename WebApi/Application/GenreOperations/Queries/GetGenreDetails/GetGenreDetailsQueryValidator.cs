using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetails
{
    public class GetGenreDetailsQueryValidator : AbstractValidator<GetGenreDetailsQuery>
    {
        public GetGenreDetailsQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
    }
}