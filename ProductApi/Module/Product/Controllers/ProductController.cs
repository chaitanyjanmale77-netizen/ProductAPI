using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[Controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    
    public ProductController(IProductService service)
    {
        _service=service;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryParameter qs)
    {
        var Product = await _service.GetAllAsync(qs);
        return Ok(Product);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var Product=await _service.GetIdByAsync(id);
        
        if (Product == null)
        {
            return NotFound();
        }
        
        return Ok(Product);
    }

    [HttpPost]
     public async Task<IActionResult> CreateProduct( CreateProductDTO dto)
    {
        var product = await _service.AddByAsync(dto);
        return CreatedAtAction(nameof(GetById),new {id=0000},product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, UpdatedProductDTO dto)
    {

        await _service.UpdateAsync(id,dto);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        
        await _service.DeleteAsync(id);
        return NoContent();
    }
}