using Microsoft.AspNetCore.Mvc;
using Serilog;
using TestSchool.Model;
using static TestSchool.Apis.MySchool;

namespace TestSchool.Controller;

[ApiController]
[Route("api/[controller]")]
public class SchoolController : ControllerBase
{
    [HttpGet]
    [Route("list")]
    public IActionResult List()
    {
        try
        {
            return Ok(Program.api_school.List());
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    [HttpGet]
    [Route("listIncludeWhere")]
    public IActionResult ListIncludeWhere()
    {
        try
        {
            return Ok(Program.api_school.ListIncludeWhere());
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    public class HttpItemCreateSchool
    {
        public string code { get; set; } = "";
        public string name { get; set; } = "";
        public string des { get; set; } = "";
    }

    [HttpPost]
    [Route("create")]
    public IActionResult Create(HttpItemCreateSchool item)
    {
        try
        {
            int result = Program.api_school.Create(item.code, item.name, item.des);
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
                        tmp = "School is exist";
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

    public class HttpItemEditSchool
    {
        public string name { get; set; } = "";
        public string des { get; set; } = "";
    }

    [HttpPut]
    [Route("{code}/edit")]
    public IActionResult Edit(string code, HttpItemEditSchool item)
    {
        try
        {
            int result = Program.api_school.Edit(code, item.name, item.des);
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
                        tmp = "School is not exist";
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
    [Route("{school}/delete")]
    public IActionResult Delete(string school)
    {
        try
        {
            int result = Program.api_school.Delete(school);
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
                        tmp = "School is not exist";
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
    [Route("{student}/compareWith/{teacher}")]
    public IActionResult CompareStudentWithTeacher(string student, string teacher)
    {
        try
        {
            int result = Program.api_school.CompareStudentWithTeacher(student, teacher);
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
                        tmp = "Student is not exist";
                        break;
                    case -2:
                        tmp = "Teacher is not exist";
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
