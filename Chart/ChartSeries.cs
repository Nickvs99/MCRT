using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

public static class ChartSeries
{
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
    /// Creates a Series from a list of datapoints. This series is a simple line chart through those datapoints.
    /// </summary>
    /// <param name="data">The list of datapoints</param>
    /// <param name="label">The label for the series. Shows up in the legend.</param>
    /// <returns></returns>
    public static Series SimpleLineSeries(List<DataPoint> data, string label="Temporary")
    {
        Series series = new Series();
        series.LegendText = label;
        series.ChartType = SeriesChartType.Line;
        series.BorderWidth = 2;

        AddData(series, data);

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
