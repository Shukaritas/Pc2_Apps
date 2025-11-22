// csharp
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using pc2NRCuCodigo.API.Premium.Domain.Services;
using pc2NRCuCodigo.API.Premium.Domain.Model.Queries;
using pc2NRCuCodigo.API.Premium.Interfaces.REST.Resources;
using pc2NRCuCodigo.API.Premium.Interfaces.REST.Transform;

namespace pc2NRCuCodigo.API.Premium.Interfaces.REST;

[ApiController]
[Route("api/v1/")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Operaciones disponibles para Premium Orders")]
public class PremiumsController
    (IPremiumOrderQueryService queryService,
     IPremiumOrderCommandService commandService)
    : ControllerBase
{
    [HttpGet("premium-orders/{id:int}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [SwaggerOperation(
        Summary = "Obtener Premium Order por Id",
        OperationId = "GetPremiumOrderById")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPremiumOrderById(int id)
    {
        var getPremiumOrderByIdQuery = new GetPremiumOrdersByIdQuery(id);
        var premiumOrder = await queryService.Handle(getPremiumOrderByIdQuery);
        
        if (premiumOrder is null) 
            return NotFound();
        
        var resource = PremiumOrderResourceFromEntityAssembler.ToResourceFromEntity(premiumOrder);
        return Ok(resource);
    }
    
    [HttpPost("premium-orders")]
    [SwaggerOperation(
        Summary = "Crear Premium Order",
        OperationId = "CreatePremiumOrder")]
    [SwaggerResponse(StatusCodes.Status201Created)]
    [SwaggerResponse(StatusCodes.Status409Conflict, Description = "Ya existe con los mismos datos")]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePremiumOrder([FromBody] CreatePremiumOrderResource resource)
    {
        var allOrders = await queryService.Handle(new GetAllPremiumOrdersQuery());
    
        var exists = allOrders?.Any(p => 
            p.CustomerEmail == resource.customerEmail && 
            p.ProductId == resource.productId && 
            p.ShippingType == resource.shippingType) ?? false;

        if (exists)
            return Conflict("Ya existe un Premium Order con el mismo email, producto y tipo de envío");

        var command = CreatePremiumOrderCommandFromResourceAssembler.ToCommandFromResource(resource);
        var created = await commandService.Handle(command);

        if (created is null)
            return BadRequest("No se pudo crear el Premium Order");

        var responseResource = PremiumOrderResourceFromEntityAssembler.ToResourceFromEntity(created);
        return CreatedAtAction(nameof(GetPremiumOrderById), new {id = created.Id}, responseResource);
    }
}
