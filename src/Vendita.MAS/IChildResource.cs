using System;
namespace Vendita.MAS
{
    /// <summary>
    /// An optional interface implemented by requests
    /// that target a child resource. For example, in 
    /// <code>/mas/invocation/b4270df6-8648-4d5b-92b0-d53744a5c115/display</code>,
    /// The <code>display</code> at the end is a <code>ChildPath</code>.
    /// </summary>
    public interface IChildResource
    {
        string ChildPath { get; }
    }
}
