﻿using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Forms.DataVisualization.Charting;

public static class Charts
{
    /// <summary>
    /// Creates a Chart which displays intensity over the angle (in degrees) for various solutions.
    /// </summary>
    /// <param name="thetaMin">Minimum theta coodinate (in degrees)</param>
    /// <param name="thetaMax">Maximum theta coodinate (in degrees)</param>
    /// <param name="dTheta">Stepsize of theta (in degrees)</param>
    /// <returns></returns>
    public static Chart CreateIntensityChart(double thetaMin, double thetaMax, double dTheta = 0.01)
    {
        Chart chart = new Chart();
        chart.Size = new System.Drawing.Size(1280, 640);
        chart.Legends.Add("legend1");


        ChartArea chartArea = chart.ChartAreas.Add("ChartArea1");
        chartArea.AxisX.Minimum = thetaMin;
        chartArea.AxisX.Maximum = thetaMax;
        chartArea.AxisX.Interval = 15;

        chartArea.AxisX.Title = "Theta (degrees)";

        chartArea.AxisY.IsStartedFromZero = false;
        chartArea.AxisY.Title = "I / H0";

        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.Chandrasekhar(thetaMin, thetaMax, dTheta), "Chandrasekhar"));
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.MilneEddington(thetaMin, thetaMax, dTheta), "Milne-Eddington"));

        return chart;
    }

    /// <summary>
    /// Creates a chart which displays the analytical solutions the mean intensity (J), flux (H), radiation pressure(K)
    /// </summary>
    /// <param name="tauMin">Minimum optical depth</param>
    /// <param name="tauMax">Maximum optical depth</param>
    /// <param name="dTau">Stepsize of the optical depth</param>
    /// <returns></returns>
    public static Chart CreateJHKChart(double tauMin, double tauMax, double dTau = 0.1)
    {
        Chart chart = new Chart();
        chart.Size = new System.Drawing.Size(1280, 640);
        chart.Legends.Add("legend1");

        ChartArea chartArea = chart.ChartAreas.Add("ChartArea1");
        chartArea.AxisX.Minimum = tauMin;
        chartArea.AxisX.Maximum = tauMax;
        chartArea.AxisX.Interval = 2;
        chartArea.AxisX.Title = "Tau";

        chartArea.AxisY.Title = "f / H0";

        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.LinearData(3, 2, tauMin, tauMax, dTau), "J"));
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.LinearData(0, 1, tauMin, tauMax, dTau), "H"));
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.LinearData(1, 2.0 / 3.0, tauMin, tauMax, dTau), "K"));

        return chart;
    }

    /// <summary>
    /// Create a chart, which displays both the analytical solutions and the MCRT.
    /// </summary>
    /// <param name="sim">A completed Simulator object</param>
    /// <returns></returns>
    public static Chart CreateMCRTChart(Simulator sim)
    {
        // Start  with the analytical solution charts
        Chart chart = CreateIntensityChart(0, 90, 0.5);
        
        chart.Series.Add(ChartSeries.MCRTSeries(sim));
        return chart;
    }

    /// <summary>
    /// Creates a chart, which displays both the analytical solutions and the the MCRT results for
    /// the mean intensity, flux and radiation pressure.
    /// </summary>
    /// <param name="sim">A completed Simulator object</param>
    /// <returns></returns>
    public static Chart CreateMCRTJHKChart(Simulator sim)
    {
        Chart chart = CreateJHKChart(0, sim.tauMax, 1);
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.MCRTRadiatonMoments(sim, sim.jBoundaries), "MCRT J"));
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.MCRTRadiatonMoments(sim, sim.hBoundaries), "MCRT H"));
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.MCRTRadiatonMoments(sim, sim.kBoundaries), "MCRT K"));
        
        return chart;
    }

    /// <summary>
    /// Creates a chart of the EddingtonFactors.
    /// </summary>
    /// <param name="sim">A completed Simulator object</param>
    /// <returns></returns>
    public static Chart CreateEddingtonFactorsChart(Simulator sim)
    {
        Chart chart = new Chart();
        chart.Size = new System.Drawing.Size(1280, 640);
        chart.Legends.Add("legend1");

        ChartArea chartArea = chart.ChartAreas.Add("ChartArea1");
        
        chartArea.AxisX.Minimum = 0;
        chartArea.AxisX.Maximum = sim.tauMax;
        chartArea.AxisX.Title = "Tau";

        chartArea.AxisY.IsStartedFromZero = false;
        chartArea.AxisY.Title = "EddingtonFactors";

        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.HOVerJAnalytic(0, sim.tauMax, 0.1), "H / J Analytic"));
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.KOVerJAnalytic(0, sim.tauMax, 0.1), "K / J Analytic"));
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.HOVerJNumeric(sim), "H / J Numeric"));
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.KOVerJNumeric(sim), "K / J Numeric"));

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
