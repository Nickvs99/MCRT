using System;

/// <summary>
/// Class stores a random number generator.
/// This is done, since now the Random number generator will only be created once.
/// </summary>
public class RNG
{
    public static Random rand;
    static RNG()
    {
        rand = new Random();
    }
}

