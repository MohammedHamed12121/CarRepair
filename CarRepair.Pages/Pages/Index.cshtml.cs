using CarRepair.Pages.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;

namespace CarRepair.Pages.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<AppUser> _userManager;
    public List<Plan> AllPlans { get; private set; }

    public IndexModel(ILogger<IndexModel> logger, UserManager<AppUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
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
        // add the subscription
        var service = new PlanService();
        AllPlans = service.List().ToList();
        return Page();
    }

    public void OnPost(string id)
        {
            var subscriptionOptions = new SubscriptionCreateOptions{
                Customer = _userManager.GetUserId(User),
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        Plan = id
                    }
                }
            };
            var service = new SubscriptionService();
            Subscription subscription = service.Create(subscriptionOptions);

        }
}
