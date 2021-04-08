using System;
using System.Collections.Generic;

/// <summary>
/// Class stores several functions which create data points. These datapoints are used in 
/// the Series objects.
/// </summary>
public static class ChartData
{

    public static List<DataPoint> Chandrasekhar(double thetaMin, double thetaMax, double dTheta = 0.01)
    {
        List<DataPoint> data = new List<DataPoint>();
        for (double theta = thetaMin; theta <= thetaMax; theta += dTheta)
        {
            // Map the angle in degrees to radians
            double mu = Math.Cos(theta * Math.PI / 180);

            // Chandrasekhar's solution
            double y = 3.0 * (mu + 0.70692 - 0.08392 / (1.0 + 4.45808 * mu) - 0.03619 / (1.0 + 1.59178 * mu) - 0.00946 / (1.0 + 1.10319 * mu));

            data.Add(new DataPoint(theta, y));
        }

        return data;
    }


    public static List<DataPoint> MilneEddington(double thetaMin, double thetaMax, double dTheta = 0.01)
    {
        List<DataPoint> data = new List<DataPoint>();
        for (double theta = thetaMin; theta <= thetaMax; theta += dTheta)
        {
            // Map the angle in degrees to radians
            double mu = Math.Cos(theta * Math.PI / 180);

            // Milne-Eddington's solution
            double y = 3.0 * mu + 2;

            data.Add(new DataPoint(theta, y));
        }

        return data;
    }

    public static List<DataPoint> LinearData(double a, double b, double xMin, double xMax, double dx = 0.01)
    {
        List<DataPoint> data = new List<DataPoint>();
        for (double x = xMin; x <= xMax; x += dx)
        {
            double y = a * x + b;

            data.Add(new DataPoint(x, y));
        }

        return data;
    }

    public static List<DataPointError> MCRTMuData(Simulator sim)
    {
        List<DataPointError> data = new List<DataPointError>();

        // Start at the center of the first cell
        double mu = sim.muCellWidth / 2.0;
        for (int i = 0; i < sim.muCells.Length; i++)
        {
            // Convert mu value to degrees
            double degree = Math.Acos(mu) * 180.0 / Math.PI;

            // Calculate the normalized intensity
            double IOverH0 = 2 * sim.muCells[i] / (mu * sim.muCellWidth * sim.nPhotons);

            double error = IOverH0 / (double)Math.Sqrt(sim.muCells[i]);

            data.Add(new DataPointError(degree, IOverH0, IOverH0 - error, IOverH0 + error));

            mu += sim.muCellWidth;
        }

        return data;
    }

    public static List<DataPoint> MCRTRadiatonMoments(Simulator sim, double[] boundaryValues)
    {
        List<DataPoint> data = new List<DataPoint>();
        for (int i = 0; i < sim.jBoundaries.Length - 1; i++)
        {
            double x = sim.taus[i];

            // y value is in the middle of the two boundaries and scaled to the number of photons.
            double y = 0.5 * (boundaryValues[i] + boundaryValues[i + 1]) / (double)sim.nPhotons;

            data.Add(new DataPoint(x, y));
        }

        return data;
    }

    public static List<DataPoint> HOVerJAnalytic(double thetaMin, double thetaMax, double dTheta = 0.1)
    {
        List<DataPoint> hData = LinearData(0, 1, thetaMin, thetaMax, dTheta);
        List<DataPoint> jData = LinearData(3, 2, thetaMin, thetaMax, dTheta);

        List<DataPoint> data = new List<DataPoint>();
        for (int i = 0; i < jData.Count; i++)
        {
            data.Add(new DataPoint(hData[i].x, hData[i].y / jData[i].y));
        }

        return data;
    }

    public static List<DataPoint> KOVerJAnalytic(double thetaMin, double thetaMax, double dTheta = 0.1)
    {
        List<DataPoint> kData = LinearData(1, 2.0 / 3.0, thetaMin, thetaMax, dTheta);
        List<DataPoint> jData = LinearData(3, 2, thetaMin, thetaMax, dTheta);

        List<DataPoint> data = new List<DataPoint>();
        for (int i = 0; i < jData.Count; i++)
        {
            data.Add(new DataPoint(kData[i].x, kData[i].y / jData[i].y));
        }

        return data;
    }

    public static List<DataPoint> KOVerJNumeric(Simulator sim)
    {
        List<DataPoint> kData = MCRTRadiatonMoments(sim, sim.kBoundaries);
        List<DataPoint> jData = MCRTRadiatonMoments(sim, sim.jBoundaries);

        List<DataPoint> data = new List<DataPoint>();
        for (int i = 0; i < jData.Count; i++)
        {
            data.Add(new DataPoint(kData[i].x, kData[i].y / jData[i].y));
        }

        return data;
    }

    public static List<DataPoint> HOVerJNumeric(Simulator sim)
    {
        List<DataPoint> hData = MCRTRadiatonMoments(sim, sim.hBoundaries);
        List<DataPoint> jData = MCRTRadiatonMoments(sim, sim.jBoundaries);

        List<DataPoint> data = new List<DataPoint>();
        for (int i = 0; i < jData.Count; i++)
        {
            data.Add(new DataPoint(hData[i].x, hData[i].y / jData[i].y));
        }

        return data;
    }
}

