using System;

public class Photon
{
    public Vector position;
    public bool isFinished;

    private double phi;

    private double _mu;
    public double mu {
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

        isFinished = false;

        ApplyInitialScattering();
    }

    public void LateUpdate()
    {
        if (position.Z < 0)
        {
            // Re-emit photon
            position = new Vector(0, 0, 0);
            ApplyInitialScattering();
        }
        else if (position.Z > 1)
        {
            // Photons has escaped from atmosphere
            isFinished = true;
        }
        else
        {
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

    public void UpdatePosition()
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

