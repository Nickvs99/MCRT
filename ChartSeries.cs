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

    /// <summary>
    /// Creates a Series conform the formula y = ax + b
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="xMin"></param>
    /// <param name="xMax"></param>
    /// <param name="label"></param>
    /// <param name="dx"></param>
    /// <returns></returns>
    public static Series LinearSeries(double a, double b, double xMin, double xMax, string label, double dx=0.01)
    {
        Series series = new Series();
        series.LegendText = label;
        series.ChartType = SeriesChartType.Line;
        series.BorderWidth = 2;

        for (double x = xMin; x <= xMax; x += dx)
        {
            double y = a * x + b;

            series.Points.AddXY(x, y);
        }

        return series;
    }

    /// <summary>
    /// Creates a Series with the results of a MCRT simulation.
    /// </summary>
    /// <param name="nPhotons">The number of photons</param>
    /// <param name="tauMax">The maximum optical depth</param>
    /// <param name="nMuCells">The number of cells</param>
    /// <returns></returns>
    public static Series MCRTSeries(int nPhotons, double tauMax, int nMuCells)
    {
        Series series = new Series();
        series.LegendText = "MCRT";
        series.ChartType = SeriesChartType.Line;

        Simulator sim = new Simulator(nPhotons, tauMax, nMuCells);
        int[] muCells = sim.Run();

        double muCellWidth = 1.0 / nMuCells;

        // Start at the center of the first cell
        double mu = muCellWidth / 2.0;

        for(int i = 0; i < muCells.Length; i++)
        {
            // Convert mu value to degrees
            double degree = Math.Acos(mu) * 180.0 / Math.PI;

            // Calculate the normalized intensity
            double IOverH0 = 2 * muCells[i] / (mu * muCellWidth * nPhotons);

            series.Points.AddXY(degree, IOverH0);

            mu += muCellWidth;
        }

        return series;
    }
}
