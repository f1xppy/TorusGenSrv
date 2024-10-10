using TorusGenSrv.Models;
namespace TorusGenSrv.Generators;
public class RandomPointsBetweenSpheres
{
    double R1; // Радиус меньшей сферы
    double R2; // Радиус большей сферы
    private readonly Random random;

    public RandomPointsBetweenSpheres(double innerRadius, double outerRadius)
    {
        if (innerRadius >= outerRadius)
        {
            throw new ArgumentException("Внутренний радиус должен быть меньше внешнего радиуса.");
        }

        R1 = innerRadius;
        R2 = outerRadius;
        random = new Random();
    }

    public Point[] GeneratePoints(int numPoints)
    {
        var points = new Point[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            double r = R1 + (R2 - R1) * random.NextDouble();
            double theta = 2 * Math.PI * random.NextDouble();
            double phi = Math.PI * random.NextDouble();
            points[i] = new Point
            {
                // Преобразование в прямоугольные координаты
                X = r * Math.Sin(phi) * Math.Cos(theta),
                Y = r * Math.Sin(phi) * Math.Sin(theta),
                Z = r * Math.Cos(phi)
            };
        }

        return points;
    }
}