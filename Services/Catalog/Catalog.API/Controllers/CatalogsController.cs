using System.Net;
using Catalog.Application.Brands.GetBrands;
using Catalog.Application.Common.Models;
using Catalog.Application.Products.Commands.CreateProduct;
using Catalog.Application.Products.Commands.DeleteProduct;
using Catalog.Application.Products.Commands.UpdateProduct;
using Catalog.Application.Products.Queries;
using Catalog.Application.Products.Queries.GetProductById;
using Catalog.Application.Products.Queries.GetProductByProductName;
using Catalog.Application.Products.Queries.GetProductsByBrandName;
using Catalog.Application.Products.Queries.GetProductsWithPagination;
using Catalog.Application.ProductTypes.GetProductTypes;
using Catalog.Domain.Specs;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

public class CatalogsController : ApiControllerBase
{
    #region Product

    [HttpGet]
    [ProducesResponseType(typeof(Response<PaginatedList<ProductResponse>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Response<PaginatedList<ProductResponse>>>> GetProductsWithPagination(
        [FromQuery] CatalogSpecParams catalogSpecParams)
    {
        return await Mediator.Send(new GetProductsWithPaginationQuery(catalogSpecParams));
    }

    [HttpGet]
    [Route("[action]/{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(Response<ProductResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<Response<ProductResponse>>> GetProductById(string id)
    {
        return await Mediator.Send(new GetProductByIdQuery(id));
    }

    [HttpGet]
    [Route("[action]/{productName}", Name = "GetProductsByProductName")]
    [ProducesResponseType(typeof(Response<IList<ProductResponse>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Response<IList<ProductResponse>>>> GetProductByProductName(string productName)
    {
        return await Mediator.Send(new GetProductByProductNameQuery(productName));
    }

    [HttpGet]
    [Route("[action]/{brandName}", Name = "GetProductsByBrandName")]
    [ProducesResponseType(typeof(Response<IList<ProductResponse>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Response<IList<ProductResponse>>>> GetProductsByBrandName(string brandName)
    {
        return await Mediator.Send(new GetProductsByBrandNameQuery(brandName));
    }

    [HttpPost]
    [Route("CreateProduct")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Response<bool>>> CreateProduct([FromBody] CreateProductCommand productCommand)
    {
        return await Mediator.Send(productCommand);
    }

    [HttpPut]
    [Route("UpdateProduct")]
    [ProducesResponseType(typeof(Response<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Response<ProductResponse>>> UpdateProduct(
        [FromBody] UpdateProductCommand productCommand)
    {
        var response = await Mediator.Send(productCommand);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        await Mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }

    #endregion Product

    #region Brands

    [HttpGet]
    [Route("GetBrands")]
    [ProducesResponseType(typeof(Response<IList<BrandResponse>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Response<IList<BrandResponse>>>> GetAllBrands()
    {
        return Ok(await Mediator.Send(new GetBrandsQuery()));
    }

    #endregion Brands

    #region Types

    [HttpGet]
    [Route("GetProductTypes")]
    [ProducesResponseType(typeof(Response<IList<ProductTypeResponse>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Response<IList<ProductTypeResponse>>>> GetProductTypes()
    {
        return Ok(await Mediator.Send(new GetProductTypesQuery()));
    }

    #endregion Types
}