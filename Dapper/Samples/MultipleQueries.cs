using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DapperPoc.Samples
{
    class MultipleQueries : ISamples
    {
        class Product
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public string ProductNumber { get; set; }
            public string Color { get; set; }
            public string Size { get; set; }
            public DateTime SellStartDate { get; set; }
            public DateTime? SellEndDate { get; set; }
        }

        class ProductCostHistory
        {
            public int ProductId { get; set; }
            public double StandardCost { get; set; }
            public DateTime ModifiedDate { get; set; }
            public DateTime StartDate { get; set; }
        }

        class ProductInventory
        {
            public int ProductId { get; set; }
            public int LocationId { get; set; }
            public string Shelf { get; set; }
            public int Quantity { get; set; }
        }


        /// <summary>
        /// Executes the specified database connection.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="args">The arguments.</param>
        public void Execute(IDbConnection dbConnection, ILogger logger, object[] args = null)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var query = @"
                    SELECT * from Product where ProductId = @Id
                    SELECT * from ProductCostHistory where ProductId = @Id
                    SELECT * from ProductInventory where ProductId = @Id";

            using (var multiResults = dbConnection.QueryMultiple(query, new { Id = 707 }))
            {
                var product = multiResults.Read<Product>().Single();
                var costHistory = multiResults.Read<ProductCostHistory>();
                var inventories = multiResults.Read<ProductInventory>();
                watch.Stop();
                logger.WriteLine($"Query {query} executed in {watch.ElapsedMilliseconds} ms");
                logger.WriteLine($"Retrived one product with id {product.ProductId}");
                logger.WriteLine($"Retrieved {costHistory.Count()} history for that Product");
                logger.WriteLine($"Retrieved {inventories.Count()} inventory for that Product");
            }
        }
    }
}
