using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;

namespace WebApi.Controllers;

//[Authorize]
[ApiController]
[Route("[Controller]s")]
public class GenreController : ControllerBase
{
    private readonly IPaintingStoreDbContext _context;
    private readonly IMapper _mapper;

    public GenreController(IMapper mapper, IPaintingStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetGenres()
    {
        GetGenresQuery query = new GetGenresQuery(_mapper,_context);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [return: System.Diagnostics.CodeAnalysis.MaybeNull]
    public IActionResult GetGenreById(int id)
    {
        GenresViewIdModel result;

        GetGenreDetailsQuery query = new GetGenreDetailsQuery(_context, _mapper);
        query.GenreId = id;

        GetGenreDetailsQueryValidator validator = new GetGenreDetailsQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddGenre([FromBody] CreateGenreViewModel newGenre)
    {
        CreateGenreCommand command = new CreateGenreCommand(_mapper,_context);
        command.Model = newGenre;

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteGenre(int id)
    {
        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId = id;

        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateGenre(int id,[FromBody] UpdateGenreModel updateGenre)
    {
        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        command.GenreId = id;
        command.Model = updateGenre;
        
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        
        return Ok();
    }
}
