using System;

namespace CommanModule
{
    public interface IStcokUpdate
    {
        int UpdateStock(SouceDocType doctype,string productid,int quantity);
    }
}
