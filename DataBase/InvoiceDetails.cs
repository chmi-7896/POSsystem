using System;
using System.Collections.Generic;
using System.Linq;
using CommanModule;

namespace DataBase
{
    public class InvoiceDetail 
    {
        private bool disposed = false;
        public string ProductId { get; set; }
        public UOM UnitOfMesure { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// update InvoiceDetail ( asume that same number of item 
        /// avialable after updating InvoiceDetail )
        /// </summary>
        /// <param name="invoiceDetail"></param>
        internal void UpdateInvoiceDetail(InvoiceDetail invoiceDetail, IStcokUpdate _IStcokUpdate)
        {
            // change the stock before update because these current object 
            // invoice item affected to stock
            _IStcokUpdate.UpdateStock(SouceDocType.INVREMOVE, this.ProductId, this.Quantity);

            this.ProductId = invoiceDetail.ProductId;
            this.UnitOfMesure = invoiceDetail.UnitOfMesure;
            this.Quantity = invoiceDetail.Quantity;
            this.UnitPrice = invoiceDetail.UnitPrice;

            //chages made to after update
            _IStcokUpdate.UpdateStock(SouceDocType.INVOICE, this.ProductId, this.Quantity);
        }
    }
}
