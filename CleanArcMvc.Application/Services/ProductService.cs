using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
	public class ProductService : IProductService
	{
		private IProductRepository _productRepository;
		private readonly IMapper _mapper;
		public ProductService(IMapper mapper, IProductRepository productRepository)
		{
			_productRepository = productRepository ??
				throw new ArgumentNullException(nameof(productRepository));
			_mapper = mapper;
		}

		public async Task<IEnumerable<ProductDTO>> GetProducts()
		{
			var productEntity = await _productRepository.GetProducts();
			return _mapper.Map<IEnumerable<ProductDTO>>(productEntity);
		}

		public async Task<ProductDTO> GetById(int? id)
		{
			var productEntity = await _productRepository.GetById(id);
			return _mapper.Map<ProductDTO>(productEntity);
		}

		public async Task<ProductDTO> GetProductCategory(int? id)
		{
			var productEntity = await _productRepository.GetProductCategory(id);
			return _mapper.Map<ProductDTO>(productEntity);
		}

		public async Task Add(ProductDTO productDTO)
		{
			var productEntity = _mapper.Map<Product>(productDTO);
			await _productRepository.Create(productEntity);
		}

		public async Task Update(ProductDTO productDTO)
		{
			var productEntity = _mapper.Map<Product>(productDTO);
			await _productRepository.Update(productEntity);
		}

		public async Task Remove(int? id)
		{
			var productEntity = _productRepository.GetById(id).Result;
			await _productRepository.Remove(productEntity);
		}
	}
}
