using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.PaintingOperations.Commands.CreatePainting;
using WebApi.Application.PaintingOperations.Commands.DeletePainting;
using WebApi.Application.PaintingOperations.Commands.UpdatePainting;
using WebApi.Application.PaintingOperations.Queries.GetPaintingDetails;
using WebApi.Application.PaintingOperations.Queries.GetPaintings;
using WebApi.DBOperations;

namespace WebApi.Controller;

//[Authorize]
[ApiController]
[Route("[Controller]s")]
public class PaintingController : ControllerBase
{
    private readonly IPaintingStoreDbContext _context;
    private readonly IMapper _mapper;

    public PaintingController(IMapper mapper, IPaintingStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetMovies()
    {
        GetPaintingsQuery query = new GetPaintingsQuery(_context,_mapper);
        
        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [return: System.Diagnostics.CodeAnalysis.MaybeNull]
    public IActionResult GetMovieById(int id)
    {
        PaintingViewIdModel result;

        GetPaintingDetailsQuery query = new GetPaintingDetailsQuery(_context, _mapper);
        query.PaintingId = id;

        GetPaintingDetailsQueryValidator validator = new GetPaintingDetailsQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddPainting([FromBody] CreatePaintingModel newPainting)
    {
        CreatePaintingCommand command = new CreatePaintingCommand(_mapper,_context);
        command.Model = newPainting;

        CreatePaintingCommandValidator validator = new CreatePaintingCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeletePainting(int id)
    {
        DeletePaintingCommand command = new DeletePaintingCommand(_context);
        command.PaintingId = id;

        DeletePaintingCommandValidator validator = new DeletePaintingCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdatePainting(int id,[FromBody] UpdatePaintingModel updatePainting)
    {
        UpdatePaintingCommand command = new UpdatePaintingCommand(_context);
        command.PaintingId = id;
        command.Model = updatePainting;
        
        UpdatePaintingCommandValidator validator = new UpdatePaintingCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        
        return Ok();
    }
}