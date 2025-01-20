using Microsoft.AspNetCore.Mvc;
using Serilog;
using static WebApplication1.Apis.MySchool;

namespace WebApplication1.Controller;

[ApiController]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    [HttpGet]
    [Route("list")]
    public IActionResult List()
    {
        try
        {
            return Ok(Program.api_teacher.List());
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    [HttpPost]
    [Route("create")]
    public IActionResult Create(ItemCommon item)
    {
        try
        {
            int result = Program.api_teacher.Create(item.code);
            if (result >= 0)
            {
                return Ok();
            }
            else
            {
                string tmp = "";
                switch (result)
                {
                    case -1:
                        tmp = "Teacher is exist";
                        break;
                    case -9:
                        tmp = "Error database";
                        break;
                    default:
                        tmp = string.Format("Code error: {0}", result);
                        break;
                }
                return BadRequest(tmp);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    [HttpDelete]
    [Route("{teacher}/delete")]
    public IActionResult Delete(string teacher)
    {
        try
        {
            int result = Program.api_teacher.Delete(teacher);
            if (result >= 0)
            {
                return Ok();
            }
            else
            {
                string tmp = "";
                switch (result)
                {
                    case -1:
                        tmp = "Teacher is not exist";
                        break;
                    case -9:
                        tmp = "Error database";
                        break;
                    default:
                        tmp = string.Format("Code error: {0}", result);
                        break;
                }
                return BadRequest(tmp);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    [HttpPut]
    [Route("{teacher}/addTo/{school}")]
    public IActionResult AddTeacherToSchool(string teacher, string school)
    {
        try
        {
            int result = Program.api_school.AddTeacherToSchool(teacher, school);
            if (result >= 0)
            {
                return Ok();
            }
            else
            {
                string tmp = "";
                switch (result)
                {
                    case -1:
                        tmp = "Teacher is not exist";
                        break;
                    case -2:
                        tmp = "School is not exist";
                        break;
                    case -3:
                        tmp = "Added";
                        break;
                    case -9:
                        tmp = "Error database";
                        break;
                    default:
                        tmp = string.Format("Code error: {0}", result);
                        break;
                }
                return BadRequest(tmp);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    [HttpPut]
    [Route("{teacher}/removeFrom/{school}")]
    public IActionResult RemoveTeacherFromSchool(string teacher, string school)
    {
        try
        {
            int result = Program.api_school.RemoveTeacherFromSchool(teacher, school);
            if (result >= 0)
            {
                return Ok();
            }
            else
            {
                string tmp = "";
                switch (result)
                {
                    case -1:
                        tmp = "Teacher is not exist";
                        break;
                    case -2:
                        tmp = "School is not exist";
                        break;
                    case -3:
                        tmp = "Removed";
                        break;
                    case -9:
                        tmp = "Error database";
                        break;
                    default:
                        tmp = string.Format("Code error: {0}", result);
                        break;
                }
                return BadRequest(tmp);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    [HttpPost]
    [Route("{teacher}/compareWith/{school}")]
    public IActionResult CompareTeacherWithSchool(string teacher, string school)
    {
        try
        {
            int result = Program.api_school.CompareTeacherWithSchool(teacher, school);
            if (result >= 0)
            {
                return Ok();
            }
            else
            {
                string tmp = "";
                switch (result)
                {
                    case -1:
                        tmp = "Teacher is not exist";
                        break;
                    case -2:
                        tmp = "School is not exist";
                        break;
                    case -3:
                        tmp = "False";
                        break;
                    case -9:
                        tmp = "Error database";
                        break;
                    default:
                        tmp = string.Format("Code error: {0}", result);
                        break;
                }
                return BadRequest(tmp);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }
}
