using TorusGenSrv.Models;
namespace TorusGenSrv.Generators;
public class RandomPointsBetweenSpheres
{
    double R1; // Радиус внутренней сферы
    double R2; // Радиус внешней сферы
    private readonly Random random;

    // Конструктор класса
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

    // Метод для генерации случайных точек
    public Point[] GeneratePoints(int numPoints)
    {
        var points = new Point[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            // Генерируем радиус r в пределах от R1 до R2
            double r = R1 + (R2 - R1) * random.NextDouble();

            // Генерируем угол theta в пределах [0, 2π]
            double theta = 2 * Math.PI * random.NextDouble();

            // Генерируем угол phi в пределах [0, π]
            double phi = Math.PI * random.NextDouble();
            points[i] = new Point
            {
                // Преобразование в декартовы координаты
                X = r * Math.Sin(phi) * Math.Cos(theta),
                Y = r * Math.Sin(phi) * Math.Sin(theta),
                Z = r * Math.Cos(phi)
            };
        }

        return points;
    }
}