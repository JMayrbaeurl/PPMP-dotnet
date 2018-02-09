using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IoT.PPMP.Machine;
using Microsoft.IoT.PPMP.Measurement;
using Microsoft.IoT.PPMP.Process;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PPMPApi.Controllers
{
    public class PPMPController : Controller
    {
        [HttpPost("/rest/v2/message")]
        public void Post([FromBody]MessagePayload message)
        {
            Console.WriteLine(message);
        }

        [HttpPost("/rest/v2/measurement")]
        public void Post([FromBody]MeasurementPayload message)
        {
            Console.WriteLine(message);
        }

         [HttpPost("/rest/v2/process")]
        public void Post([FromBody]ProcessPayload message)
        {
            Console.WriteLine(message);
        }

        [Route("/rest/v2")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Welcome message", "PPMP REST server" };
        }
    }
}
