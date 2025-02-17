using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace TestChangeTrackingInCameraCenterBe.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    [Route("ListUserCamerasTest")]
    public IActionResult ListUserCamerasTest()
    {
        try
        {
            return Ok(Program.api_test.ListUserCamerasTest());
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    [HttpGet]
    [Route("ListCamerasCamerasTest")]
    public IActionResult ListCamerasCamerasTest()
    {
        try
        {
            return Ok(Program.api_test.ListCamerasCamerasTest());
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }
}
