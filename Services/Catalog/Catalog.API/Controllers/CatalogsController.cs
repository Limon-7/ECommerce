using Catalog.Application.Command;
using Catalog.Domain.Entities;
using Catalog.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CatalogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Product> Get(CreateProductCommand cmd)
    {
        return await _mediator.Send(cmd);
    }
}