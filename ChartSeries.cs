using System;
using System.Windows.Forms.DataVisualization.Charting;

public static class ChartSeries
{
    /// <summary>
    /// Create a Series conform the sine function.
    /// </summary>
    /// <param name="xMin">Minimum x coordinate</param>
    /// <param name="xMax">Maximum x coordinate</param>
    /// <param name="dx">Stepsize of x</param>
    /// <returns></returns>
    public static Series SineSeries(double xMin, double xMax, double dx=0.01)
    {
        Series series = new Series();
        series.LegendText = "Sin(x)";
        series.ChartType = SeriesChartType.Line;

        for (double x = xMin; x <= xMax; x += dx)
        {
            series.Points.AddXY(x, Math.Sin(x));
        }

        return series;
    }

    /// <summary>
    /// Creates a Series conform the Chandrasekhar's solution.
    /// </summary>
    /// <param name="thetaMin">Minimum theta coodinate (in degrees)</param>
    /// <param name="thetaMax">Maximum theta coodinate (in degrees)</param>
    /// <param name="dTheta">Stepsize of theta (in degrees)</param>
    /// <returns></returns>
    public static Series ChandrasekharSeries(double thetaMin, double thetaMax, double dTheta = 0.01)
    {
        Series series = new Series();
        series.LegendText = "Chandrasekhar";
        series.ChartType = SeriesChartType.Line;
        series.BorderWidth = 2;
       

        for (double theta = thetaMin; theta <= thetaMax; theta += dTheta)
        {
            // Map the angle in degrees to radians
            double x = Math.Cos(theta * Math.PI / 180);

            // Chandrasekhar's solution
            double y = 3.0 * (x + 0.70692 - 0.08392 / (1.0 + 4.45808 * x) - 0.03619 / (1.0 + 1.59178 * x) - 0.00946 / (1.0 + 1.10319 * x));

            series.Points.AddXY(theta, y);
        }

        return series;
    }

    /// <summary>
    /// Creates a Series conform the Milne-Eddington's solution.
    /// </summary>
    /// <param name="thetaMin">Minimum theta coodinate (in degrees)</param>
    /// <param name="thetaMax">Maximum theta coodinate (in degrees)</param>
    /// <param name="dTheta">Stepsize of theta (in degrees)</param>
    /// <returns></returns>
    public static Series MilneEddingtonSeries(double thetaMin, double thetaMax, double dTheta = 0.01)
    {
        Series series = new Series();
        series.LegendText = "Milne-Eddington";
        series.ChartType = SeriesChartType.Line;
        series.BorderWidth = 2;

        for (double theta = thetaMin; theta <= thetaMax; theta += dTheta)
        {
            // Map the angle in degrees to radians
            double x = Math.Cos(theta * Math.PI / 180);

            // Milne-Eddington's solution
            double y = 3.0 * x + 2;

            series.Points.AddXY(theta, y);
        }

        return series;
    }
}
