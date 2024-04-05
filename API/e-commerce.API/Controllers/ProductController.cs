
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using Core.Specifications;
using AutoMapper;
using e_commerce.API.Dtos;
using e_commerce.API.Helpers;

namespace e_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository <ProductBrand> _brandRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductType> _typeRepository;
        private readonly IMapper _mapper;
        public ProductController(IGenericRepository<ProductBrand> brandRepository,
            IGenericRepository<Product> productRepository,
            IGenericRepository<ProductType> typeRepository,
            IMapper mapper)
        {
            _brandRepository = brandRepository;
            _productRepository = productRepository;
            _typeRepository = typeRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productPrams)
        {
            // var spec = new ProductWithTypesAndBrandsSpecification(sort, brandId, typeId);
            var spec = new ProductWithTypesAndBrandsSpecification(productPrams);

            var countSpec = new ProductWithWiltersForCountSpecification(productPrams);

            var totalItems = await _productRepository.CountAsync(countSpec);
            var products = await _productRepository.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);



            return Ok(new Pagination<ProductToReturnDto>(productPrams.PageIndex, productPrams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(id);

            var product= await _productRepository.GetEntityWithSpec(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);

        }
        [HttpGet("brands")]
        public async Task<ActionResult<Product>> GetProductBrands()
        {
            return Ok(_brandRepository.ListAllAsync());

        }
        [HttpGet("types")]
        public async Task<ActionResult<Product>> GetProductTypes()
        {
            return Ok(_typeRepository.ListAllAsync());

        }
    }
}
