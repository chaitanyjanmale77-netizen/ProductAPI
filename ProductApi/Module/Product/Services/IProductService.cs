public interface IProductService
{
    Task<ApiResponse<List<ProductDTO>>> GetAllAsync(QueryParameter qs);

    Task<ApiResponse<ProductDTO>> GetIdByAsync(int id);

    Task<ApiResponse<ProductDTO>> AddByAsync(CreateProductDTO dto);

    Task UpdateAsync(int id, UpdatedProductDTO dto);

    Task DeleteAsync(int id);
}