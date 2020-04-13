// <copyright file = "ConsolePlatform.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SmartAss.Communication
{
    public sealed class ConsolePlatform : IDisposable
    {
        public static void Run<TBot, TReader>()
            where TBot : new()
            where TReader : IMessageReader, new()
        {
            using (var platform = new ConsolePlatform(Console.In, Console.Out))
            {
                var bot = new TBot();
                var reader = new TReader();
                platform.Run(bot, reader);
            }
        }

        /// <summary>Constructs a console platform with Console.In and Console.Out.</summary>
        public ConsolePlatform() : this(Console.In, Console.Out) { }

        /// <summary>Constructs a console platform.</summary>
        public ConsolePlatform(TextReader reader, TextWriter writer)
        {
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
            Writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        /// <summary>The reader.</summary>
        public TextReader Reader { get; }
        /// <summary>The reader.</summary>
        public TextWriter Writer { get; }

        public void Run(object bot, IMessageReader reader)
        {
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
            if (ApplyMethods.TryGetValue(message.GetType(), out MethodInfo apply))
            {
                try
                {
                    return apply.Invoke(bot, new[] { message });
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
                ApplyMethods[method.GetParameters()[0].ParameterType] = method;
            }

        }
        private readonly Dictionary<Type, MethodInfo> ApplyMethods = new Dictionary<Type, MethodInfo>();

        /// <inheritdoc />
        public void Dispose()
        {
            if (!disposed)
            {
                Reader.Dispose();
                Writer.Dispose();
                disposed = true;
            }
            GC.SuppressFinalize(this);
        }
        private bool disposed;
    }
}
