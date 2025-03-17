using Microsoft.AspNetCore.Mvc;

namespace TestSchool.Controller;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    [HttpGet]
    [Route("list")]
    public IActionResult list()
    {
        return Ok(Program.api_order.list());
    }

    [HttpPost]
    [Route("create")]
    public IActionResult create(string code)
    {
        string result = Program.api_order.create(code);
        if (string.IsNullOrEmpty(result))
        {
            return Ok();
        }
        return BadRequest(result);
    }

    [HttpDelete]
    [Route("delete/{code}")]
    public IActionResult delete(string code)
    {
        string result = Program.api_order.delete(code);
        if (string.IsNullOrEmpty(result))
        {
            return Ok();
        }
        return BadRequest(result);
    }

    [HttpPut]
    [Route("{order}/add/order/{orderChild}")]
    public IActionResult addOrderToOrder(string order, string orderChild)
    {
        string result = Program.api_order.addOrderToOrder(order, orderChild);
        if (string.IsNullOrEmpty(result))
        {
            return Ok();
        }
        return BadRequest(result);
    }

    [HttpPut]
    [Route("{order}/remove/order/{orderChild}")]
    public IActionResult removeOrderFromOrder(string order, string orderChild)
    {
        string result = Program.api_order.removeOrderFromOrder(order, orderChild);
        if (string.IsNullOrEmpty(result))
        {
            return Ok();
        }
        return BadRequest(result);
    }
}
