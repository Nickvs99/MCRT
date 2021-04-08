public struct Range<T>
{
    public T min;
    public T max;
    public T step;
    public Range(T _minimum, T maximum, T stepSize)
    {
        min = _minimum;
        max = maximum;
        step = stepSize;
    }
}
