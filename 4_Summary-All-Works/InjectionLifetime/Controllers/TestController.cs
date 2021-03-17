using InjectionLifetime.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InjectionLifetime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        ITransientService _transientService1;
        ITransientService _transientService2;

        IScopedService _scopedService1;
        IScopedService _scopedService2;

        ISingletonService _singletonService1;
        ISingletonService _singletonService2;

        public TestController(ITransientService transientService1,
                              ITransientService transientService2,
                              IScopedService scopedService1,
                              IScopedService scopedService2,
                              ISingletonService singletonService1,
                              ISingletonService singletonService2)
        {
            _transientService1 = transientService1;
            _transientService2 = transientService2;

            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;

            _singletonService1 = singletonService1;
            _singletonService2 = singletonService2;
        }

        [HttpGet]
        public string Get()
        {
            string result = $"Transient - First Instance  : {_transientService1.GetID()} {Environment.NewLine}" +
                            $"Transient - Second Instance : {_transientService2.GetID()} {Environment.NewLine}" +
                            $"Scoped    - First Instance  : {_scopedService1.GetID()} {Environment.NewLine}" +
                            $"Scoped    - Second Instance : {_scopedService2.GetID()} {Environment.NewLine}" +
                            $"Singleton - First Instance  : {_singletonService1.GetID()} {Environment.NewLine}" +
                            $"Singleton - Second Instance : {_singletonService2.GetID()} {Environment.NewLine}";

            return result;
        }
    }
}
