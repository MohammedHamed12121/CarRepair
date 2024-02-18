using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Stripe;

namespace CarRepair.Pages.Pages.Payment
{
    public class StripePayment : PageModel
    {
        private readonly ILogger<StripePayment> _logger;

        public StripePayment(ILogger<StripePayment> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = 1000,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };
            var service = new PaymentIntentService();
            var intent = service.Create(options);

        }
    }
}