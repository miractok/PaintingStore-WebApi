using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetails;
using WebApi.Application.TokenOperations.Commands.CreateToken;
using WebApi.Application.TokenOperations.Commands.RefreshToken;
using WebApi.Application.TokenOperations.Models;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]s")]
public class CustomerController : ControllerBase
{
    private readonly IPaintingStoreDbContext _context;
    private readonly IMapper _mapper;
    readonly IConfiguration _configuration;

    public CustomerController(IMapper mapper, IPaintingStoreDbContext context, IConfiguration configuration)
    {
        _mapper = mapper;
        _context = context;
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult CreateCustomer([FromBody] CreateCustomerModel newCustomer)
    {
        CreateCustomerCommand command = new CreateCustomerCommand(_mapper,_context);
        command.Model = newCustomer;

        CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
    {
        CreateTokenCommand command = new CreateTokenCommand(_mapper,_context,_configuration);
        command.Model = login;
        
        var token = command.Handle();
        return token;
    }

    [HttpDelete("id")]
    public IActionResult DeleteUser(int id)
    {
        DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
        command.CustomerId = id;

        DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();      
    }

    [HttpGet("refreshToken")]
    public ActionResult<Token> RefreshToken([FromQuery] string token)
    {
        RefreshTokenCommand command = new RefreshTokenCommand(_context,_configuration);
        command._refreshToken = token;
        
        var resultToken = command.Handle();
        return resultToken;
    }

    [HttpGet("{id}")]
    [return: System.Diagnostics.CodeAnalysis.MaybeNull]
    public IActionResult GetCustomById(int id)
    {
        CustomerViewIdModel result;

        GetCustomerDetailsQuery query = new GetCustomerDetailsQuery(_context, _mapper);
        query.CustomerId = id;

        GetCustomerDetailQueryValidator validator = new GetCustomerDetailQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }
}