using System;
using System.Collections.Generic;
using System.Linq;
using DataBase;
using CommanModule;

namespace StockModule
{
    public  class StockService : IStcokUpdate
    {
        private IList<Stock> _stock;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="stock"></param>
        public StockService(IList<Stock> stock)
        {
            this._stock = stock;
        }
        /// <summary>
        /// Get Stock balance of All stock item available in stock 
        /// including zeero blanaces
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Stock> GetStockBalances()
        {
            return _stock.OrderBy(x => x.StkLocation).ToList();
        }
        /// <summary>
        /// display stock balance by giving productid
        /// </summary>
        /// <param name="productid"></param>
        public void DisplayStockBalance(string productid)
        {
            var stockitem = _stock.Where(x => x.ProductId.Equals(productid))
                                .FirstOrDefault();

            Console.WriteLine(stockitem.ToString());
        }
        /// <summary>
        /// Get the stock blance of the product by giving parameter as productid
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public Stock GetStockByProductId(string productid)
        {
          return   _stock.Where(x => x.ProductId.Equals(productid)).FirstOrDefault();
        }

        #region IStcokUpdate Members

        /// <summary>
        /// Update stock when add /delete and update trasaction happend 
        /// in the system
        /// </summary>
        /// <param name="doctype"></param>
        /// <param name="productid"></param>
        /// <param name="quantity"></param>
        /// <returns>int</returns>
        public int UpdateStock(SouceDocType doctype, string productid, int quantity)
        {
            int updatedQty = 0;
            using (var stockItems = _stock.GetEnumerator())
            {
                while (stockItems.MoveNext())
                {
                    // making sure that product is correct available and apdate is valid
                    //one based on the soucedocument 
                    if ( stockItems.Current.IsCorrectProductId(productid) 
                        && stockItems.Current.CanUpdateValid(doctype,quantity))
                    {
                        // update stock based on the souce document type
                     updatedQty +=  stockItems.Current.MakeUpdate(doctype,quantity);
                    }
                }
                
            }
            return updatedQty;
        }

        #endregion
    }
}
