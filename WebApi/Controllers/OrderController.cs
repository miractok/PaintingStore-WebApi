using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.Application.OrderOperations.Commands.UpdateOrder;
using WebApi.Application.OrderOperations.Queries.GetOrder;
using WebApi.Application.OrderOperations.Queries.GetOrderDetails;
using WebApi.DBOperations;

namespace WebApi.Controllers;

//[Authorize]
[ApiController]
[Route("[Controller]s")]
public class OrderController : ControllerBase
{
    private readonly IPaintingStoreDbContext _context;
    private readonly IMapper _mapper;

    public OrderController(IMapper mapper, IPaintingStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        GetOrderQuery query = new GetOrderQuery(_mapper,_context);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [return: System.Diagnostics.CodeAnalysis.MaybeNull]
    public IActionResult GetOrderById(int id)
    {
        OrderViewIdModel result;

        GetOrderDetailsQuery query = new GetOrderDetailsQuery(_context, _mapper);
        query.OrderId = id;

        GetOrderDetailsQueryValidator validator = new GetOrderDetailsQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddOrder([FromBody] CreateOrderViewModel newOrder)
    {
        CreateOrderCommand command = new CreateOrderCommand(_mapper,_context);
        command.Model = newOrder;

        CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteOrder(int id)
    {
        DeleteOrderCommand command = new DeleteOrderCommand(_context);
        command.DataId = id;

        DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateOrder(int id,[FromBody] UpdateOrderModel updateOrder)
    {
        UpdateOrderCommand command = new UpdateOrderCommand(_context);
        command.DataId = id;
        command.Model = updateOrder;
        
        UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        
        return Ok();
    }
}