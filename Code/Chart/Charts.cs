using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms.DataVisualization.Charting;

public static class Charts
{
    /// <summary>
    /// Creates a Chart which displays intensity over the angle (in degrees) for various solutions.
    /// </summary>
    /// <param name="thetaRange">Range object of theta(in degrees)</param>
    /// <returns></returns>
    public static Chart CreateIntensityChart(Range<double> thetaRange)
    {
        Chart chart = CreateBaseChart();
        ChartArea chartArea = chart.ChartAreas.Add("ChartArea1");
        ApplyBaseFont(chartArea);

        chartArea.AxisX.Minimum = thetaRange.min;
        chartArea.AxisX.Maximum = thetaRange.max;
        chartArea.AxisX.Interval = 15;
        chartArea.AxisX.Title = "Theta (degrees)";

        chartArea.AxisY.IsStartedFromZero = false;
        chartArea.AxisY.Title = "I / H0";


        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.Chandrasekhar(thetaRange), "Chandrasekhar"));
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.MilneEddington(thetaRange), "Milne-Eddington"));

        return chart;
    }

    /// <summary>
    /// Creates a chart which displays the analytical solutions the mean intensity (J), flux (H), radiation pressure(K)
    /// </summary>
    /// <param name="tauRange">Range object of the optical depth</param>
    /// <returns></returns>
    public static Chart CreateJHKChart(Range<double> tauRange)
    {
        Chart chart = CreateBaseChart();
        ChartArea chartArea = chart.ChartAreas.Add("ChartArea1");
        ApplyBaseFont(chartArea);

        chartArea.AxisX.Minimum = tauRange.min;
        chartArea.AxisX.Maximum = tauRange.max;
        chartArea.AxisX.Interval = 2;
        chartArea.AxisX.Title = "Tau";

        chartArea.AxisY.Title = "f / H0";

        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.LinearData(3, 2, tauRange), "J"));
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.LinearData(0, 1, tauRange), "H"));
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.LinearData(1, 2.0 / 3.0, tauRange), "K"));

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
        Chart chart = CreateIntensityChart(new Range<double>(0.0, 90.0, 0.5));

        Series series = ChartSeries.SimpleErrorSeries(ChartData.MCRTMuData(sim), "MCRT");
        series.CustomProperties = "PointWidth=2";
        chart.Series.Add(series);

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
        Chart chart = CreateJHKChart(new Range<double>(0, sim.tauMax, 1.0));
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
        Chart chart = CreateBaseChart();
        ChartArea chartArea = chart.ChartAreas.Add("ChartArea1");
        ApplyBaseFont(chartArea);

        chartArea.AxisX.Minimum = 0;
        chartArea.AxisX.Maximum = sim.tauMax;
        chartArea.AxisX.Title = "Tau";

        chartArea.AxisY.IsStartedFromZero = false;
        chartArea.AxisY.Title = "EddingtonFactors";

        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.HOVerJAnalytic(new Range<double>(0, sim.tauMax, 0.1)), "H / J Analytic"));
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.KOVerJAnalytic(new Range<double>(0, sim.tauMax, 0.1)), "K / J Analytic"));
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.HOVerJNumeric(sim), "H / J Numeric"));
        chart.Series.Add(ChartSeries.SimpleLineSeries(ChartData.KOVerJNumeric(sim), "K / J Numeric"));

        return chart;
    }

    /// <summary>
    /// Creates a chart, which displays the avg and stddev of the number of times a photon package undergoes
    /// a scatter event before escaping the atmosphere.
    /// </summary>
    /// <param name="tauRange">The range of tau max we are interested in.</param>
    /// <returns></returns>
    public static Chart CreateScatterChart(Range<double> tauRange)
    {
        Chart chart = CreateBaseChart();
        ChartArea chartArea = chart.ChartAreas.Add("ChartArea1");
        ApplyBaseFont(chartArea);

        chartArea.AxisX.Minimum = Math.Min(0, tauRange.min);

        chartArea.AxisX.Title = "Tau Max";
        chartArea.AxisY.Title = "Scattering events";

        Series series = ChartSeries.SimpleErrorSeries(ChartData.ScatterResearch(tauRange), "Avg scattering");
        series.CustomProperties = "PointWidth=0.5";
        series.BorderWidth = 2;
        chart.Series.Add(series);

        return chart;
    }

    private static Chart CreateBaseChart()
    {
        Chart chart = new Chart
        {
            Size = new Size(1280, 640)
        };

        Legend legend = chart.Legends.Add("legend1");
        legend.Font = new Font("Trebuchet MS", 15f);

        return chart;
    }

    private static void ApplyBaseFont(ChartArea area)
    {
        Font titleFont = new Font("Trebuchet MS", 15f);
        Font labelFont = new Font("Trebuchet MS", 12f);

        area.AxisX.TitleFont = titleFont;
        area.AxisY.TitleFont = titleFont;

        area.AxisX.LabelStyle.Font = labelFont;
        area.AxisY.LabelStyle.Font = labelFont;
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
        string chartPath = path;
        chart.SaveImage(chartPath, ImageFormat.Png);
    }
}
