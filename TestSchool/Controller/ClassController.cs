using Microsoft.AspNetCore.Mvc;
using Serilog;
using TestSchool.Apis;

namespace TestSchool.Controller;

[ApiController]
[Route("api/[controller]")]
public class ClassController : ControllerBase
{
    [HttpGet]
    [Route("list")]
    public IActionResult List()
    {
        try
        {
            return Ok(Program.api_class.ListClasses());
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    public class HttpItemClass
    {
        public string code { get; set; } = "";
        public string name { get; set; } = "";
    }

    [HttpPost]
    [Route("create")]
    public IActionResult Create(HttpItemClass item)
    {
        try
        {
            string result = Program.api_class.Create(item.code, item.name);
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
    [Route("edit/{code}")]
    public IActionResult Edit(HttpItemClass item)
    {
        try
        {
            string result = Program.api_class.Edit(item.code, item.name);
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
    [Route("delete/{code}")]
    public IActionResult Delete(string code)
    {
        try
        {
            string result = Program.api_class.Delete(code);
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
    [Route("{classs}/addStudent/{student}")]
    public IActionResult AddStudentToClass(string classs, string student)
    {
        try
        {
            string result = Program.api_class.AddStudentToClass(student, classs);
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
    [Route("{classs}/removeStudent/{student}")]
    public IActionResult RemoveStudentFromClass(string classs, string student)
    {
        try
        {
            string result = Program.api_class.RemoveStudentFromClass(student, classs);
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
    [Route("{classs}/setState/{state}")]
    public IActionResult SetState(string classs, string state)
    {
        try
        {
            string result = Program.api_class.SetState(classs, state);
            if (string.IsNullOrEmpty(result))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }
}
