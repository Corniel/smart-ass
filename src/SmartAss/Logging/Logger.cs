// <copyright file = "Logger.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.IO;

namespace SmartAss.Logging;

public enum LogLevel
{
    Debug,
    Info,
    Warn,
    Error,
}

public static class Logger
{
    public static LogLevel Level { get; set; } = LogLevel.Error;

    public static void SetWriter(TextWriter writer) => Logger.writer = writer ?? Console.Error;

    private static TextWriter writer = Console.Error;

    [Conditional("DEBUG")]
    public static void Debug(string message, params object[] args)
    {
        if (Level <= LogLevel.Debug) { Write("DEBUG ", message, args); }
    }

    [Conditional("DEBUG")]
    public static void Info(string message, params object[] args)
    {
        if (Level <= LogLevel.Info) { Write("INFO  ", message, args); }
    }

    [Conditional("DEBUG")]
    public static void Warn(string message, params object[] args)
    {
        if (Level <= LogLevel.Warn) { Write("WARN  ", message, args); }
    }

    [Conditional("DEBUG")]
    public static void Error(string message, params object[] args)
    {
        if (Level <= LogLevel.Error) { Write("ERROR ", message, args); }
    }

    private static void Write(string prefix, string message, params object[] args)
    {
        if (args.Length == 0)
        {
            writer.WriteLine(prefix + message);
        }
        else
        {
            writer.WriteLine(prefix + message, args);
        }
    }

    [Conditional("DEBUG")]
    public static void Ctor<T>()
    {
        lock (CtorCalls)
        {
            if (CtorCalls.ContainsKey(typeof(T)))
            {
                CtorCalls[typeof(T)]++;
            }
            else
            {
                CtorCalls[typeof(T)] = 1;
            }
        }
    }

    [Conditional("DEBUG")]
    public static void Action(string action)
    {
        lock (ActionCalls)
        {
            if (ActionCalls.ContainsKey(action))
            {
                ActionCalls[action]++;
            }
            else
            {
                ActionCalls[action] = 1;
            }
        }
    }

    [Conditional("DEBUG")]
    public static void Heatmap(int[] heatmap, int index)
    {
        Guard.NotNull(heatmap, nameof(heatmap));
        heatmap[index]++;
    }

    public static readonly Dictionary<Type, int> CtorCalls = new Dictionary<Type, int>();
    public static readonly Dictionary<string, int> ActionCalls = new Dictionary<string, int>();
}
