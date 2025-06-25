using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blazor_routing_custom.Pages;

public class SearchModel : PageModel
{
    private readonly ProductService _productService;
    private readonly LinkGenerator _link;
    
    public SearchModel(ProductService productService, LinkGenerator link)
    {
        _productService = productService;
        _link = link;
    }
    
    [BindProperty, Required]
    public string SearchTerm { get; set; }
    
    public List<Product> Result { get; set; }
    
    public void OnGet()
    {
        
        
    }

    public void OnPost()
    {
        
    }

    public void OnPostIgnoreCase()
    {
        
    }
    
}