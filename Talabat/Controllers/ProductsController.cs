using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net.Http.Headers;
using talabat.Core;
using talabat.Core.Entities;
using talabat.Core.Repositories;
using talabat.Core.Specifications;
using Talabat.Core.Specifications;
using Talabat.DTOs;
using Talabat.Errors;
using Talabat.Helpers;

namespace Talabat.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IGenericRepository<Product> _productsRepo;
        //private readonly IGenericRepository<ProductBrand> _brandsrepo;
        //private readonly IGenericRepository<ProductType> _typesepo;
        private readonly IMapper _mapper;

        public ProductsController(
            //IGenericRepository<Product> productsRepo,
            //IGenericRepository<ProductBrand> brandsrepo,
            //IGenericRepository<ProductType> typesepo,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_productsRepo = productsRepo;
            //_brandsrepo = brandsrepo;
            //_typesepo = typesepo;
            _mapper = mapper;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts( [FromQuery] ProductSpecParams SpecParams)
        {
            var spec = new ProductWithBrandAndTypeSpecfications(SpecParams);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var countSpec = new ProductWithFilterationForCountSpecification(SpecParams);
            var count = await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(countSpec);
            return Ok(new Pagination<ProductToReturnDto>(SpecParams.PageIndex, SpecParams.PageSize,count, data));
        }
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecfications(id);
            var product = await _unitOfWork.Repository<Product>().GetByEntityWithSpecAsync(spec);
            if(product == null) return NotFound(new ApiResponse(400));
            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(types);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);
        }
    }
}
