using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRepair.Pages.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        // if user is authenticated 
        if(User.Identity.IsAuthenticated)
        {

            // if the role is user
            return RedirectToPage("Users/SentAppointments");
        }

        // if the user is not authonticated
        return Page();
    }
}
