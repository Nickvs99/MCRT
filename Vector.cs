public struct Vector
{
    public double X;
    public double Y;
    public double Z;

    public Vector(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public void Add(Vector vector)
    {
        X += vector.X;
        Y += vector.Y;
        Z += vector.Z;
    }
}
