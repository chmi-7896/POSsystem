using System;
using System.Collections.Generic;
using System.Linq;
using CommanModule;

namespace DataBase
{
    public class InvoiceHeader
    {
        public string InvoiceNumber { get; set; }
        public string CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public IList<InvoiceDetail> InvoiceDetials { get; set; }

        //this mothod should optimize by using stringbuilder object
        public override string ToString()
        {
            string str2 = "";
             var str1 = string.Format("\nInvoice :{0} ,Customer :{1} inv Date: {2} \n",
                 InvoiceNumber,CustomerId,InvoiceDate);

            if (InvoiceDetials != null)
            {
                foreach (var item in InvoiceDetials)
                {
                  str2 += string.Format("ProductId :{0}, Quntity {1}, UoM:{2}, UnitPrice:{3}\n",
                      item.ProductId,item.Quantity,item.UnitOfMesure,item.UnitPrice);
                }
            }
            return str1 + str2;
        }
        /// <summary>
        /// make sure that is maching invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public bool IsCorrectInvoice(string invoiceNumber)
        {
            return this.InvoiceNumber.Equals(invoiceNumber);
        }

        /// <summary>
        /// update  invoiceHeder and then  delegate to  invoiceDetails and 
        /// update stock in invoiceDetails
        /// </summary>
        /// <param name="invoiceheader"></param>
        public void UpdateInvoiceHeader(InvoiceHeader invoiceheader,IStcokUpdate _IStcokUpdate)
        {
            // update invoiceHeder information
            this.CustomerId = invoiceheader.CustomerId;
            this.InvoiceDate = invoiceheader.InvoiceDate;
            //then move to invoiceDetails
            using (var currentInvoiceDetails = InvoiceDetials.GetEnumerator())
            using (var invoiceDetails = invoiceheader.InvoiceDetials.GetEnumerator())
            {
                // making sure that number of object in both invoiceDetails is same
                while (currentInvoiceDetails.MoveNext() && invoiceDetails.MoveNext())
                {
                    // call UpdateInvoiceDetail of invoiceDetails
                    currentInvoiceDetails.Current.
                        UpdateInvoiceDetail(invoiceDetails.Current, _IStcokUpdate);
                }
            }
            Console.WriteLine("\nInovice {0} is updated\n",invoiceheader.InvoiceNumber);
        }
        /// <summary>
        /// remove instance of invoiceDetail by giving invoiceDetails as parameter 
        /// </summary>
        /// <param name="invoiceDetail"></param>
        public void DeleteInvoiceItem(InvoiceDetail invoiceDetail)
        {
            InvoiceDetials.Remove(invoiceDetail);
        }

    }
}
