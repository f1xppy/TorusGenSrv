namespace TorusGenSrv.Models;

public class Torus
{
    public required double[] Center { get; set; }  // Координаты центра (x, y, z)
    public double MajorRadius { get; set; }  // Радиус направляющей окружности
    public double MinorRadius { get; set; }  // Радиус образующей окружности
    public required double[] Rotation { get; set; } // Углы вращения вокруг осей (в радианах)
    public required List<double[]> PointsOnMajorCircle { get; set; }  // Точки на направляющей окружности
}