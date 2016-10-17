using System;
using System.Collections.Generic;
using System.Linq;
using DataBase;
using ProductModule;
using StockModule;
using InvoiceModule;

namespace TestAppliction
{
    class Program
    {
        static void Main(string[] args)
        {
            // creater product service 
          ProductService productService= CreateProductService();

           // GetProductByStoresLocation(productService); // this method is working 
          StockService stockService = CreateStockModule();
            
            //create invoice service 
          InvoiceService invoiceService = new InvoiceService(stockService);
          
          // add three invoices
          AddListOfInvoice(invoiceService);

            //display stock balance
          GetStockBalaceOfallProductOrderByLocation(stockService); // this method working 

          //delete invoice INV00001 from the system
          DeleteInvoiceFromSystem(invoiceService);

          //display stock balance
          GetStockBalaceOfallProductOrderByLocation(stockService);

          // delete product CC0009 from invoice INV00002
          invoiceService.DeleteInvoiceDetailByProduct("INV00002", "CC0009");

          // display detial of invoice number INV00002
          invoiceService.DisplayInvoiceInformation("INV00002");
          //display stock balance
          GetStockBalaceOfallProductOrderByLocation(stockService); // this method working 

          //update invoice 
          UpdateInvoice(invoiceService);
          //display stock balance
          GetStockBalaceOfallProductOrderByLocation(stockService); // this method working 

          TotalAmountOfEachInvoice(invoiceService);
        }

        private static void TotalAmountOfEachInvoice(InvoiceService invoiceService)
        {
            foreach (var item in invoiceService.GetAllInvoiceHeaders())
            {
                var amount = invoiceService.GetInvoiceAmount(item.InvoiceNumber);
             Console.WriteLine("\nTotal Amount of Inoice {0} is :{1}", "INV00003", amount);
            }
          
        }

        private static void UpdateInvoice(InvoiceService invoiceService)
        {

             invoiceService.UpdateInvoice(new InvoiceHeader()
            {
                InvoiceNumber = "INV00003",
                CustomerId = "DCC101",
                InvoiceDate = DateTime.Now,
                InvoiceDetials = new List<InvoiceDetail>
                {
                    new InvoiceDetail { ProductId="AA0012",Quantity=20,UnitOfMesure=UOM.Nos,UnitPrice=75000},
                    new InvoiceDetail {ProductId="RR8812",Quantity=40,UnitOfMesure=UOM.Nos,UnitPrice=7000},
                    new InvoiceDetail {ProductId="BB1234",Quantity=27,UnitOfMesure=UOM.Nos,UnitPrice=6500},
                }
            });
        }

        /// <summary>
        /// delete full invoice from the system
        /// </summary>
        /// <param name="invoiceService"></param>
        private static void DeleteInvoiceFromSystem(InvoiceService invoiceService)
        {

            invoiceService.DeleteInvoice(new InvoiceHeader()
            {
                InvoiceNumber = "INV00001",
                CustomerId = "CCC001",
                InvoiceDate = DateTime.Now,
                InvoiceDetials = new List<InvoiceDetail>
                {
                    new InvoiceDetail {ProductId="AA0012",Quantity=12,UnitOfMesure=UOM.Nos,UnitPrice=75000},
                    new InvoiceDetail {ProductId="AB0010",Quantity=6,UnitOfMesure=UOM.Nos,UnitPrice=15000},
                    new InvoiceDetail {ProductId="CC1111",Quantity=20,UnitOfMesure=UOM.Nos,UnitPrice=25000},
                }
            });
        }

        /// <summary>
        /// add three invoice into invoiceService using AddInvoice method
        /// </summary>
        /// <param name="invoiceService"></param>
        private static void AddListOfInvoice(InvoiceService invoiceService)
        {
            invoiceService.AddInvoice(new InvoiceHeader()
            {
                InvoiceNumber = "INV00001",
                CustomerId = "CCC001",
                InvoiceDate = DateTime.Now,
                InvoiceDetials = new List<InvoiceDetail>
                {
                    new InvoiceDetail {ProductId="AA0012",Quantity=12,UnitOfMesure=UOM.Nos,UnitPrice=75000},
                    new InvoiceDetail {ProductId="AB0010",Quantity=6,UnitOfMesure=UOM.Nos,UnitPrice=15000},
                    new InvoiceDetail {ProductId="CC1111",Quantity=20,UnitOfMesure=UOM.Nos,UnitPrice=25000},
                } });

            invoiceService.AddInvoice(new InvoiceHeader()
            {
                InvoiceNumber = "INV00002",
                CustomerId = "CCG111",
                InvoiceDate = DateTime.Now,
                InvoiceDetials = new List<InvoiceDetail>
                {
                    new InvoiceDetail { ProductId="CC0009",Quantity=33,UnitOfMesure=UOM.Nos,UnitPrice=55000},
                    new InvoiceDetail {ProductId="KK8888",Quantity=18,UnitOfMesure=UOM.Nos,UnitPrice=1500},
                    new InvoiceDetail {ProductId="CC1111",Quantity=25,UnitOfMesure=UOM.Nos,UnitPrice=25000},
                }
            });

            invoiceService.AddInvoice(new InvoiceHeader()
            {
                InvoiceNumber = "INV00003",
                CustomerId = "DCC101",
                InvoiceDate = DateTime.Now,
                InvoiceDetials = new List<InvoiceDetail>
                {
                    new InvoiceDetail { ProductId="AA0012",Quantity=12,UnitOfMesure=UOM.Nos,UnitPrice=75000},
                    new InvoiceDetail {ProductId="RR8812",Quantity=40,UnitOfMesure=UOM.Nos,UnitPrice=7000},
                    new InvoiceDetail {ProductId="BB1234",Quantity=27,UnitOfMesure=UOM.Nos,UnitPrice=6500},
                }
            });
        }

        /// <summary>
        /// Get the current Stock balance of each item
        /// </summary>
        /// <param name="stockService"></param>
        private static void GetStockBalaceOfallProductOrderByLocation(StockService stockService)
        {

            var stocks = stockService.GetStockBalances();
            foreach (var item in stocks)
            {
                Console.WriteLine(item.ToString());
            }
        }

        /// <summary>
        /// create stock service
        /// this provide opening balances to available stock items
        /// </summary>
        /// <returns></returns>
        private static StockService CreateStockModule()
        {

           return  new StockService(new List<Stock>
              {
                new Stock { ProductId="AA0012", Quntity=100,StkLocation=Location.GeneralStors }, 
                new Stock { ProductId="CC0009", Quntity=100,StkLocation=Location.GeneralStors }, 
                new Stock { ProductId="AB0010", Quntity=100,StkLocation=Location.MainStores }, 
                new Stock { ProductId="CC1111", Quntity=100,StkLocation=Location.MainStores }, 
                new Stock { ProductId="KK8888", Quntity=100,StkLocation=Location.GeneralStors},
                new Stock { ProductId="RR8812", Quntity=100,StkLocation=Location.MainStores},
                new Stock { ProductId="BB1234", Quntity=100,StkLocation=Location.GeneralStors} 
              }
                );
        }

        /// <summary>
        /// create product service
        /// this initialize product list by adding list of items
        /// </summary>
        private static ProductService CreateProductService()
        {
           return   new  ProductService(new List<Product> 
            { 
                new Product { ProductId="AA0012",ProductName="LAPTOP",UnitPrice=75000,UoM=UOM.Nos,Location=Location.GeneralStors }, 
                new Product { ProductId="CC0009",ProductName="DESKTOP",UnitPrice=55000,UoM=UOM.Nos,Location=Location.GeneralStors }, 
                new Product { ProductId="AB0010",ProductName="CRT MONITOR",UnitPrice=15000,UoM=UOM.Nos,Location=Location.MainStores }, 
                new Product { ProductId="CC1111",ProductName="LCD MONITOR",UnitPrice=25000,UoM=UOM.Nos,Location=Location.MainStores }, 
                new Product { ProductId="KK8888",ProductName="KEY BOARD",UnitPrice=1500,UoM=UOM.Nos,Location=Location.GeneralStors } ,
                new Product { ProductId="RR8812",ProductName="ROUTER",UnitPrice=7000,UoM=UOM.Nos,Location=Location.MainStores } ,
                new Product { ProductId="BB1234",ProductName="BACKPACK",UnitPrice=6500,UoM=UOM.Nos,Location=Location.GeneralStors } 
            });
        }

        /// <summary>
        /// Get all the product By store location 
        /// </summary>
        /// <param name="productService"></param>
        private static void GetProductByStoresLocation(ProductService productService)
        {
            var products = productService.GetProductByLocation(Location.GeneralStors);
            foreach (var item in products)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
