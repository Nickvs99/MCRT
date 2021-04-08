using System.Windows.Forms.DataVisualization.Charting;

namespace MCRT
{
    class Program
    {
        static void Main(string[] args)
        {

            Simulator sim = new Simulator(Settings.nPhotons, Settings.tauMax, Settings.nMuCells, Settings.nZCells);
            sim.Run();

            Chart MCRTChart = Charts.CreateMCRTChart(sim);
            Charts.ShowSaveChart(MCRTChart, "MCRT.png");

            Chart MCRTJHKChart = Charts.CreateMCRTJHKChart(sim);
            Charts.ShowSaveChart(MCRTJHKChart, "MCRTJHK.png");

            Chart EddingtonFactorChart = Charts.CreateEddingtonFactorsChart(sim);
            Charts.ShowSaveChart(EddingtonFactorChart, "EddingtonFactor.png");
        }
    }
}
