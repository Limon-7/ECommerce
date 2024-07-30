using Catalog.Application.Common.Models;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<Response<bool>>
{
    public string ProductId { get; set; }

    public DeleteProductCommand(string productId)
    {
        ProductId = productId;
    }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<bool>>
{
    private readonly IProductRepository _repository;

    public DeleteProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _repository.DeleteProduct(request.ProductId);
            return Response.Ok(true);
        }
        catch (Exception e)
        {
            return Response.Fail<bool>(e.Message);
        }
    }
}