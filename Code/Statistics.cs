using System;
using System.Collections.Generic;
using System.Linq;

public static class Statistics
{
    /// <summary>
    /// Calculates the standard deviation of a list of values.
    /// </summary>
    /// <param name="values"></param>
    /// <see cref="https://www.developer.com/microsoft/adding-standard-deviation-to-linq/"/>
    /// <returns></returns>
    public static double CalcStdDev(IEnumerable<int> values)
    {
        double stddev = 0;

        if (values.Count() > 0)
        {
            //Compute the Average
            double avg = values.Average();

            //Perform the Sum of (value-avg)^2
            double sum = values.Sum(d => Math.Pow(d - avg, 2));

            //Put it all together
            stddev = Math.Sqrt((sum) / values.Count() - 1);
        }

        return stddev;
    }
}
