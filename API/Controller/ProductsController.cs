using Microsoft.AspNetCore.Mvc;
using MediatR;
using Domain.Entities.Products.ProductAggregate;
using API.Controllers.BaseApi;
using SharedKernel.Interfaces;
using API.Requests;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseDeleteAndRecoveryController<Product>
{
    protected override string SoftDeletePermission => "";
    private readonly ISender _mediator;

    public ProductsController(ISender mediator, ICurrentUser currentUser)
        : base(mediator, currentUser)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {

        var productId = await _mediator.Send(request.ToCommand());
        if (productId <= 0)
        {
            return BadRequest("Failed to create product.");
        }
        return Ok(productId);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id) => Ok();
    
}
