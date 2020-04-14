// <copyright file = "CountdownTimer.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Logging;
using System;
using System.Diagnostics;

namespace SmartAss
{
    /// <summary>A simplified version of a <see cref="Stopwatch"/>.</summary>
    /// <remarks>
    /// The countdown timer can not be stopped, nor be reset.
    /// It just allows to ensure that certain tasks don't take longer than planned.
    /// </remarks>
    public class CountdownTimer
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DateTime end;

        private CountdownTimer(DateTime dt) => end = dt;

        /// <summary>Time left before the countdown timer expires.</summary>
        public TimeSpan Left => end - DateTime.UtcNow;

        /// <summary>Returns true if the countdown timer counted down fully.</summary>
        public bool Expired
        {
            get
            {
                Logger.Action("CountdownTimer.Expired");
                return DateTime.UtcNow > end;
            }
        }

        /// <inheritdoc />
        public override string ToString() => $"{Left:G}";

        /// <summary>Creates a countdown timer that will expire after the specified duration.</summary>
        public static CountdownTimer Duration(TimeSpan duration)
        {
            return new CountdownTimer(DateTime.UtcNow.Add(duration));
        }
    }
}
