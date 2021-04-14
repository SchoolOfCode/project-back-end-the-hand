using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Extensions.Configuration;
    [ApiController]
    [Route("[controller]")]

    public class ResponseController : ControllerBase 
    {
        IConfiguration _configuration;

        public ResponseController (IConfiguration configuration)
        {
        _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string messageBody, string mobile)
        {
            TwilioClient.Init(_configuration["TWILIOID"],_configuration["TWILIOTOKEN"]); 

            var response = MessageResource.Create(
            body: $"{messageBody}",from: new Twilio.Types.PhoneNumber("+447782623184"), to: new Twilio.Types.PhoneNumber($"{mobile}"));
    
            return Ok(response.Sid);
        }
    }  

