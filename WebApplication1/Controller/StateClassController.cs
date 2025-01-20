using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApplication1.Controller;

[ApiController]
[Route("api/[controller]")]
public class StateClassController : ControllerBase
{
    [HttpGet]
    [Route("list")]
    public IActionResult List()
    {
        try
        {
            return Ok(Program.api_stateClasses.List());
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest("Error controller");
        }
    }

    public class ItemStateClass
    {
        public string code { get; set; } = "";
        public string name { get; set; } = "";
    }

    [HttpPost]
    [Route("create")]
    public IActionResult Create(ItemStateClass item)
    {
        try
        {
            string result = Program.api_stateClasses.Create(item.code, item.name);
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
    [Route("edit")]
    public IActionResult Edit(ItemStateClass item)
    {
        try
        {
            string result = Program.api_stateClasses.Edit(item.code, item.name);
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
            string result = Program.api_stateClasses.Delete(code);
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
}
