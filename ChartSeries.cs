﻿using System;
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
    /// <param name="sim">A completed Simulator object</param>
    /// <returns></returns>
    public static Series MCRTSeries(Simulator sim)
    {
        Series series = new Series();
        series.LegendText = "MCRT";
        series.ChartType = SeriesChartType.ErrorBar;
        series.YValuesPerPoint = 3;
        series.CustomProperties = $"PointWidth=2";

        // Start at the center of the first cell
        double mu = sim.muCellWidth / 2.0;

        for(int i = 0; i < sim.muCells.Length; i++)
        {
            // Convert mu value to degrees
            double degree = Math.Acos(mu) * 180.0 / Math.PI;

            // Calculate the normalized intensity
            double IOverH0 = 2 * sim.muCells[i] / (mu * sim.muCellWidth * sim.nPhotons);

            double error = IOverH0 / (double) Math.Sqrt(sim.muCells[i]);
            series.Points.AddXY(degree,IOverH0, IOverH0 - error, IOverH0 + error);

            mu += sim.muCellWidth;
        }

        return series;
    }

    /// <summary>
    /// Creates a series of the mean intensity results.
    /// </summary>
    /// <param name="sim">A completed Simulator object</param>
    /// <returns></returns>
    public static Series MCRTJSeries(Simulator sim)
    {
        Series series = new Series();
        series.LegendText = "MCRT J";
        series.ChartType = SeriesChartType.Line;
        
        for(int i = 0; i < sim.jBoundaries.Length - 1; i++)
        {
            // y value is in the middle of the two boundaries and scaled to the number of photons.
            series.Points.AddXY(sim.taus[i], 0.5 * (sim.jBoundaries[i] + sim.jBoundaries[i + 1]) / (double) sim.nPhotons);
        }

        return series;
    }

    /// <summary>
    /// Creates a series of the flux.
    /// </summary>
    /// <param name="sim">A completed Simulator object</param>
    /// <returns></returns>
    public static Series MCRTHSeries(Simulator sim)
    {
        Series series = new Series();
        series.LegendText = "MCRT H";
        series.ChartType = SeriesChartType.Line;

        for (int i = 0; i < sim.hBoundaries.Length - 1; i++)
        {
            // y value is in the middle of the two boundaries and scaled to the number of photons.
            series.Points.AddXY(sim.taus[i], 0.5 * (sim.hBoundaries[i] + sim.hBoundaries[i + 1]) / (double)sim.nPhotons);
        }

        return series;
    }

    /// <summary>
    /// Creates a series of the radiation pressure.
    /// </summary>
    /// <param name="sim">A completed Simulator object</param>
    /// <returns></returns>
    public static Series MCRTKSeries(Simulator sim)
    {
        Series series = new Series();
        series.LegendText = "MCRT K";
        series.ChartType = SeriesChartType.Line;

        for (int i = 0; i < sim.kBoundaries.Length - 1; i++)
        {
            // y value is in the middle of the two boundaries and scaled to the number of photons.
            // TODO: The k values dont match the analytical solution. A added vertical -0.5 offset seems to make it work...           
            series.Points.AddXY(sim.taus[i], 0.5 * (sim.kBoundaries[i] + sim.kBoundaries[i + 1]) / (double)sim.nPhotons);
        }

        return series;
    }
}
