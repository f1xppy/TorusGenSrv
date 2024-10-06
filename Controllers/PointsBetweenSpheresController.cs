using Microsoft.AspNetCore.Mvc;
using TorusGenSrv.Models;
using Newtonsoft.Json;

namespace TorusGenSrv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PointsController : ControllerBase
{
    [HttpPost("generateByNumber")]
    public IActionResult Generate([FromBody] PointsRequest request)
    {
        if (request == null)
        {
            return BadRequest("Неверный запрос.");
        }
        Generators.RandomPointsBetweenSpheres pointGenerator = new(Convert.ToDouble(request.MinorRadius), Convert.ToDouble(request.MajorRadius));
        var points = pointGenerator.GeneratePoints(Convert.ToInt32(request.NumberOfPoints));



        return Ok(JsonConvert.SerializeObject(points, Formatting.Indented));
    }

}
