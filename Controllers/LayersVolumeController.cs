using Microsoft.AspNetCore.Mvc;
using TorusGenSrv.Models;

namespace TorusGenSrv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LayersVolumeController : ControllerBase
{
    [HttpPost("getLayersVolume")]
    public IActionResult GetLayersVolume([FromBody] LayersVolumeRequest request)
    {
        if (request == null)
        {
            return BadRequest("Неверный запрос.");
        }
        int k = Convert.ToInt16(request.LayersNumber);
        double cubeEdge = Convert.ToDouble(request.CubeEdge);
        double incrR = cubeEdge / 2 / k;
        List<double> volume = [];
        bool isInside = false;
        int numberInside = 0;
        double ratio = 0;
        double v = 0;
        var toriList = DataStorage.DataStorage.GetData("toriList") as List<Torus>;
        for (int i = 1; i < k; i++)
        {
            int number_i = 10000;
            Generators.RandomPointsBetweenSpheres pointsGenerator = new((i - 1) * incrR, i * incrR);
            var points_i = pointsGenerator.GeneratePoints(number_i);
            numberInside = 0;

            foreach (var point in points_i)
            {
                isInside = false;
                foreach (var torus in toriList)
                {
                    foreach (var sphereCenter in torus.PointsOnMajorCircle)
                    {
                        double distanceSquared = Math.Pow(point.X - sphereCenter[0], 2) +
                                 Math.Pow(point.Y - sphereCenter[1], 2) +
                                 Math.Pow(point.Z - sphereCenter[2], 2);
                        if (distanceSquared <= Math.Pow(torus.MinorRadius, 2))
                        {
                            isInside = true;
                            break;
                        }
                    }
                    if (isInside)
                    {
                        break;
                    }
                }
                if (isInside)
                {
                    numberInside++;
                }
            }
            ratio = Convert.ToDouble(numberInside) / Convert.ToDouble(number_i);
            v = 4 / 3 * Math.PI * (Math.Pow(i * incrR, 3) - Math.Pow((i - 1) * incrR, 3)) * ratio;
            volume.Add(v);
            Console.WriteLine($"{numberInside}, {number_i}, {ratio}");
        }
        List<Point> points = [];
        Random rand = new();
        double halfEdge = cubeEdge / 2;
        double sphereRadiusSquared = Math.Pow(k * incrR, 2);
        int number = 10000;
        while (points.Count < number)
        {
            Point point = new()
            {
                X = rand.NextDouble() * cubeEdge - halfEdge,
                Y = rand.NextDouble() * cubeEdge - halfEdge,
                Z = rand.NextDouble() * cubeEdge - halfEdge
            };

            if (Math.Pow(point.X, 2) + Math.Pow(point.Y, 2) + Math.Pow(point.Z, 2) >= sphereRadiusSquared)
            {
                points.Add(point);
            }
        }
        numberInside = 0;
        foreach (var point in points)
        {
            isInside = false;
            foreach (var torus in toriList)
            {
                foreach (var sphereCenter in torus.PointsOnMajorCircle)
                {
                    double distanceSquared = Math.Pow(point.X - sphereCenter[0], 2) +
                             Math.Pow(point.Y - sphereCenter[1], 2) +
                             Math.Pow(point.Z - sphereCenter[2], 2);
                    if (distanceSquared <= Math.Pow(torus.MinorRadius, 2))
                    {
                        isInside = true;
                        break;
                    }
                }
                if (isInside)
                {
                    break;
                }
            }
            if (isInside)
            {
                numberInside++;
            }
        }
        ratio = Convert.ToDouble(numberInside) / Convert.ToDouble(number);
        v = 4 / 3 * Math.PI * Math.Pow(k * incrR, 3) * ratio;
        volume.Add(v);
        Console.WriteLine($"{numberInside}, {number}, {ratio}");
        return Ok(volume);
    }

}