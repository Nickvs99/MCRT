﻿public static class Settings
{
    // The number of photon packages used.
    public static int nPhotons = 500000;

    // The maximum optical length of the atmosphere
    public static double tauMax = 10.0;

    // The number of mu cells
    public static int nMuCells = 50;

    // The number of z cells. Each cell represents 1 / n  of the atmosphere.
    public static int nZCells = 200;

    // The range of possible tauMax values we are interested for the number of scatter events.
    public static Range<double> scatterResearchRange = new Range<double>(2, 20, 2);
}
