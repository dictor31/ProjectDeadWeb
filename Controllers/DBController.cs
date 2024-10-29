using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using WebDead.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDead.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DBController : ControllerBase
    {
        BD.BD dataBase;
        public DBController(BD.BD dataBase) 
        {
            this.dataBase = dataBase;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult> Get()
        {
            return Ok();
        }
    }
}
