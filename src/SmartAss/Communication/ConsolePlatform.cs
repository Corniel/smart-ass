// <copyright file = "ConsolePlatform.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Logging;
using System.IO;
using System.Reflection;

namespace SmartAss.Communication;

public sealed class ConsolePlatform : IDisposable
{
    /// <summary>Initializes a new instance of the <see cref="ConsolePlatform"/> class.</summary>
    public ConsolePlatform()
        : this(Console.In, Console.Out) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="ConsolePlatform"/> class.</summary>
    public ConsolePlatform(TextReader reader, TextWriter writer)
    {
        Reader = reader ?? throw new ArgumentNullException(nameof(reader));
        Writer = writer ?? throw new ArgumentNullException(nameof(writer));
    }

    public static void Run<TBot, TReader>()
        where TBot : new()
        where TReader : IMessageReader, new()
    {
        using var platform = new ConsolePlatform(Console.In, Console.Out);

        var bot = new TBot();
        var reader = new TReader();
        platform.Run(bot, reader);
    }

    /// <summary>Gets the reader.</summary>
    public TextReader Reader { get; }

    /// <summary>Gets the writer.</summary>
    public TextWriter Writer { get; }

    public void Run(object bot, IMessageReader reader)
    {
        Guard.NotNull(bot, nameof(bot));
        Guard.NotNull(reader, nameof(reader));

        Initialize(bot.GetType());
        foreach (var message in reader.Read(Reader))
        {
            var response = Process(bot, message);
            if (response != null)
            {
                Logger.Info("Response: {0}", response);
                Writer.WriteLine(response.ToString());
            }
        }
    }

    private object Process(object bot, object message)
    {
        if (applyMethods.TryGetValue(message.GetType(), out MethodInfo apply))
        {
            try
            {
                return apply.Invoke(bot, [message]);
            }
            catch (TargetInvocationException x)
            {
                throw x.InnerException;
            }
        }

        Logger.Info($"{message.GetType()} is not handled by the bot.");
        return null;
    }

    private void Initialize(Type type)
    {
        var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(m => m.Name == "Apply" && m.GetParameters().Length == 1);

        foreach (var method in methods)
        {
            applyMethods[method.GetParameters()[0].ParameterType] = method;
        }
    }

    private readonly Dictionary<Type, MethodInfo> applyMethods = new Dictionary<Type, MethodInfo>();

    /// <inheritdoc />
    public void Dispose()
    {
        if (!disposed)
        {
            Reader.Dispose();
            Writer.Dispose();
            disposed = true;
        }
    }

    private bool disposed;
}
