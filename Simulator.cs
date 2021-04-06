using System;

public class Simulator
{
    public int nPhotons;
    public double tauMax;

    public int[] muCells;
    public int nMuCells;
    public double muCellWidth;

    public Simulator(int _nPhotons, double _tauMax, int _nMuCells)
    {
        nPhotons = _nPhotons;
        tauMax = _tauMax;

        nMuCells = _nMuCells;
        muCellWidth = 1.0 / nMuCells;

        muCells = new int[nMuCells];
    }

    public void Run()
    {
        // Simulate n photons
        for (int i = 0; i < nPhotons; i++)
        {
            Photon photon = new Photon(tauMax);
            double mu = photon.Simulate();

            // Map mu to the right box
            int index = (int)Math.Floor(mu / muCellWidth);
            muCells[index] += 1;

            if(i % 50000 == 0)
            {
                Console.Write($"\rCurrent photon package: {i} of {nPhotons}");
            }
        }
        Console.WriteLine();
    }
}
