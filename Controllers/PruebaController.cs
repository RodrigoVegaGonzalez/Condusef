using Microsoft.AspNetCore.Mvc;

namespace Condusef.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PruebaController : Controller
    {
        [HttpGet("test-conexion")]
        public JsonResult Test_Conexion()
        {
            var response = new
            {
                message = "La conexion está funcionando"
            };
            return new JsonResult(response);
        }
    }
}
