using System;
using System.Collections.Generic;
using System.Linq;
using CommanModule;

namespace DataBase
{
    public class Stock
    {
        public string ProductId { get; set; }
        public int Quntity { get; set; }
        public Location StkLocation { get; set; }
        public override string ToString()
        {
            return string.Format("Stock Item:{0}, Quantity:{1}, Location:{2}"
               , ProductId, Quntity,StkLocation);
        }
        /// <summary>
        /// making sure current object productid is match against productid 
        /// provide as parameter
        /// </summary>
        /// <param name="productid"></param>
        /// <returns>bool</returns>
        public bool IsCorrectProductId(string productid)
        {
            return this.ProductId == productid;
        }
        /// <summary>
        /// making sure that it can update stock without violating  specified 
        /// codition in each helper method.
        /// </summary>
        /// <param name="doctype"></param>
        /// <param name="tranquantity"></param>
        /// <returns>bool</returns>
        public bool CanUpdateValid(SouceDocType doctype, int tranquantity)
        {
            bool canupdate = false;

            switch (doctype)
            {
                case SouceDocType.INVOICE:
                    canupdate = ByInvoice(tranquantity);
                    break;
                case SouceDocType.GRNTOSTORSE:
                    canupdate = ByGrn(tranquantity);
                    break;
                case SouceDocType.GTNTOSUPPLIER:
                    canupdate = ByGtn(tranquantity);
                    break;
                case SouceDocType.INVREMOVE:
                    canupdate = ByInvoiceRemove(tranquantity);
                    break;
                default:
                    break;
            }
            return canupdate;
        }
        private bool ByInvoiceRemove(int tranquantity)
        {
            if (this.Quntity >= tranquantity)
            { 
                return true;
            }
            else {
                return false; 
            }
        }
        private bool ByGtn(int tranquantity)
        {
            if (this.Quntity >= tranquantity)
            {
                return true;
            }
            else {
                return false;
            }
        }

        private bool ByGrn(int tranquantity)
        {
            if (tranquantity > 0)
            {
                return true;
            }
            else {
                return false;
            }
        }
        private bool ByInvoice(int tranquantity)
        {
            if (this.Quntity >= tranquantity)
            {
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// update stock quntities based on soucedocument type
        /// </summary>
        /// <param name="doctype"></param>
        /// <param name="quantity"></param>
        /// <returns>int</returns>
        public int MakeUpdate(SouceDocType doctype, int quantity)
        {
            int updatequantity = 0;
            switch (doctype)
            {
                case SouceDocType.INVOICE:
                    this.Quntity -= quantity;
                    break;
                case SouceDocType.GRNTOSTORSE:
                    this.Quntity += quantity;
                    break;
                case SouceDocType.GTNTOSUPPLIER:
                    this.Quntity -= quantity;
                    break;
                case SouceDocType.INVREMOVE:
                    this.Quntity += quantity;
                    break;
                default:
                    break;
            }
            return updatequantity = quantity;
        }
    }
}
