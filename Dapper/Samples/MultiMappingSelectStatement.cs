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
        public void Execute(IDbConnection dbConnection, ILogger logger, object[] args = null)
        {
            Stopwatch watch = new Stopwatch();
            var query = @"SELECT * FROM Production.Product P INNER JOIN Production.ProductModel PM ON P.ProductModelID = PM.ProductModelID";
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
