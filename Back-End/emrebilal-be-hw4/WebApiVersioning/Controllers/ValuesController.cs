using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiVersioning.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")] //URL API Versioning
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("1.1")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        public ActionResult<IEnumerable<string>> GetV1_1()
        {
            return new string[] { "version 1.1 - value1", "version 1.1 - value2 " };
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value" + id;
        }
    }
}
