using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireTestController : ControllerBase
    {
        [HttpPost]
        [Route("welcome")]
        public IActionResult Welcome(string userName)
        {
            var jobId = BackgroundJob.Enqueue(() => SendWelcomeMail(userName));
            return Ok($"Job Id {jobId} Completed. Welcome Mail Sent!");
        }

        private void SendWelcomeMail(string userName)
        {
            //Logic to Mail the user
            Console.WriteLine($"Welcome to our application, {userName}");
        }

        [HttpPost]
        [Route("delayedWelcome")]
        public IActionResult DelayedWelcome(string userName)
        {
            var jobId = BackgroundJob.Schedule(() => SendDelayedWelcomeMail(userName), TimeSpan.FromMinutes(2));
            return Ok($"Job Id {jobId} Completed. Delayed Welcome Mail Sent!");
        }

        private void SendDelayedWelcomeMail(string userName)
        {
            //Logic to Mail the user
            Console.WriteLine($"Welcome to our application, {userName}");
        }

        [HttpPost]
        [Route("invoice")]
        public IActionResult Invoice(string userName)
        {
            RecurringJob.AddOrUpdate(() => SendInvoiceMail(userName), Cron.Monthly);
            return Ok($"Recurring Job Scheduled. Invoice will be mailed Monthly for {userName}!");
        }

        private void SendInvoiceMail(string userName)
        {
            //Logic to Mail the user
            Console.WriteLine($"Here is your invoice, {userName}");
        }

        [HttpPost]
        [Route("unsubscribe")]
        public IActionResult Unsubscribe(string userName)
        {
            var jobId = BackgroundJob.Enqueue(() => UnsubscribeUser(userName));
            BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine($"Sent Confirmation Mail to {userName}"));
            return Ok($"Unsubscribed");
        }
        private void UnsubscribeUser(string userName)
        {
            //Logic to Unsubscribe the user
            Console.WriteLine($"Unsubscribed {userName}");
        }
    }
}
