using System;

public class Photon
{
    private Vector position;

    private double phi;

    private double _mu;
    private double mu {
        get {
            return _mu;
        }
        set {
            _mu = value;
            theta = Math.Acos(mu);
        }
    }
    // The cached theta value of mu
    private double theta;

    private double tauMax;

    public Photon(double _tauMax)
    {
        position = new Vector(0, 0, 0);
        tauMax = _tauMax;
    }

    /// <summary>
    /// Simulate a photon in a simple atmosphere. 
    /// </summary>
    /// <returns>The mu value of the photon, when it escapes the atmosphere</returns>
    public double Simulate()
    {
        ApplyInitialScattering();

        while(true)
        {
            UpdatePosition();

            if(position.Z < 0)
            {
                // Re-emit photon
                position = new Vector(0, 0, 0);
                ApplyInitialScattering();
                continue;
            }
            else if(position.Z > 1)
            {
                // Photons has escaped from atmosphere
                return mu;
            }

            ApplyIsotropicScattering();
        }
    }

    private void ApplyInitialScattering()
    {
        double r1 = RNG.rand.NextDouble();
        double r2 = RNG.rand.NextDouble();

        mu = Math.Sqrt(r1);
        phi = 2 * Math.PI * r2;
    }

    private void ApplyIsotropicScattering()
    {
        double r1 = RNG.rand.NextDouble();
        double r2 = RNG.rand.NextDouble();

        mu = 2 * r1 - 1;
        phi = 2 * Math.PI * r2;
    }

    private void UpdatePosition()
    {
        // Get a random distance to the next scattering event.
        double r = RNG.rand.NextDouble();
        double tau = -Math.Log(r);
        double s = tau / tauMax;

        // Calculate the displacement of the photon
        double sinT = Math.Sin(theta);
        Vector displacement = new Vector(
            s * sinT * Math.Cos(phi),
            s * sinT * Math.Sin(phi),
            s * mu
        );

        position.Add(displacement);
    }
}

