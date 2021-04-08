using System.Windows.Forms.DataVisualization.Charting;

namespace MCRT
{
    class Program
    {
        static void Main(string[] args)
        {

            Simulator sim = new Simulator(500000, 10, 50, 200);
            sim.Run();

            Chart MCRTChart = Charts.CreateMCRTChart(sim);
            Charts.ShowSaveChart(MCRTChart, "MCRT.png");

            Chart MCRTJHKChart = Charts.CreateMCRTJHKChart(sim);
            Charts.ShowSaveChart(MCRTJHKChart, "MCRTJHK.png");

        }
    }
}   
