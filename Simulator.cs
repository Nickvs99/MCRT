using System;

public class Simulator
{
    private int nPhotons;
    private double tauMax;

    private int[] muCells;
    private int nMuCells;
    private double muCellWidth;

    public Simulator(int _nPhotons, double _tauMax, int _nMuCells)
    {
        nPhotons = _nPhotons;
        tauMax = _tauMax;

        nMuCells = _nMuCells;
        muCellWidth = 1.0 / nMuCells;

        muCells = new int[nMuCells];
    }

    public int[] Run()
    {
        // Simulate n photons
        for (int i = 0; i < nPhotons; i++)
        {
            Photon photon = new Photon(tauMax);
            double mu = photon.Simulate();

            // Map mu to the right box
            int index = (int)Math.Floor(mu / muCellWidth);
            muCells[index] += 1;
        }

        return muCells;
    }
}
