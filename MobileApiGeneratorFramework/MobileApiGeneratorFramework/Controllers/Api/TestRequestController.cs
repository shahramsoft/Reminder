using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileApiGeneratorFramework.ActionFilters;

namespace MobileApiGeneratorFramework.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/TestRequest")]
    public class TestRequestController : Controller
    {
        [TokenValidator]
        [HttpGet]
        public string Get()
        {
            return "Ok";
        }
    }
}