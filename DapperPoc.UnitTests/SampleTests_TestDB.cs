using DapperPoc.Common;
using DapperPoc.Samples;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperPoc.UnitTests
{
    [TestFixture]
    class SampleTests_TestDB
    {
        SqlConnection sqlConnection;
        Person person = new Person()
        {
            PersonType = "EM",
            Suffix = "Dr",
            FirstName = "first Name",
            MiddleName = "middle Name",
            LastName = "last Name",
            Title = "title",
            AdditionalContactInfo = "contactInfo"
        };

        class BusinessEntity
        {
            public int BusinessEntityId { get; set; }
            public int RowGuid { get; set; }
            public int ModifiedDate { get; set; }
        }

        /// <summary>
        /// Samples the tests test database setup.
        /// </summary>
        [OneTimeSetUp]
        public void SampleTests_TestDB_Setup()
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServer"].ConnectionString);
        }

        /// <summary>
        /// Samples the tests test database tear down.
        /// </summary>
        [OneTimeTearDown]
        public void SampleTests_TestDB_TearDown()
        {
            sqlConnection.Dispose();
        }

        /// <summary>
        /// Tests the setup.
        /// </summary>
        [SetUp]
        public void TestSetup()
        {
            List<string> idsToDelete = GetBusinessEntitityIdsForPersonData(person);
            DeleteFromPersonTable(idsToDelete);
            DeleteFromBusinessEntityTable(idsToDelete);
        }

        /// <summary>
        /// Shoulds the execute simple insert statement.
        /// </summary>
        [Test]
        public void should_execute_simple_insert_statement()
        {
            var mockRepo = new MockRepository(MockBehavior.Default);
            var loggerMock = mockRepo.Create<ILogger>();

            loggerMock.Setup(a => a.WriteLine(It.IsAny<string>()))
                .Callback<string>(msg => Assert.IsTrue(
                            msg.Contains(@"INSERT Person(BusinessEntityId, PersonType, Suffix, FirstName, MiddleName, LastName, Title) 
                                                values(@pBusinessEntityId, @pType, @suffix, @fName, @mName, @lName, @title)") ||
                            msg.Contains(@"insert BusinessEntity (rowguid, ModifiedDate) Output Inserted.BusinessEntityId values(")
                                                ));

            var simpleInsertStatement = new SimpleInsertStatement();
            simpleInsertStatement.Execute(sqlConnection, loggerMock.Object, new object[] { person });
        }

        /// <summary>
        /// Gets the business entitity ids for person data.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        List<string> GetBusinessEntitityIdsForPersonData(Person person)
        {
            List<string> ids = new List<string>();
            string connectionString = ConfigurationManager.ConnectionStrings["sqlServerTest"].ConnectionString;
            string query = @"select BusinessEntityId from Person where " + 
                        "PersonType='" + person.PersonType + "' and " + 
                        "Suffix='" + person.Suffix + "' and " +
                        "FirstName='" + person.FirstName + "' and " +
                        "MiddleName='" + person.MiddleName + "' and " +
                        "LastName='" + person.LastName + "' and " +
                        "Title='" + person.Title + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ids.Add(reader["BusinessEntityId"].ToString());
                    }
                }
            }
            return ids;
        }

        /// <summary>
        /// Deletes from person table.
        /// </summary>
        /// <param name="ids">The ids.</param>
        void DeleteFromPersonTable(List<string> ids)
        {
            if (ids.Count == 0)
                return;

            string idList = string.Empty;
            foreach (string id in ids)
            {
                idList = idList + id + ",";
            }
            idList = idList.Remove(idList.Length - 1);

            string connectionString = ConfigurationManager.ConnectionStrings["sqlServerTest"].ConnectionString;
            string query = "delete from Person where BusinessEntityId IN (" + idList + ")";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletes from business entity table.
        /// </summary>
        /// <param name="ids">The ids.</param>
        void DeleteFromBusinessEntityTable(List<string> ids)
        {
            if (ids.Count == 0)
                return;

            string idList = string.Empty;
            foreach (string id in ids)
            {
                idList = idList + id + ",";
            }
            idList = idList.Remove(idList.Length - 1);

            string connectionString = ConfigurationManager.ConnectionStrings["sqlServerTest"].ConnectionString;
            string query = "delete from BusinessEntity where BusinessEntityId IN (" + idList + ")";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
