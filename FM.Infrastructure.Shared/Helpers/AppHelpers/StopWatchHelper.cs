using System;
using System.Diagnostics;

namespace FM.Infrastructure.Shared.Helpers.AppHelpers
{
    public class StopWatchHelper
    {
        public static string CalculateTime(Stopwatch stopWatch)
        {
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            return elapsedTime;
        }
    }
}