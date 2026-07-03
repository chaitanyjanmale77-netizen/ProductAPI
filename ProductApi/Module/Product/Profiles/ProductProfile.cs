using System.Runtime;
using AutoMapper;

public class ProductProfile: Profile
{
    public ProductProfile()
    {
        CreateMap<Product,ProductDTO>();
        CreateMap<UpdatedProductDTO,Product>();
        CreateMap<CreateProductDTO,Product>();
    }
}