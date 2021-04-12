using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

public static class ChartSeries
{
    /// <summary>
    /// Creates a Series with the results of a MCRT simulation.
    /// </summary>
    /// <param name="data">The list of datapoints.</param>
    /// <returns></returns>
    public static Series SimpleErrorSeries(List<DataPointError> data, string label = "Temporary")
    {
        Series series = new Series
        {
            LegendText = label,
            ChartType = SeriesChartType.ErrorBar,
            YValuesPerPoint = 3,
        };

        AddDataError(series, data);

        return series;
    }

    /// <summary>
    /// Creates a Series from a list of datapoints. This series is a simple line chart through those datapoints.
    /// </summary>
    /// <param name="data">The list of datapoints</param>
    /// <param name="label">The label for the series. Shows up in the legend.</param>
    /// <returns></returns>
    public static Series SimpleLineSeries(List<DataPoint> data, string label = "Temporary")
    {
        Series series = new Series
        {
            LegendText = label,
            ChartType = SeriesChartType.Line,
            BorderWidth = 2
        };

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
