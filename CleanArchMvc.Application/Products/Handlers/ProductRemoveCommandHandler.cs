﻿using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers;

public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
{
    public readonly IProductRepository _productRepository;

    public ProductRemoveCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.Id);

        if (product == null)
            throw new ApplicationException("Entity could not be found.");
        return await _productRepository.Remove(product);
    }
}