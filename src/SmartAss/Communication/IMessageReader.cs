// <copyright file = "IMessageReader.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.IO;

namespace SmartAss.Communication;

public interface IMessageReader
{
    [Pure]
    IEnumerable<object> Read(TextReader reader);
}
