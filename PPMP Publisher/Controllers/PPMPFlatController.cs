using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IoT.PPMP.Machine;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PPMP_Publisher.Controllers
{
   
    public class PPMPFlatController : AbstractPPMPController
    {
        [HttpPost("/rest/v2/flat/message")]
        public void Post([FromBody] MessagePayload message)
        {
            if (message != null)
            { 
                IList<FlatMessagePayload> messages = FlatMessagePayload.ToFlatMessagePayloads(message);
                if (messages != null && messages.Count > 0)
                {
                    Dictionary<string, string> props = new Dictionary<string, string>() { { "flat", "true" } };

                    foreach(FlatMessagePayload flatMessage in messages)
                    {
                        this.PublishToIoTHub(flatMessage, props);
                    }
                }
            }
        }
    }
}
