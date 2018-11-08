using System;
using System.Collections.Generic;

namespace Vendita.MAS.Requests
{
    /// <summary>
    /// An optional interface implemented by resource
    /// requests that provide query string parameters.
    /// </summary>
    public interface IParameterized
    {
        IDictionary<string, string> Parameters { get; }
    }
}
