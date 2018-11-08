using System;
namespace Vendita.MAS
{
    public interface IResource
    {
        string Path { get; }
    }

    public class FormResource: IResource
    {
        public string Path { get; } = "form";
    }

    public class InvocationResource: IResource
    {
        public string Path { get; } = "invocation";
    }
}
