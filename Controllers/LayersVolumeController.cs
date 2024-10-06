using Microsoft.AspNetCore.Mvc;
using TorusGenSrv.Models;
using Newtonsoft.Json;

namespace TorusGenSrv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LayersVolumeController : ControllerBase
{
    [HttpPost("getLayersVolume")]
    public IActionResult Generate([FromBody] LayersVolumeRequest request)
    {
        if (request == null)
        {
            return BadRequest("Неверный запрос.");
        }
        int k = Convert.ToInt16(request.LayersNumber);
        double cubeEdge = Convert.ToDouble(request.CubeEdge);
        double incrR = (cubeEdge / 2) / k;
        double[] volume = [];
        for (int i = 2; i < k; i++)
        {
            Generators.RandomPointsBetweenSpheres pointsGenerator = new(i * incrR, (i - 1) * incrR);
            var points_i = pointsGenerator.GeneratePoints(10000);
            
        }



        return Ok();
    }

}