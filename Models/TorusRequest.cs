namespace TorusGenSrv.Models;

public class TorusRequest
{
    public int NumberOfTori { get; set; }
    public double CubeEdge { get; set; }
    public double MaxMajorRadius { get; set; }
    public double MinMajorRadius { get; set; }
    public double MaxMinorRadius { get; set; }
    public double MinMinorRadius { get; set; }
    public double Nc { get; set; }
}