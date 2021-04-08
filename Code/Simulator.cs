using System;

public class Simulator
{
    public int nPhotons;
    public double tauMax;

    public int[] muCells;

    public int nZCells;
    public int nMuCells;

    public double muCellWidth;
    public double zCellHeight;

    public double[] taus;

    // Store the radiation moments at the boundaries of a cell
    public double[] jBoundaries;
    public double[] hBoundaries;
    public double[] kBoundaries;

    public Simulator(int _nPhotons, double _tauMax, int _nMuCells, int _nZCells)
    {
        nPhotons = _nPhotons;
        tauMax = _tauMax;

        nMuCells = _nMuCells;
        muCellWidth = 1.0 / nMuCells;

        nZCells = _nZCells;
        zCellHeight = 1.0 / nZCells;

        muCells = new int[nMuCells];
        jBoundaries = new double[nZCells + 1];
        hBoundaries = new double[nZCells + 1];
        kBoundaries = new double[nZCells + 1];

        taus = new double[nZCells];
        taus[0] = tauMax - 0.5 * tauMax / nZCells;
        for (int i = 1; i < taus.Length; i++)
        {
            taus[i] = taus[i - 1] - tauMax / nZCells;
        }
    }

    public void Run()
    {
        // Simulate n photons
        for (int i = 0; i < nPhotons; i++)
        {
            Photon photon = new Photon(tauMax);

            while (!photon.isFinished)
            {
                UpdatePhoton(photon);

            }

            // Map mu to the right box
            int index = muMapper(photon.mu);
            muCells[index] += 1;

            if (i % 50000 == 0)
            {
                Console.Write($"\rCurrent photon package: {i} of {nPhotons}");
            }
        }
        Console.WriteLine();
    }

    private void UpdatePhoton(Photon photon)
    {
        double z0 = photon.position.Z;
        photon.UpdatePosition();
        double z1 = photon.position.Z;

        UpdateBoundaryValues(photon, z0, z1);

        photon.LateUpdate();
    }

    private void UpdateBoundaryValues(Photon photon, double z0, double z1)
    {
        // Map the old and new position to the right cell
        int j0 = zMapper(z0);
        int j1 = zMapper(z1);

        // Get the maximum and minumum cells, clamped to 0 and nZcells + 1
        int jMax = Math.Min(nZCells + 1, Math.Max(j0, j1));
        int jMin = Math.Max(0, Math.Min(j0, j1));

        // Update values for the radiaton moments at the boundary
        for (int j = jMin; j < jMax; j++)
        {
            double absMu = (double)Math.Abs(photon.mu);
            jBoundaries[j] += 1.0 / absMu;
            hBoundaries[j] += photon.mu / absMu;
            kBoundaries[j] += absMu;
        }
    }

    private int muMapper(double mu)
    {
        return (int)Math.Floor(mu / muCellWidth);
    }

    private int zMapper(double z)
    {
        return (int)Math.Floor(z / zCellHeight);
    }
}
