﻿using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Forms.DataVisualization.Charting;

public static class Charts
{
    /// <summary>
    /// Create a Chart from a sine function.
    /// </summary>
    /// <param name="xMin">Minimum x coordinate</param>
    /// <param name="xMax">Maximum x coordinate</param>
    /// <param name="dx">Stepsize of x</param>
    /// <returns></returns>
    public static Chart CreateSineChart(double xMin, double xMax, double dx = 0.01)
    {
        Chart chart = new Chart();
        chart.Size = new System.Drawing.Size(640, 320);
        chart.ChartAreas.Add("ChartArea1");
        chart.Legends.Add("legend1");
        
        chart.Series.Add(ChartSeries.SineSeries(xMin, xMax, dx));

        return chart;
    }

    /// <summary>
    /// Creates a Chart which displays the Chandrasekhar's solution.
    /// </summary>
    /// <param name="thetaMin">Minimum theta coodinate (in degrees)</param>
    /// <param name="thetaMax">Maximum theta coodinate (in degrees)</param>
    /// <param name="dTheta">Stepsize of theta (in degrees)</param>
    /// <returns></returns>
    public static Chart CreateChandrasekharChart(double thetaMin, double thetaMax, double dTheta = 0.01)
    {
        Chart chart = new Chart();
        chart.Size = new System.Drawing.Size(1280, 640);
        chart.Legends.Add("legend1");


        ChartArea chartArea = chart.ChartAreas.Add("ChartArea1");
        chartArea.AxisX.Minimum = thetaMin;
        chartArea.AxisX.Maximum = thetaMax;
        chartArea.AxisX.Interval = 15;

        chartArea.AxisX.Title = "Theta(degrees)";

        chartArea.AxisY.IsStartedFromZero = false;
        chartArea.AxisY.Title = "I / H0";

        chart.Series.Add(ChartSeries.ChandrasekharSeries(thetaMin, thetaMax, dTheta));

        return chart;
    }

    /// <summary>
    /// Show and save a Chart object at a given path.
    /// </summary>
    /// <param name="chart"></param>
    /// <param name="path"></param>
    public static void ShowSaveChart(Chart chart, string path)
    {
        SaveChart(chart, path);
        Process.Start(path);
    }

    /// <summary>
    /// Save a Chart object at a given path.
    /// </summary>
    /// <param name="chart"></param>
    /// <param name="path"></param>
    public static void SaveChart(Chart chart, string path)
    {        
        // Save and open chart
        string chartPath = path;
        chart.SaveImage(chartPath, ImageFormat.Png);
    }
}
