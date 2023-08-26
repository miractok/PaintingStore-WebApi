using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.MediumOperations.Commands.CreateMedium;
using WebApi.Application.MediumOperations.Commands.DeleteMedium;
using WebApi.Application.MediumOperations.Commands.UpdateMedium;
using WebApi.Application.MediumOperations.Queries.GetMediumDetails;
using WebApi.Application.MediumOperations.Queries.GetMediums;
using WebApi.DBOperations;

namespace WebApi.Controllers;

//[Authorize]
[ApiController]
[Route("[Controller]s")]
public class MediumController : ControllerBase
{
    private readonly IPaintingStoreDbContext _context;
    private readonly IMapper _mapper;

    public MediumController(IMapper mapper, IPaintingStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetMediums()
    {
        GetMediumsQuery query = new GetMediumsQuery(_mapper,_context);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [return: System.Diagnostics.CodeAnalysis.MaybeNull]
    public IActionResult GetMediumById(int id)
    {
        MediumViewIdModel result;

        GetMediumDetailsQuery query = new GetMediumDetailsQuery(_context, _mapper);
        query.MediumId = id;

        GetMediumDetailsQueryValidator validator = new GetMediumDetailsQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddMedium([FromBody] CreateMediumViewModel newMedium)
    {
        CreateMediumCommand command = new CreateMediumCommand(_mapper,_context);
        command.Model = newMedium;

        CreateMediumCommandValidator validator = new CreateMediumCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteMedium(int id)
    {
        DeleteMediumCommand command = new DeleteMediumCommand(_context);
        command.MediumId = id;

        DeleteMediumCommandValidator validator = new DeleteMediumCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateMedium(int id,[FromBody] UpdateMediumModel updateMedium)
    {
        UpdateMediumCommand command = new UpdateMediumCommand(_context);
        command.MediumId = id;
        command.Model = updateMedium;
        
        UpdateMediumCommandValidator validator = new UpdateMediumCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        
        return Ok();
    }
}
