namespace TorusGenSrv.Models;

public class Torus
{
    public required double[] Center { get; set; } 
    public double MajorRadius { get; set; }  // Радиус направляющей окружности
    public double MinorRadius { get; set; }  // Радиус образующей окружности
    public required double[] Rotation { get; set; } // Углы вращения (в радианах)
    public required List<double[]> PointsOnMajorCircle { get; set; }  // Точки на направляющей окружности
}