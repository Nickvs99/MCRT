using System.Windows.Forms.DataVisualization.Charting;

namespace MCRT
{
    class Program
    {
        static void Main(string[] args)
        {
            Chart chart = Charts.CreateIntensityChart(0, 90, 0.5);
            Charts.ShowSaveChart(chart, "Intensity.png");
        }
    }
}   
