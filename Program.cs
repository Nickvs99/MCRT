using System.Windows.Forms.DataVisualization.Charting;

namespace MCRT
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Chart chart = Charts.CreateIntensityChart(0, 90, 0.5);
            Charts.ShowSaveChart(chart, "Intensity.png");

            Chart chartJHK = Charts.CreateJHKChart(0, 10, 1);
            Charts.ShowSaveChart(chartJHK, "JHK.png");
            */
            Simulator sim = new Simulator(500000, 10, 20);
            sim.Run();

            Chart MCRTChart = Charts.CreateMCRTChart(sim);
            Charts.ShowSaveChart(MCRTChart, "MCRT.png");
        }
    }
}   
