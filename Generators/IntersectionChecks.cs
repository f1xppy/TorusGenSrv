using TorusGenSrv.Models;

namespace TorusGenSrv.Generators;

public class IntersectionChecks
{

    // Проверка пересечения двух сфер по их центрам и радиусам
    public static bool AreSpheresIntersecting(double[] center1, double radius1, double[] center2, double radius2)
    {
        double distanceSquared = Math.Pow(center1[0] - center2[0], 2) +
                                 Math.Pow(center1[1] - center2[1], 2) +
                                 Math.Pow(center1[2] - center2[2], 2);
        double radiusSum = radius1 + radius2;

        return distanceSquared < Math.Pow(radiusSum, 2);
    }

    public static bool IsTorusIntersectingWithCube(Torus torus, double cubeEdge){
        double r = torus.MajorRadius + torus.MinorRadius;
        return !((r + torus.Center[0] <= cubeEdge / 2) &&
        (r + torus.Center[1] <= cubeEdge / 2) &&
        (r + torus.Center[2] <= cubeEdge / 2)); 
    }

    // Проверка пересечений между сферами нового тора и уже существующими
    public static bool IsTorusIntersecting(Torus newTorus, List<Torus> existingTori, double cubeEdge)
    {
        foreach (Torus existingTorus in existingTori)
        {
            for (int i = 0; i < newTorus.PointsOnMajorCircle.Count; i++)
            {
                if (IsSphereIntersectingWithCube(newTorus.PointsOnMajorCircle[i], newTorus.MinorRadius, cubeEdge))
                {
                    return true;  // Пересечение с кубом найдено
                }
                double distanceSquared = Math.Pow(newTorus.Center[0] - existingTorus.Center[0], 2) +
                                 Math.Pow(newTorus.Center[1] - existingTorus.Center[1], 2) +
                                 Math.Pow(newTorus.Center[2] - existingTorus.Center[2], 2);
                double radiusSumTori = newTorus.MajorRadius + newTorus.MinorRadius + existingTorus.MajorRadius + existingTorus.MinorRadius;
                if (distanceSquared < Math.Pow(radiusSumTori, 2))
                {
                    // Проверка пересечения сфер
                    for (int j = 0; j < existingTorus.PointsOnMajorCircle.Count; j++)
                    {

                        if (AreSpheresIntersecting(newTorus.PointsOnMajorCircle[i], newTorus.MinorRadius,
                                                   existingTorus.PointsOnMajorCircle[j], existingTorus.MinorRadius))
                        {
                            return true;  // Пересечение найдено
                        }
                    }
                }
            }
        }
        return false;  // Пересечений нет
    }

    // Проверка пересечения сферы с кубом
    public static bool IsSphereIntersectingWithCube(double[] sphereCenter, double sphereRadius, double cubeEdge)
    {
        double halfEdge = cubeEdge / 2;
        return !(Math.Abs(sphereCenter[0]) + sphereRadius <= halfEdge &&
               Math.Abs(sphereCenter[1]) + sphereRadius <= halfEdge &&
               Math.Abs(sphereCenter[2]) + sphereRadius <= halfEdge);
    }

}