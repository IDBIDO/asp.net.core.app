using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blazor_routing_custom.Pages;

public class TestModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public TestModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet() // page handler, named by convention
    {
    }
}