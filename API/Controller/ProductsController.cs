using Microsoft.AspNetCore.Mvc;
using MediatR;
using API.Controllers.BaseApi;
using SharedKernel.Interfaces;
using Application.Commands.ProductModule.CreateProduct;
using Application.Commands.ProductModule.Auditable;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseDeleteAndRecoveryController<
        ProductSoftDeleteCommand,
        ProductHardDeleteCommand,
        ProductRecoveryCommand>
{
    private readonly ISender _mediator;
    public ProductsController(ISender mediator, ICurrentUser currentUser)
        : base(mediator, currentUser)
    {
        _mediator = mediator;
    }

    protected override Func<List<long>, long, ProductSoftDeleteCommand> CreateSoftDeleteCommand => (ids, userId) => new ProductSoftDeleteCommand(ids, userId);

    protected override Func<List<long>, long, ProductHardDeleteCommand> CreateHardDeleteCommand => (ids, userId) => new ProductHardDeleteCommand(ids, userId);

    protected override Func<long, long, ProductRecoveryCommand> CreateRecoveryCommand => CreateRecovery;

    private ProductRecoveryCommand CreateRecovery(long id, long userId)
    {
        return new ProductRecoveryCommand(id, userId);
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
    public IActionResult GetById(int id) => Ok();
    
}
