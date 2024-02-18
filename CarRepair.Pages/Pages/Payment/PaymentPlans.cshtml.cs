using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.Pages.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Stripe;

namespace CarRepair.Pages.Pages.Payment
{
    public class PaymentPlans : PageModel
    {
        private readonly ILogger<PaymentPlans> _logger;
        private readonly UserManager<AppUser> _userManager;

        public PaymentPlans(ILogger<PaymentPlans> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public List<Plan> AllPlans { get; private set; }

        public void OnGet()
        {
            var service = new PlanService();
            AllPlans = service.List().ToList();
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
}