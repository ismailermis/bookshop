using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
         private readonly IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger, 
        IGenericRepository<Product> productRepo,
        IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo,IMapper mapper)
        {
            
            _logger = logger;
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<Product>>> GetProducts([FromQuery]ProductSpecParams productParams) 
        {
            var spec = new ProductsWithTypesAndBrandsSpeccification(productParams); 
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);
            var totalItems= await _productRepo.CountAsync(spec);
 
            //var products= await _productRepo.ListAllAsync();

            var productList = await _productRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(productList);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems,data));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product==null)
            {   
                return NotFound(new ApiResponse(404));
            }  
            return Ok(product);
        }
         [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var products= await _productBrandRepo.ListAllAsync();
            
            return Ok(products);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var products= await _productTypeRepo.ListAllAsync();
            return Ok(products);
        }
    }
}