using TorusGenSrv.Models;

namespace TorusGenSrv.Generators;


public class Points
{
    // Применение вращения вокруг осей X, Y и Z
    static double[] RotatePoint(double[] point, double[] center, double[] rotation)
    {
        // Перенос точки относительно центра
        double x = point[0] - center[0];
        double y = point[1] - center[1];
        double z = point[2] - center[2];

        // Вращение вокруг оси Z
        double cosZ = Math.Cos(rotation[2]);
        double sinZ = Math.Sin(rotation[2]);
        double xZ = (x * cosZ) - (y * sinZ);
        double yZ = (x * sinZ) + (y * cosZ);

        // Вращение вокруг оси Y
        double cosY = Math.Cos(rotation[1]);
        double sinY = Math.Sin(rotation[1]);
        double xY = (xZ * cosY) + (z * sinY);
        double zY = (z * cosY) - (xZ * sinY);

        // Вращение вокруг оси X
        double cosX = Math.Cos(rotation[0]);
        double sinX = Math.Sin(rotation[0]);
        double yX = (yZ * cosX) - (zY * sinX);
        double zX = (yZ * sinX) + (zY * cosX);

        // Возвращаем точку в центр
        return [xY + center[0], yX + center[1], zX + center[2]];
    }

    public static Torus GeneratePointsOnMajorCircle(Torus torus)
    {

        int pointsCount = Convert.ToInt32(2 * Math.PI * torus.MajorRadius / torus.MinorRadius) * 2;

        for (int i = 0; i < pointsCount; i++)
        {
            double angle = 2 * Math.PI / pointsCount * i;
            double x = torus.Center[0] + (torus.MajorRadius * Math.Cos(angle));
            double y = torus.Center[1] + (torus.MajorRadius * Math.Sin(angle));
            double z = torus.Center[2];

            double[] point = RotatePoint([x, y, z], torus.Center, torus.Rotation);
            torus.PointsOnMajorCircle.Add(point);
        }

        return torus;
    }
}