using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Waes.Core;

namespace Waes.Controllers
{   
    [ApiController]
    public class DiffController : ControllerBase
    {
        [HttpGet]
        [Route("v1/[controller]/{id}/left")]
        public ActionResult<string> GetLeft(int id) => WaesService.Get(id).Left;
        

        [HttpGet]
        [Route("v1/[controller]/{id}/right")]
        public ActionResult<string> GetRight(int id) => WaesService.Get(id).Right;

        [HttpGet]
        [Route("v1/[controller]/{id}/")]
        public ActionResult<ComparerResult> GetDiff(int id)
        {
            try
            {
                var ent = WaesService.Get(id);
                return Ok(Comparer.Compare(ent.Left, ent.Right));    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("v1/[controller]/{id}/left")]
        public ActionResult PostLeft([FromQuery] int id, [FromBody] Entity ent)    
        {
            try
            {
                return Ok(WaesService.SaveOrUpdate(ent));    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("v1/[controller]/{id}/right")]
        public ActionResult PostRight([FromQuery] int id, [FromBody] Entity ent)
        {
            try
            {
                return Ok(WaesService.SaveOrUpdate(ent));    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}