using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blazor_routing_custom.Pages.ProductDetails;

public class IndexModel : PageModel
{
    private readonly ProductService _service;
    
    public IndexModel(ProductService service)
    {
        _service = service;
    }
    
    public Product Selected { get; set; }
    
    public IActionResult OnGet(string name)
    {
        var url = Url.Page("Product", new { name = "super-fancy-widget"} );
        Selected = _service.GetProduct(name);

        if (Selected is null)
        {
            return NotFound();
        }

        return Page();
    }
}