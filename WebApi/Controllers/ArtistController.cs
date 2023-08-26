using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using Microsoft.AspNetCore.Authorization;
using WebApi.Application.ArtistOperations.Queries.GetArtists;
using WebApi.Application.ArtistOperations.Queries.GetArtistDetails;
using WebApi.Application.ArtistOperations.Commands.CreateArtist;
using WebApi.Application.ArtistOperations.Commands.DeleteArtist;
using WebApi.Application.ArtistOperations.Commands.UpdateArtist;

namespace WebApi.Controllers;

//[Authorize]
[ApiController]
[Route("[Controller]s")]
public class ArtistController : ControllerBase
{
    private readonly IPaintingStoreDbContext _context;
    private readonly IMapper _mapper;

    public ArtistController(IMapper mapper, IPaintingStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetArtists()
    {
        GetArtistsQuery query = new GetArtistsQuery(_mapper,_context);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [return: System.Diagnostics.CodeAnalysis.MaybeNull]
    public IActionResult GetArtistById(int id)
    {
        ArtistViewIdModel result;

        GetArtistDetailsQuery query = new GetArtistDetailsQuery(_context, _mapper);
        query.ArtistId = id;

        GetArtistDetailsQueryValidator validator = new GetArtistDetailsQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddArtist([FromBody] CreateArtistViewModel newArtist)
    {
        CreateArtistCommand command = new CreateArtistCommand(_mapper,_context);
        command.Model = newArtist;

        CreateArtistCommandValidator validator = new CreateArtistCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteArtist(int id)
    {
        DeleteArtistCommand command = new DeleteArtistCommand(_context);
        command.ArtistId = id;

        DeleteArtistCommandValidator validator = new DeleteArtistCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateArtist(int id,[FromBody] UpdateArtistModel updateArtist)
    {
        UpdateArtistCommand command = new UpdateArtistCommand(_context);
        command.ArtistId = id;
        command.Model = updateArtist;
        
        UpdateArtistCommandValidator validator = new UpdateArtistCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        
        return Ok();
    }
}
