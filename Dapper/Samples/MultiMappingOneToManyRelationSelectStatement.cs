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
    class MultiMappingOneToManyRelationSelectStatement : ISamples
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
            var query = @"SELECT * FROM Production.ProductModel PM INNER JOIN Production.Product P ON PM.ProductModelID = P.ProductModelID";
            var lookUp = new Dictionary<int, ProductModel>();
            watch.Start();
            dbConnection.Query<ProductModel, Product, ProductModel>(query,
                (pm, p) =>
                {
                    ProductModel model = null;
                    if (!lookUp.TryGetValue(pm.ProductModelId, out model))
                        lookUp.Add(pm.ProductModelId, model = pm);

                    if (model.Products == null)
                        model.Products = new List<Product>();
                    model.Products.Add(p);
                    return model;
                }, splitOn:"ProductModelId");
            watch.Stop();

            logger.WriteLine($"Executed {query} in {watch.ElapsedMilliseconds} ms");
            logger.WriteLine($"Retrieved {lookUp.Values.Count()} ProductModel entities");
            logger.WriteLine($"The first Product Model with name {lookUp.Values.First().Name} has {lookUp.Values.First().Products.Count} products");

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
        }

        class ProductModel
        {
            public int ProductModelId { get; set; }
            public string Name { get; set; }
            public IList<Product> Products { get; set; }
        }
    }
}
