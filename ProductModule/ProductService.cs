using System;
using System.Collections.Generic;
using System.Linq;
using DataBase;
using CommanModule;

namespace ProductModule
{
    public class ProductService
    {
        private IList<Product> _productList;
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="productList"></param>
        public ProductService(IList<Product> productList)
        {
            _productList = productList;
        }

        /// <summary>
        /// add new product to list 
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product)
        {
            _productList.Add(product);
        }

        /// <summary>
        /// delete product from list
        /// </summary>
        /// <param name="product"></param>
        public void DeleteProduct(Product product)
        {
            _productList.Remove(product);
        }
        /// <summary>
        /// Get all product by assending order of productid
        /// </summary>
        /// <returns></returns>
        public IList<Product> GetAllProduct()
        {
            return _productList
                .OrderBy(x=>x.ProductId)
                .ToList();
        }

        /// <summary>
        /// Get prouduct Detail by Product id 
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public Product GetProductByProductId(string productid)
        {
            return _productList
                .Where(x => x.ProductId.Equals(productid)).
                FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prodcutName"></param>
        /// <returns></returns>
        public Product GetProductByName(string prodcutName)
        {
            return _productList
                .Where(x => x.ProductName.Contains(prodcutName))
                .FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public Location GetProductLcation(string productid)
        {
            return GetProductByProductId(productid).Location; ;
        }

        /// <summary>
        /// return product based on the locaiton 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetProductByLocation(Location location)
        {
            var prodcutlist = new List<Product>();
            using (var products = _productList.GetEnumerator())
            {
                while (products.MoveNext())
                {
                    // delegating reposibility to product class
                    if (products.Current.IsCorrectLocation(location))
                    {
                        prodcutlist.Add(products.Current);
                    }
                }
            }
            return prodcutlist;
        }
    }
}
