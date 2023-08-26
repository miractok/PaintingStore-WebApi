using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.StyleOperations.Commands.CreateStyle;
using WebApi.Application.StyleOperations.Commands.DeleteStyle;
using WebApi.Application.StyleOperations.Commands.UpdateStyle;
using WebApi.Application.StyleOperations.Queries.GetStyleDetails;
using WebApi.Application.StyleOperations.Queries.GetStyles;
using WebApi.DBOperations;

namespace WebApi.Controllers;

//[Authorize]
[ApiController]
[Route("[Controller]s")]
public class StyleController : ControllerBase
{
    private readonly IPaintingStoreDbContext _context;
    private readonly IMapper _mapper;

    public StyleController(IMapper mapper, IPaintingStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetStyles()
    {
        GetStylesQuery query = new GetStylesQuery(_mapper,_context);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [return: System.Diagnostics.CodeAnalysis.MaybeNull]
    public IActionResult GetStyleById(int id)
    {
        StyleViewIdModel result;

        GetStyleDetailsQuery query = new GetStyleDetailsQuery(_context, _mapper);
        query.StyleId = id;

        GetStyleDetailsQueryValidator validator = new GetStyleDetailsQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddStyle([FromBody] CreateStyleViewModel newStyle)
    {
        CreateStyleCommand command = new CreateStyleCommand(_mapper,_context);
        command.Model = newStyle;

        CreateStyleCommandValidator validator = new CreateStyleCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteStyle(int id)
    {
        DeleteStyleCommand command = new DeleteStyleCommand(_context);
        command.StyleId = id;

        DeleteStyleCommandValidator validator = new DeleteStyleCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateStyle(int id,[FromBody] UpdateStyleModel updateStyle)
    {
        UpdateStyleCommand command = new UpdateStyleCommand(_context);
        command.StyleId = id;
        command.Model = updateStyle;
        
        UpdateStyleCommandValidator validator = new UpdateStyleCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        
        return Ok();
    }
}
