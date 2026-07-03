public interface IProductRepository
    : IGenricRepository<Product>
{
    Task<List<Product>> GetAllAsync(QueryParameter qs);
}