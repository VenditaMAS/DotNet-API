using System;
namespace Vendita.MAS.Resources
{
    public interface IPagedResource: IResource
    {
        int PageSize { get; }
    }
}
