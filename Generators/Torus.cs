using TorusGenSrv.Models;

namespace TorusGenSrv.Generators;

public class TorusGenerator
{
    static readonly Random random = new();

    // Генерация случайного числа в заданном диапазоне
    static double RandomDouble(double min, double max)
    {
        return (random.NextDouble() * (max - min)) + min;
    }


    // Генерация тора без точек
    public static Torus GenerateTorus(double cubeEdge, double max_R, double min_R, double max_r, double min_r)
    {

        Torus torus = new()
        {
            Center = [RandomDouble(-cubeEdge / 2, cubeEdge / 2), RandomDouble(-cubeEdge / 2, cubeEdge / 2), RandomDouble(-cubeEdge / 2, cubeEdge / 2)],
            MinorRadius = RandomDouble(min_r, max_r),
            MajorRadius = RandomDouble(min_R, max_R),
            Rotation = [RandomDouble(0, 2 * Math.PI), RandomDouble(0, 2 * Math.PI), RandomDouble(0, 2 * Math.PI)],
            PointsOnMajorCircle = []
        };

        torus.MajorRadius -= torus.MinorRadius / 2;
        torus.MinorRadius /= 2;

        return torus;
    }
}