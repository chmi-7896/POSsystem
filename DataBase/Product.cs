using System;
using System.Collections.Generic;
using System.Linq;
using CommanModule;

namespace DataBase
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public UOM UoM { get; set; }
        public decimal UnitPrice { get; set; }
        public Location Location { get; set; }

        public override string ToString()
        {
          return string.Format("Product Id:{0}, Name:{1}, UoM:{2},"  +
                               " Unit Price:{3}, Location:{4}"
                               ,ProductId,ProductName,UoM,UnitPrice,Location);
        }
        /// <summary>
        /// make sure current object location is match against location provide as parameter
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool IsCorrectLocation(Location location)
        {
            return this.Location == location;
        }
    }
}
