using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.Types;

namespace TwilioDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TwilioController : ControllerBase
    {


        private readonly ILogger<TwilioController> _logger;

        public TwilioController(ILogger<TwilioController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult SendMesage(string phone)
        {
            TwilioClient.Init("AC40a639a7a0fcd819212d994dcdb3582d", "085234e04090c20036160c80128249a3");

            var message = MessageResource.Create(
                body: "What is your name",
                from: new PhoneNumber("+1(628)232-3896"),
                to: new PhoneNumber(phone)

                );

            return StatusCode(200, new { message = message.Sid });
        }


        [HttpPost]
        public IActionResult RecieveMesage([FromForm] TwilioSMS twilioSMS)
        {
            var msg = new MessagingResponse();

            msg.Message("Thank you for replying! We will look into it.");



            return Content(msg.ToString(), "application/xml");
        }
    }
}
