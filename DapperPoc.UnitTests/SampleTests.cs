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
            //public double StandardCost { get; set; }
            //public DateTime ModifiedDate { get; set; }
            //public DateTime StartDate { get; set; }
        }

        class ProductInventory
        {
            public int ProductId { get; set; }
            //public int LocationId { get; set; }
            //public string Shelf { get; set; }
            //public int Quantity { get; set; }
        }

        class BusinessEntity
        {
            public int BusinessEntityId { get; set; }
            public int RowGuid { get; set; }
            public int ModifiedDate { get; set; }
        }

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

        [Test]
        public void should_execute_simple_insert_statement()
        {
            var inMemoryDatabase = new InMemoryDatabase();

            Person person = new Person()
            {
                PersonType = "EP",
                Suffix = "Dr",
                FirstName = "fName",
                MiddleName = "mName",
                LastName = "lName",
                Title = "title",
                AdditionalContactInfo = "contactInfo"
            };

            inMemoryDatabase.Insert<BusinessEntity>(new List<BusinessEntity>
            {
                new BusinessEntity()
            });

            var mockRepo = new MockRepository(MockBehavior.Default);
            var loggerMock = mockRepo.Create<ILogger>();

            loggerMock.Setup(a => a.WriteLine(It.IsAny<string>()))
                .Callback<string>(msg => Assert.IsTrue(msg.Contains(@"INSERT PERSON(BusinessEntityId, PersonType, Suffix, FirstName, MiddleName, LastName, Title) 
                                                values(@pBusinessEntityId, @pType, @suffix, @fName, @mName, @lName, @title)")));

            var simpleInsertStatement = new SimpleInsertStatement();
            simpleInsertStatement.Execute(inMemoryDatabase.OpenConnection(), loggerMock.Object, new object[] { person });
        }

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
