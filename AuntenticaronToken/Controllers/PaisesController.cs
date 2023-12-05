using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutenticarConToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("Lista")]

        public async Task<ActionResult> Lista()
        {
            var listaPaises = await Task.FromResult(new List<string> { "Colombia", "Dinamarca", "Suecia", "Mexico" });
            return Ok(listaPaises);
        }
        
    }
}
