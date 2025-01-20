using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApplication1.Controller;

[ApiController]
[Route("api/[controller]")]
public class JsonController : ControllerBase
{
    [HttpGet]
    [Route("checkJson")]
    public IActionResult Check()
    {
        try
        {
            return Ok(Program.api_json.Check());
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }
}
