using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Waes.Core;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var left =  "123456789888abc44453qwe";
            var right = "124653789888abc99962qwe";
            var res = Comparer.Compare(left, right);
            
            return new string[] { "value1", res.Message };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var left =  "123456789888abc44453qwe";
            var right = "124653789888abc99962qwe";
            var res = Comparer.Compare(left, right);
            return res.Message;
            
        }
    }
}
