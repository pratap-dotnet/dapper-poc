using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Dapper;

namespace DapperPoc.Samples
{
    class MultiMappingSelectStatement : ISamples
    {
        /// <summary>
        /// Executes the specified database connection.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="args">The arguments.</param>
        public void Execute(IDbConnection dbConnection, ILogger logger, object[] args = null)
        {
            Stopwatch watch = new Stopwatch();
            var query = @"SELECT * FROM Product P INNER JOIN ProductModel PM ON P.ProductModelID = PM.ProductModelID";
            watch.Start();
            IEnumerable<Product> products = dbConnection.Query<Product, ProductModel, Product>(query,
                (p, pm) =>
                {
                    p.Model = pm;
                    return p;
                }, splitOn:"ProductModelId").ToList();
            watch.Stop();

            logger.WriteLine($"Executed query {query} in {watch.ElapsedMilliseconds} ms");
            logger.WriteLine($"Retrieved {products.Count()} products");
            logger.WriteLine($"First Product with name {products.First().Name} is model of type {products.First().Model.Name}");
        }

        class Product
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public string ProductNumber { get; set; }
            public string Color { get; set; }
            public string Size { get; set; }
            public DateTime SellStartDate { get; set; }
            public DateTime? SellEndDate { get; set; }
            public ProductModel Model { get; set; }
        }

        class ProductModel
        {
            public int ProductModelId { get; set; }
            public string Name { get; set; }
        }
    }
}
