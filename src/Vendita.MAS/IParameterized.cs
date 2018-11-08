using System;
using System.Collections.Generic;

namespace Vendita.MAS
{
    public interface IParameterized
    {
        IDictionary<string, string> Parameters { get; }
    }
}
