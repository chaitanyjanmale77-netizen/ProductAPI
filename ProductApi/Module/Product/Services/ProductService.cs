using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

public class ProductService:IProductService
{
    private readonly IProductRepository _repository;
    private readonly ILogger<ProductService> _logger;
    private readonly IMapper _mapper;
    public ProductService(IProductRepository repository,IMapper mapper,ILogger<ProductService> logger)
    {
        _repository=repository;
        _mapper=mapper;
        _logger=logger;
    }
    public async Task<ApiResponse<List<ProductDTO>>> GetAllAsync(QueryParameter qs)
    {
        var product =await _repository.GetAllAsync(qs);
        _logger.LogInformation("Getting All Products");
        return new ApiResponse<List<ProductDTO>>
        {
            Success=true,Message="Fetched Success !" ,Data =_mapper.Map<List<ProductDTO>>(product)
        };
        
    }
    public async Task<ApiResponse<ProductDTO>> GetIdByAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
       
        return new ApiResponse<ProductDTO>
        {
            Success=true,Message="Fetched Success !" ,Data =_mapper.Map<ProductDTO>(product)
        };
    }

    public async Task<ApiResponse<ProductDTO>> AddByAsync(CreateProductDTO dto)
    {
        var product=new Product
        {
           Id=dto.id,Name=dto.Name,Description=dto.Description,Price=dto.Price
        };
        await _repository.AddAsync(product);
        _logger.LogInformation(
    "Creating product {Name}",
    dto.Name);
       
        return new ApiResponse<ProductDTO>
        {
            Success=true,Message="added Success !" ,Data =_mapper.Map<ProductDTO>(product)
        };

    }
    public async Task DeleteAsync(int id)
{
    var product = await _repository.GetByIdAsync(id);

    if (product == null)
    {
        return;
    }

    await _repository.DeleteAsync(product);
}
public async Task UpdateAsync(int id, UpdatedProductDTO dto)
{
    var product = await _repository.GetByIdAsync(id);

    if (product == null)
    {
        return;
    }
    _mapper.Map(dto,product);

    await _repository.UpdateAsync(product);
}
}