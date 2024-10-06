using Newtonsoft.Json;
using TorusGenSrv.Generators;
using TorusGenSrv.Models;

namespace TorusGenSrv.Controllers;



public class Results
{

    public static string GenerateToriByNumber(int numberOfTori, double cubeEdge, double max_R, double min_R, double max_r, double min_r)
    {
        double CubeV = Math.Pow(cubeEdge, 3);
        double V = 0;
        double Nc = 0;
        int maxAttempts = 2000;
        int attempts = 0;
        int notGenerated = 0;

        List<Torus> toriList = [];
        Torus newTorus;

        bool flag;
        do
        {
            do
            {
                newTorus = TorusGenerator.GenerateTorus(cubeEdge, max_R, min_R, max_r, min_r);
                attempts++;
            }
            while (newTorus.MajorRadius < newTorus.MinorRadius);
            newTorus = Points.GeneratePointsOnMajorCircle(newTorus);


            flag = false;

            for (int i = 0; i < newTorus.PointsOnMajorCircle.Count; i++)
            {
                if (IntersectionChecks.IsSphereIntersectingWithCube(newTorus.PointsOnMajorCircle[i], newTorus.MinorRadius, cubeEdge))
                {
                    flag = true;
                    break;
                }
            }
        }
        while (flag && attempts < maxAttempts);

        if (attempts >= maxAttempts)
        {
            Console.WriteLine($"Не удалось сгенерировать 1 торов.");
            notGenerated = 1;
        }
        else
        {
            toriList.Add(newTorus);  // Добавляем тор в список, если пересечений нет
            attempts = 0;
            V += newTorus.MajorRadius * newTorus.MinorRadius * newTorus.MinorRadius * 2 * Math.PI * Math.PI;
        }

        // Генерация заданного количества торов
        for (int i = 1; i < numberOfTori; i++)
        {
            do
            {
                do
                {
                    newTorus = TorusGenerator.GenerateTorus(cubeEdge, max_R, min_R, max_r, min_r);
                    attempts++;
                }
                while (newTorus.MajorRadius < newTorus.MinorRadius);
                newTorus = Points.GeneratePointsOnMajorCircle(newTorus);
            }
            // Проверяем пересечение с существующими
            while (IntersectionChecks.IsTorusIntersecting(newTorus, toriList, cubeEdge) && attempts < maxAttempts);
            Nc = V / CubeV;
            // Если не удалось сгенерировать тор за отведенное количество попыток
            if ((attempts >= maxAttempts) | (Nc >= 40))
            {
                Console.WriteLine($"Не удалось сгенерировать {numberOfTori - (i + 1)} торов.");
                notGenerated = numberOfTori - (i + 1);
                break;
            }
            else
            {
                toriList.Add(newTorus);  // Добавляем тор в список, если пересечений нет
                attempts = 0;
                V += newTorus.MajorRadius * newTorus.MinorRadius * newTorus.MinorRadius * 2 * Math.PI * Math.PI;
            }
        }


        TorusGenerationResult result = new(cubeEdge, notGenerated, Nc, numberOfTori, toriList);

        return JsonConvert.SerializeObject(result, Formatting.Indented);
    }

    public static string GetNc(int numberOfTori, double cubeEdge, double max_R, double min_R, double max_r, double min_r)
    {
        double cubeV = Math.Pow(cubeEdge, 3);
        double V = 0;
        double Nc = 0;
        int notGenerated = numberOfTori;
        Torus newTorus;
        max_R -= (max_R - min_R) * 0.25;
        max_r -= (max_r - min_r) * 0.25;

        for (int i = 0; i < numberOfTori; i++)
        {
            do
            {
                newTorus = TorusGenerator.GenerateTorus(cubeEdge, max_R, min_R, max_r, min_r);
            }
            while (IntersectionChecks.IsTorusIntersectingWithCube(newTorus, cubeEdge) && (newTorus.MajorRadius < newTorus.MinorRadius));
            if ((V + (newTorus.MajorRadius * newTorus.MinorRadius * newTorus.MinorRadius * 2 * Math.PI * Math.PI)) / cubeV > 0.4)
            {
                break;
            }
            V += newTorus.MajorRadius * newTorus.MinorRadius * newTorus.MinorRadius * 2 * Math.PI * Math.PI;
            notGenerated -= 1;
        }

        Nc = Math.Round(V / cubeV * 100, 2);
        var result = new
        {
            notGenerated,
            Nc
        };

        return JsonConvert.SerializeObject(result);
    }


    public static string GetNumberOfTori(double Nc, double cubeEdge, double max_R, double min_R, double max_r, double min_r)
    {
        double cubeV = Math.Pow(cubeEdge, 3);
        double V = 0;
        int numberOfTori = 0;
        Torus newTorus;
        max_R -= (max_R - min_R) * 0.25;
        max_r -= (max_r - min_r) * 0.25;
        do
        {
            do
            {
                newTorus = TorusGenerator.GenerateTorus(cubeEdge, max_R, min_R, max_r, min_r);
            }
            while (IntersectionChecks.IsTorusIntersectingWithCube(newTorus, cubeEdge) && (newTorus.MajorRadius < newTorus.MinorRadius));
            if ((V + (newTorus.MajorRadius * newTorus.MinorRadius * newTorus.MinorRadius * 2 * Math.PI * Math.PI)) / cubeV > Nc / 100)
            {
                break;
            }
            V += newTorus.MajorRadius * newTorus.MinorRadius * newTorus.MinorRadius * 2 * Math.PI * Math.PI;
            numberOfTori += 1;
        }
        while (true);

        var result = new
        {
            numberOfTori,
            Nc
        };
        return JsonConvert.SerializeObject(result);
    }

    public class TorusGenerationResult(double cubeEdge, int notGenerated, double nc, int numberOfTori, List<Torus> toriList)
    {
        public double CubeEdge { get; set; } = cubeEdge;
        public int NotGenerated { get; set; } = notGenerated;
        public double Nc { get; set; } = Math.Round(nc * 100, 2);
        public int NumberOfTori { get; set; } = numberOfTori;
        public List<Torus> ToriList { get; set; } = toriList;
    }
}
