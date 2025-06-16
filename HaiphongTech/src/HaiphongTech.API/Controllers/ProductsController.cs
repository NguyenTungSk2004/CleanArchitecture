using Microsoft.AspNetCore.Mvc;
using HaiphongTech.Application.Services.Products.Commands.CreateProduct;
using MediatR;
using HaiphongTech.Domain.Entities.Products.ProductAggregate;
using HaiphongTech.API.Controllers.BaseApi;
using HaiphongTech.SharedKernel.Interfaces;
using HaiphongTech.API.Requests;

namespace HaiphongTech.WebAPI.Controllers;

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
