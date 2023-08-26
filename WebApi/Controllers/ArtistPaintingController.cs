using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.ArtistPaintingOperations.Commands.CreateArtistPainting;
using WebApi.Application.ArtistPaintingOperations.Commands.DeleteArtistPainting;
using WebApi.Application.ArtistPaintingOperations.Commands.UpdateArtistPainting;
using WebApi.Application.ArtistPaintingOperations.Queries.GetArtistPaintingDetails;
using WebApi.Application.ArtistPaintingOperations.Queries.GetArtistPaintings;
using WebApi.DBOperations;

namespace WebApi.Controllers;
//[Authorize]
[ApiController]
[Route("[Controller]s")]
public class ArtistPaintingController : ControllerBase
{
    private readonly IPaintingStoreDbContext _context;
    private readonly IMapper _mapper;

    public ArtistPaintingController(IMapper mapper, IPaintingStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetArtistPaintings()
    {
        GetArtistPaintingQuery query = new GetArtistPaintingQuery(_mapper,_context);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [return: System.Diagnostics.CodeAnalysis.MaybeNull]
    public IActionResult GetArtistPaintingById(int id)
    {
        ArtistPaintingViewIdModel result;

        GetArtistPaintingDetailsQuery query = new GetArtistPaintingDetailsQuery(_context, _mapper);
        query.ArtistPaintingId = id;

        GetArtistPaintingDetailsQueryValidator validator = new GetArtistPaintingDetailsQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddArtistPainting([FromBody] CreateArtistPaintingViewModel newArtistPainting)
    {
        CreateArtistPaintingCommand command = new CreateArtistPaintingCommand(_mapper,_context);
        command.Model = newArtistPainting;

        CreateArtistPaintingCommandValidator validator = new CreateArtistPaintingCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteArtistPainting(int id)
    {
        DeleteArtistPaintingCommand command = new DeleteArtistPaintingCommand(_context);
        command.DataId = id;

        DeleteArtistPaintingCommandValidator validator = new DeleteArtistPaintingCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateArtistPainting(int id,[FromBody] UpdateArtistPaintingModel updateArtistPainting)
    {
        UpdateArtistPaintingCommand command = new UpdateArtistPaintingCommand(_context);
        command.DataId = id;
        command.Model = updateArtistPainting;
        
        UpdateArtistPaintingCommandValidator validator = new UpdateArtistPaintingCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        
        return Ok();
    }
}