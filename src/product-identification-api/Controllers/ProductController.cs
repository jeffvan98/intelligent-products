using IntelligentProducts.ProductIdentificationApi.Contracts;
using IntelligentProducts.ProductIdentificationApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// See: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code#scaffold-a-controller

namespace IntelligentProducts.ProductIdentificationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    public ProductController(ProductIdentificationContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO productDTO)
    {
        var product = DTOToProduct(productDTO);

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetProductById), 
            new { id = product.Id }, 
            ProductToDTO(product));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProductById(int id)
    {
        var product = await _context.Products
            .Include(p => p.AlternateIdentities)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        var productDTO = ProductToDTO(product);
        return productDTO;
    }

    [HttpGet("{kind}/{id}")]
    public async Task<ActionResult<ProductDTO>> GetProductByKindAndId(string kind, string id)
    {
        var identityType = Enum.Parse<IdentityTypes>(kind);
        var product = await _context.Products
            .Include(p => p.AlternateIdentities)
            .FirstOrDefaultAsync(p => p.AlternateIdentities.Any(a => a.IdentityType == identityType && a.Value == id));

        if (product == null)
        {
            return NotFound();
        }

        var productDTO = ProductToDTO(product);
        return productDTO;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDTO productDTO)
    {
        if (id != productDTO.Id)
        {
            return BadRequest();
        }

        var product = await _context.Products
            .Include(p => p.AlternateIdentities)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        product.Name = productDTO.Name;
        product.Description = productDTO.Description;
        product.ProductType = (ProductTypes)(int)productDTO.ProductType;
        product.IntroductionDate = productDTO.IntroductionDate;
        product.SalesDiscontinuationDate = productDTO.SalesDiscontinuationDate;
        product.SupportDiscontinuationDate = productDTO.SupportDiscontinuationDate;
        
        product.AlternateIdentities.Clear();
        foreach(var aid in productDTO.AlternateIdentities)
        {
            product.AlternateIdentities.Add(new AlternateIdentity()
            {
                ProductId = product.Id,
                IdentityType = (IdentityTypes)(int)aid.IdentityType,
                Value = aid.Value
            });
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            if (!await ProductExists(id))
            {
                return NotFound();
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> ProductExists(int id)
    {
        return await _context.Products.AnyAsync(e => e.Id == id);
    }

    private static ProductDTO ProductToDTO(Product product)
    {
        var productDTO = new ProductDTO()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ProductType = (ProductTypesDTO)(int)product.ProductType,
            IntroductionDate = product.IntroductionDate,
            SalesDiscontinuationDate = product.SalesDiscontinuationDate,
            SupportDiscontinuationDate = product.SupportDiscontinuationDate,
            AlternateIdentities = product.AlternateIdentities.Select(a => new AlternateIdentityDTO()
            {
                IdentityType = (IdentityTypesDTO)(int)a.IdentityType,
                Value = a.Value
            }).ToList()
        };

        return productDTO;
    }  

    private static Product DTOToProduct(ProductDTO productDTO)
    {
        var product = new Product()
        {
            Id = productDTO.Id,
            Name = productDTO.Name,
            Description = productDTO.Description,
            ProductType = (ProductTypes)(int)productDTO.ProductType,
            IntroductionDate = productDTO.IntroductionDate,
            SalesDiscontinuationDate = productDTO.SalesDiscontinuationDate,
            SupportDiscontinuationDate = productDTO.SupportDiscontinuationDate,
            AlternateIdentities = productDTO.AlternateIdentities.Select(a => new AlternateIdentity()
            {
                ProductId = productDTO.Id,
                IdentityType = (IdentityTypes)(int)a.IdentityType,
                Value = a.Value
            }).ToList()
        };

        return product;
    }

    private readonly ProductIdentificationContext _context;
}
