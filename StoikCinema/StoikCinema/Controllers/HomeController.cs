using Stripe;
using Newtonsoft.Json;
using StoikCinema.Models;
using System.Diagnostics;
using StoikCinema.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace StoikCinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IOptions<StripeSettings> options)
        {

            StripeConfiguration.ApiKey = options.Value.SecretKey;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        [Route("/Donate")]
        public IActionResult Donate()
        {
            return View();
        }
        public IActionResult Stripe()
        {
            return this.View();
        }
        [HttpPost("Stripe")]
        public IActionResult StripePost()
        {
            StripeConfiguration.ApiKey = "sk_test_51HKKyiJ1Wp4jNKPf1hA7WpHr2ABtPVe9l7gXmuWxOzjpkrmxVKVBVgwVQfP12P1fLLxaTVMFEY3LfY9fnJVCC1Af00N5Clyfht";

            var options = new PaymentIntentCreateOptions
            {
                Amount = 2000,
                Currency = "bgn",
                PaymentMethodTypes = new List<string> { "card", }
                ,
            };
            var service = new PaymentIntentService();
            service.Create(options);
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    [Route("create-payment-intent")]
    [ApiController]
    public class PaymentIntentApiController : Controller
    {
        [HttpPost]
        public ActionResult Create(PaymentIntentCreateRequest request)
        {
            var paymentIntents = new PaymentIntentService();
            var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            {
                Amount = CalculateOrderAmount(request.Items),
                Currency = "bgn",
            });

            return Json(new { clientSecret = paymentIntent.ClientSecret });
        }

        private int CalculateOrderAmount(Item[] items)
        {

            return 1000 * 100;
        }


    }
}