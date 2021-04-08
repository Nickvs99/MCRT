using System;
using System.Collections.Generic;
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

        AddData(series, ChartData.Chandrasekhar(thetaMin, thetaMax, dTheta));

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

        AddData(series, ChartData.MilneEddington(thetaMin, thetaMax, dTheta));

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

        AddData(series, ChartData.LinearData(a, b, xMin, xMax, dx));

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

        AddDataError(series, ChartData.MCRTMuData(sim));

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

        AddData(series, ChartData.MCRTRadiatonMoments(sim, sim.jBoundaries));

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

        AddData(series, ChartData.MCRTRadiatonMoments(sim, sim.hBoundaries));

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

        // TODO: The k values dont match the analytical solution. A vertical -0.5 offset seems to make it work...           
        AddData(series, ChartData.MCRTRadiatonMoments(sim, sim.jBoundaries));

        return series;
    }

    public static Series HOverJAnalyticSeries(double tauMin, double tauMax, double dTau = 0.01)
    {
        Series series = new Series();
        series.LegendText = "H / J analytic";
        series.ChartType = SeriesChartType.Line;
        series.BorderWidth = 2;

        AddData(series, ChartData.HOVerJAnalytic(tauMin, tauMax, dTau));
        return series;
    }

    public static Series KOverJAnalyticSeries(double tauMin, double tauMax, double dTau = 0.01)
    {
        Series series = new Series();
        series.LegendText = "K / J analytic";
        series.ChartType = SeriesChartType.Line;
        series.BorderWidth = 2;

        AddData(series, ChartData.KOVerJAnalytic(tauMin, tauMax, dTau));
        return series;
    }

    public static Series HOverJNumericSeries(Simulator sim)
    {
        Series series = new Series();
        series.LegendText = "H / J Numeric";
        series.ChartType = SeriesChartType.Line;
        series.BorderWidth = 2;

        AddData(series, ChartData.HOVerJNumeric(sim));
        return series;
    }

    public static Series KOverJNumericSeries(Simulator sim)
    {
        Series series = new Series();
        series.LegendText = "K / J Numeric";
        series.ChartType = SeriesChartType.Line;
        series.BorderWidth = 2;

        AddData(series, ChartData.KOVerJNumeric(sim));

        return series;
    }


    private static void AddData(Series series, List<DataPoint> data)
    {
        foreach (DataPoint dataPoint in data)
        {
            series.Points.AddXY(dataPoint.x, dataPoint.y);
        }

    }

    private static void AddDataError(Series series, List<DataPointError> data)
    {
        foreach (DataPointError dataPoint in data)
        {
            series.Points.AddXY(dataPoint.x, dataPoint.y, dataPoint.yLower, dataPoint.yUpper);
        }
    }
}
