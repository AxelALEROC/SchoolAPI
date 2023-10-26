using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class PruebaController : ControllerBase
    {
        [HttpGet("prueba")]
        public string puebaApi()
        {
            return "Prueba";
        }
    }
}
