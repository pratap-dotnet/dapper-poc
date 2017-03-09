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
    }
}
