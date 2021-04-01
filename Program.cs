using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Forms.DataVisualization.Charting;

namespace MCRT
{
    class Program
    {
        static void Main(string[] args)
        {
            Chart chart = new Chart();
            chart.Size = new System.Drawing.Size(640, 320);
            chart.ChartAreas.Add("ChartArea1");
            chart.Legends.Add("legend1");

            // Add Series with legend
            chart.Series.Add("sin");
            chart.Series["sin"].LegendText = "Sin(x)";
            chart.Series["sin"].ChartType = SeriesChartType.Spline;

            // Add datapoints
            for (double x = 0; x < 2 * Math.PI; x += 0.01)
            {
                chart.Series["sin"].Points.AddXY(x, Math.Sin(x));
            }

            // Save and open chart
            string chartPath = "sin_0_2pi.png";
            chart.SaveImage(chartPath, ImageFormat.Png);
            Process.Start(chartPath);
        }
    }
}   
