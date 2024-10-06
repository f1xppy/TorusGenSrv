using Microsoft.AspNetCore.Mvc;
using TorusGenSrv.Models;

namespace TorusGenSrv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TorusController : ControllerBase
    {
        [HttpPost("generateByNumber")]
        public IActionResult GenerateToriByNumber([FromBody] TorusRequest request)
        {
            if (request == null)
            {
                return BadRequest("Неверный запрос.");
            }

            string result = Results.GenerateToriByNumber(
                Convert.ToInt32(request.NumberOfTori),
                Convert.ToDouble(request.CubeEdge),
                Convert.ToDouble(request.MaxMajorRadius),
                Convert.ToDouble(request.MinMajorRadius),
                Convert.ToDouble(request.MaxMinorRadius),
                Convert.ToDouble(request.MinMinorRadius)
            );


            return Ok(result);
        }

        [HttpPost("getNc")]
        public IActionResult GetNc([FromBody] TorusRequest request)
        {
            if (request == null)
            {
                return BadRequest("Неверный запрос.");
            }

            string result = Results.GetNc(
                Convert.ToInt32(request.NumberOfTori),
                Convert.ToDouble(request.CubeEdge),
                Convert.ToDouble(request.MaxMajorRadius),
                Convert.ToDouble(request.MinMajorRadius),
                Convert.ToDouble(request.MaxMinorRadius),
                Convert.ToDouble(request.MinMinorRadius)
            );


            return Ok(result);
        }

        [HttpPost("getNumberOfTori")]
        public IActionResult GetNumberOfTori([FromBody] TorusRequest request)
        {
            if (request == null)
            {
                return BadRequest("Неверный запрос.");
            }
            string result = Results.GetNumberOfTori(
                Convert.ToDouble(request.Nc),
                Convert.ToDouble(request.CubeEdge),
                Convert.ToDouble(request.MaxMajorRadius),
                Convert.ToDouble(request.MinMajorRadius),
                Convert.ToDouble(request.MaxMinorRadius),
                Convert.ToDouble(request.MinMinorRadius)
            );


            return Ok(result);
        }
    }

}
