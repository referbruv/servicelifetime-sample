using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ScopedServices;

namespace HostedServicesApp.Controllers
{
    public class DemoController : Controller
    {
        private readonly TimestampServiceBase service;
        private readonly IServiceProvider serviceProvider;

        public DemoController(TimestampServiceBase service, IServiceProvider serviceProvider)
        {
            this.service = service;
            this.serviceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var tsService = scope.ServiceProvider.GetRequiredService<TimestampServiceBase>();
                ViewBag.Timestamp = tsService.GetCurrentTimestamp();
            }

            return View(this.service.GetCurrentTimestamp());
        }
    }
}