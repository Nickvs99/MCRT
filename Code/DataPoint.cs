public struct DataPoint
{
    public double x;
    public double y;
    public DataPoint(double _x, double _y)
    {
        x = _x;
        y = _y;
    }
}

public struct DataPointError
{
    public double x;
    public double y;
    public double yUpper;
    public double yLower;
    public DataPointError(double _x, double _y, double _yUpper, double _yLower)
    {
        x = _x;
        y = _y;
        yUpper = _yUpper;
        yLower = _yLower;
    }
}