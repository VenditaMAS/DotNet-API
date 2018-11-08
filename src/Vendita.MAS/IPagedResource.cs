using System;
namespace Vendita.MAS
{
    public interface IPagedResource: IResource
    {
        int PageSize { get; }
    }
}
