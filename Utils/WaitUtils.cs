using System;
using System.Threading;

namespace DingAssignment.Utils
{
    /// <summary>
    /// Centralized utility class to handle explicit orchestration or pacing adjustments uniformly.
    /// </summary>
    public static class WaitUtils
    {
        /// <summary>
        /// Freezes thread execution for a controlled baseline duration.
        /// Useful for satisfying strict environment synchronization requirements cleanly.
        /// </summary>
        /// <param name="seconds">The duration to pause in seconds.</param>
        public static void ForSeconds(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        /// <summary>
        /// Expressive shortcut for a common framework landing or processing delay.
        /// </summary>
        public static void ForPageSync()
        {
            ForSeconds(10);
        }

        /// <summary>
        /// Expressive shortcut for common micro UI interactions (e.g., button clicks, transitions).
        /// </summary>
        public static void ForActionSync()
        {
            ForSeconds(5);
        }
    }
}