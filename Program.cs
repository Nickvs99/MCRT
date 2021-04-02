using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace MCRT
{
    class Program
    {
        static void Main(string[] args)
        {
            Chart chart = Charts.CreateSineChart(0, 2 * Math.PI);
            Charts.ShowSaveChart(chart, "sin_0_2pi.png");
        }
    }
}   
