using System;
using System.Collections.Generic;
using System.Linq;
using DataBase;
using CommanModule;

namespace InvoiceModule
{
    public class InvoiceService
    {
        private  IList<InvoiceHeader> _invoiceHader;
        private IStcokUpdate _IStkUpdate;

        /// <summary>
        /// constructor which tack IList<InvoiceHeder>
        /// </summary>
        /// <param name="invoiceHader"></param>
        public InvoiceService(IStcokUpdate StcokUpdate)
        {
            this._invoiceHader = new List<InvoiceHeader>();
            this._IStkUpdate = StcokUpdate;
        }

        /// <summary>
        /// adding new invoice and update stock
        /// </summary>
        /// <param name="invoiceHeder"></param>
        public void AddInvoice(InvoiceHeader invoiceHeder)
        {
            _invoiceHader.Add(invoiceHeder);
                
            //update stock items
            foreach (var item in invoiceHeder.InvoiceDetials)
            {
                _IStkUpdate
                 .UpdateStock(SouceDocType.INVOICE, item.ProductId, item.Quantity);
            }
            Console.WriteLine("****** Add invoice {0} ******** \n"
                ,invoiceHeder.InvoiceNumber);
        }

        /// <summary>
        /// remove invoiceHeader and update stock
        /// </summary>
        /// <param name="invoiceHeder"></param>
        public void DeleteInvoice(InvoiceHeader invoiceHeder)
        {
            _invoiceHader.Remove(invoiceHeder);

            // update stock by calling UpdateStock
            foreach (var item in invoiceHeder.InvoiceDetials)
            {
                _IStkUpdate
                 .UpdateStock(SouceDocType.INVREMOVE, item.ProductId, item.Quantity);
            }
            Console.WriteLine("\n****** Delete invoice {0} ******** \n"
                , invoiceHeder.InvoiceNumber);
        }

        /// <summary>
        /// delete invoiceDetail line  by it giving invoice number 
        /// and product number as parameter and update stock
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <param name="productNumber"></param>
        public void DeleteInvoiceDetailByProduct(string invoiceNumber, string productNumber)
        {
            var invdetail = FindInvoiceDetail(invoiceNumber, productNumber);
            using (var invoiceDetails = _invoiceHader.GetEnumerator())
            {
                while (invoiceDetails.MoveNext())
                {
                    invoiceDetails.Current.DeleteInvoiceItem(invdetail);
                }
            }
            // update stock by calling UpdateStock
            _IStkUpdate.UpdateStock(SouceDocType.INVREMOVE, invdetail.ProductId,
                invdetail.Quantity);

            Console.WriteLine("\n** Delete {0} from invoice :{1}, Quantity:{2} ** \n"
                , invdetail.ProductId, invoiceNumber, invdetail.Quantity);
        }

        /// <summary>
        /// display invoice details by giving invoice number
        /// as parameter
        /// </summary>
        /// <param name="invoiceNumber"></param>
        public void DisplayInvoiceInformation(string invoiceNumber)
        {
            var invoice= _invoiceHader
                .Where(x => x.InvoiceNumber
                    .Equals(invoiceNumber))
                    .FirstOrDefault();

            Console.WriteLine(invoice.ToString());
        }

        /// <summary>
        /// update invoice header 
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <param name="invoiceheader"></param>
        public void UpdateInvoice(InvoiceHeader invoiceheader)
        {
            using (var invHeader = _invoiceHader.GetEnumerator())
            {
                while (invHeader.MoveNext())
                {    // making sure that invoice number is correct 
                    if (invHeader.Current.IsCorrectInvoice(invoiceheader.InvoiceNumber))
                    {      // call UpdateInvoiceHeader of InvoiceHaader
                        invHeader.Current.UpdateInvoiceHeader(invoiceheader,_IStkUpdate);
                    }
                }
            }
        }
        /// <summary>
        /// return the invoicedetail object when it given invoicenumber and 
        /// productid
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <param name="productid"></param>
        /// <returns>InvoiceDetail</returns>
        private  InvoiceDetail FindInvoiceDetail(string invoiceNumber, string productid)
        {
           return  _invoiceHader
                .Where(x => x.InvoiceNumber
                    .Equals(invoiceNumber))
                    .FirstOrDefault()
                    .InvoiceDetials
                    .Where(z => z.ProductId.Equals(productid))
                    .FirstOrDefault();
        }
        /// <summary>
        /// return the invoice amount of given invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns>decimal</returns>
        public decimal GetInvoiceAmount(string invoiceNumber)
        {
           return  _invoiceHader
               .Where(x => x.InvoiceNumber
                   .Equals(invoiceNumber))
                   .FirstOrDefault()
                   .InvoiceDetials
                   .Sum(x => x.Quantity * x.UnitPrice);
        }
        /// <summary>
        ///  Get number of specified items in the given invoice number
        /// </summary>
        /// <param name="invoicenumber"></param>
        /// <param name="productid"></param>
        /// <returns>int</returns>
        public int GetNumberOfProductInInvoice(string invoicenumber, string productid)
        {
           return  _invoiceHader
               .Where(x => x.InvoiceNumber
                   .Equals(invoicenumber))
                   .FirstOrDefault()
                     .InvoiceDetials
                     .Where(y => y.ProductId.Equals(productid))
                     .Sum(z => z.Quantity);
        }
        /// <summary>
        /// return all invoiceHeaders 
        /// </summary>
        /// <returns>IEnumerable<InvoiceHeader></returns>
        public IEnumerable<InvoiceHeader> GetAllInvoiceHeaders()
        {
            return _invoiceHader.ToList();
            
        }
    }
}
