using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Microsoft.Extensions.Configuration;

    [ApiController]
    [Route("[controller]")]

    public class ResponseController : ControllerBase 
    {
        IConfiguration _configuration;

        [HttpGet]
        public IActionResult Get([FromQuery] string messageBody, string mobile)
        {
            // Initialize twilio client
            TwilioClient.Init("AC56b6be20a25f4637181b1cf585e25875","3f113dd9ca21d7140b8cd5e9585cd9b6");

            var response = MessageResource.Create(
            body: $"{messageBody}",from: new Twilio.Types.PhoneNumber("+447782623184"), to: new Twilio.Types.PhoneNumber($"{mobile}"));
    
            return Ok(response.Sid);
        }
    }  

