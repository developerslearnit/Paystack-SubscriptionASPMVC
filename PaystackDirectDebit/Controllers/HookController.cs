using Newtonsoft.Json;
using PaystackDirectDebit.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PaystackDirectDebit.Controllers
{
    [RoutePrefix("hook")]
    public class HookController : Controller
    {
        
        [Route("events")]
        public ActionResult Listen()
        {
            var requestBody = Request.InputStream;
            requestBody.Seek(0, System.IO.SeekOrigin.Begin);
            string jsonString = new StreamReader(requestBody).ReadToEnd();

            ChargeEventViewModel eventOjb = null;

            eventOjb = JsonConvert.DeserializeObject<ChargeEventViewModel>(jsonString);

            if (eventOjb.@event == "charge.success")
            {
                //Update subscribers account

            // eventOjb.data contains every information needed
            }

            return null;
        }
    }
}