using Microsoft.AspNetCore.Mvc;
using HaiphongTech.Application.Features.Products.Commands.CreateProduct;
using MediatR;

namespace HaiphongTech.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ISender _mediator;

    public ProductsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        var productId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = productId }, null); // bạn có thể cài GetById sau
    }

    // Optional placeholder nếu bạn chưa có GetById
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id) => Ok();
    
}
