using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DapperPoc.Common;
using DapperPoc.Samples;
using Moq;
using NUnit.Framework;

namespace DapperPoc.UnitTests
{
    [TestFixture]
    public class SampleTests
    {
        class Product
        {
            public Product() { }
            public Product(string ProductID, string ProductModelID)
            {
                this.ProductID = ProductID;
                this.ProductModelID = ProductModelID;
            }

            public string ProductID { get; set; }
            public string ProductModelID { get; set; }
        }

        class ProductModel
        {
            public ProductModel() { }
            public ProductModel(string ProductID, string ProductModelID)
            {
                this.ProductID = ProductID;
                this.ProductModelID = ProductModelID;
            }

            public string ProductID { get; set; }
            public string ProductModelID { get; set; }
        }

        class ProductCostHistory
        {
            public int ProductId { get; set; }
        }

        class ProductInventory
        {
            public int ProductId { get; set; }
        }

        /// <summary>
        /// Shoulds the execute multi mapping one to many relation select statement.
        /// </summary>
        [Test]
        public void should_execute_multi_mapping_one_to_many_relation_select_statement()
        {
            var inMemoryDatabase = new InMemoryDatabase();

            inMemoryDatabase.Insert<Product>(new List<Product>
            {
                new Product("987", "123")
            });

            inMemoryDatabase.Insert<ProductModel>(new List<ProductModel>
            {
                new ProductModel("987", "123")
            });

            var mockRepo = new MockRepository(MockBehavior.Default);
            var loggerMock = mockRepo.Create<ILogger>();

            loggerMock.Setup(a => a.WriteLine(It.IsAny<string>()))
                .Callback<string>(msg => Assert.IsTrue(
                    msg.Contains("SELECT * FROM ProductModel PM INNER JOIN Product P ON PM.ProductModelID = P.ProductModelID") ||
                    msg.Contains("Retrieved 1 ProductModel entities") ||
                    msg.Contains("The first Product Model with name  has 1 products")
                    ));

            var multiMappingOneToManyRelationSelectStatement = new MultiMappingOneToManyRelationSelectStatement();
            multiMappingOneToManyRelationSelectStatement.Execute(inMemoryDatabase.OpenConnection(), loggerMock.Object);
        }

        /// <summary>
        /// Shoulds the execute multi mapping select statement.
        /// </summary>
        [Test]
        public void should_execute_multi_mapping_select_statement()
        {
            var inMemoryDatabase = new InMemoryDatabase();

            inMemoryDatabase.Insert<Product>(new List<Product>
            {
                new Product("987", "123")
            });

            inMemoryDatabase.Insert<ProductModel>(new List<ProductModel>
            {
                new ProductModel("987", "123")
            });

            var mockRepo = new MockRepository(MockBehavior.Default);
            var loggerMock = mockRepo.Create<ILogger>();

            loggerMock.Setup(a => a.WriteLine(It.IsAny<string>()))
                .Callback<string>(msg => Assert.IsTrue(
                    msg.Contains("SELECT * FROM Product P INNER JOIN ProductModel PM ON P.ProductModelID = PM.ProductModelID") ||
                    msg.Contains("Retrieved 1 products") ||
                    msg.Contains("First Product with name  is model of type ")
                    ));

            var multiMappingSelectStatement = new MultiMappingSelectStatement();
            multiMappingSelectStatement.Execute(inMemoryDatabase.OpenConnection(), loggerMock.Object);
        }

        /// <summary>
        /// Shoulds the execute multiple queries statement.
        /// </summary>
        [Test]
        public void should_execute_multiple_queries_statement()
        {
            var inMemoryDatabase = new InMemoryDatabase();

            inMemoryDatabase.Insert<Product>(new List<Product>
            {
                new Product()
                {
                    ProductID = "707"
                }
            });

            inMemoryDatabase.Insert<ProductCostHistory>(new List<ProductCostHistory>
            {
                new ProductCostHistory()
            });

            inMemoryDatabase.Insert<ProductInventory>(new List<ProductInventory>
            {
                new ProductInventory()
            });

            var mockRepo = new MockRepository(MockBehavior.Default);
            var loggerMock = mockRepo.Create<ILogger>();

            loggerMock.Setup(a => a.WriteLine(It.IsAny<string>()))
                .Callback<string>(msg => Assert.IsTrue(
                        msg.Contains(@"SELECT * from Product where ProductId = @Id;") ||
                        msg.Contains(@"Retrived one product with id 707") ||
                        msg.Contains(@"Retrieved 0 history for that Product") ||
                        msg.Contains(@"Retrieved 0 inventory for that Product")
                    ));
            
            var multipleQueries = new MultipleQueries();
            multipleQueries.Execute(inMemoryDatabase.OpenConnection(), loggerMock.Object);
        }

        /// <summary>
        /// Shoulds the execute parameterized select statement.
        /// </summary>
        [Test]
        public void should_execute_parameterized_select_statement()
        {
            var inMemoryDatabase = new InMemoryDatabase();

            inMemoryDatabase.Insert<Product>(new List<Product>
            {
                new Product()
            });

            var mockRepo = new MockRepository(MockBehavior.Default);
            var loggerMock = mockRepo.Create<ILogger>();

            loggerMock.Setup(a => a.WriteLine(It.IsAny<string>()))
                .Callback<string>(msg => Assert.IsTrue(msg.Contains("SELECT * FROM Product where ProductId = @Id")));

            var parameterizedSelectStatement = new ParameterizedSelectStatement();
            parameterizedSelectStatement.Execute(inMemoryDatabase.OpenConnection(), loggerMock.Object);
        }

        /// <summary>
        /// Shoulds the execute simple select statement.
        /// </summary>
        [Test]
        public void should_execute_simple_select_statement()
        {
            var inMemoryDatabase = new InMemoryDatabase();

            inMemoryDatabase.Insert<Person>(new List<Person>
            {
                new Person()
            });

            var mockRepo = new MockRepository(MockBehavior.Default);
            var loggerMock = mockRepo.Create<ILogger>();

            loggerMock.Setup(a => a.WriteLine(It.IsAny<string>()))
                .Callback<string>(msg => Assert.IsTrue(msg.Contains("SELECT * FROM PERSON")));

            var simpleSelectStatement = new SimpleSelectStatement();
            simpleSelectStatement.Execute(inMemoryDatabase.OpenConnection(), loggerMock.Object);
        }

        /// <summary>
        /// Shoulds the execute simple select with dynamic entities statement.
        /// </summary>
        [Test]
        public void should_execute_simple_select_with_dynamic_entities_statement()
        {
            var inMemoryDatabase = new InMemoryDatabase();

            inMemoryDatabase.Insert<Person>(new List<Person>
            {
                new Person()
            });

            var mockRepo = new MockRepository(MockBehavior.Default);
            var loggerMock = mockRepo.Create<ILogger>();

            loggerMock.Setup(a => a.WriteLine(It.IsAny<string>()))
                .Callback<string>(msg => Assert.IsTrue(msg.Contains("SELECT FirstName,MiddleName,LastName FROM Person")));

            var simpleSelectStatementWithDynamicEntities = new SimpleSelectStatementWithDynamicEntities();
            simpleSelectStatementWithDynamicEntities.Execute(inMemoryDatabase.OpenConnection(), loggerMock.Object);
        }
    }
}
