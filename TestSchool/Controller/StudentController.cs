using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Serilog;
using System.Globalization;
using TestSchool.Apis;
using static TestSchool.Apis.MySchool;

namespace TestSchool.Controller;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    [HttpGet]
    [Route("list")]
    public IActionResult List()
    {
        try
        {
            return Ok(Program.api_student.List());
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    public class HttpItemCreateStudent
    {
        public string code { get; set; } = "";
        public string school { get; set; } = "";
    }

    [HttpGet]
    [Route("get")]
    public IActionResult GetStudentsFromSchool(string school)
    {
        try
        {
            return Ok(Program.api_student.GetStudentInSchool(school));
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    [HttpPost]
    [Route("create")]
    public IActionResult Create(HttpItemCreateStudent item)
    {
        try
        {
            string result = Program.api_student.Create(item.code, item.school);
            if (string.IsNullOrEmpty(result))
            {
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    public class HttpItemEditStudent
    {
        public string school { get; set; } = "";
    }

    [HttpPut]
    [Route("{student}/edit")]
    public IActionResult Edit(string student, HttpItemEditStudent item)
    {
        try
        {
            string result = Program.api_student.Edit(student, item.school);
            if (string.IsNullOrEmpty(result))
            {
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    [HttpDelete]
    [Route("{student}/{school}/delete")]
    public IActionResult DeleteStudentBySchool(string student, string school)
    {
        try
        {
            string result = Program.api_student.DeleteStudentBySchool(student, school);
            if (string.IsNullOrEmpty(result))
            {
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    [HttpDelete]
    [Route("{student}/delete")]
    public IActionResult Delete(string student)
    {
        try
        {
            string result = Program.api_student.Delete(student);
            if (string.IsNullOrEmpty(result))
            {
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    [HttpPut]
    [Route("{student}/addTo/{school}")]
    public IActionResult AddStudentToSchool(string student, string school)
    {
        try
        {
            int result = Program.api_school.AddStudentToSchool(student, school);
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
    [Route("{student}/removeFrom/{school}")]
    public IActionResult RemoveStudentFromSchool(string student, string school)
    {
        try
        {
            int result = Program.api_school.RemoveStudentFromSchool(student, school);
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
    [Route("{student}/compareWith/{school}")]
    public IActionResult CompareStudentWithSchool(string student, string school)
    {
        try
        {
            int result = Program.api_school.CompareStudentWithSchool(student, school);
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

    [HttpGet]
    [Route("ListInclude")]
    public IActionResult ListInclude()
    {
        try
        {
            return Ok(Program.api_student.ListTestInclude());
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }
}
