using DapperPoc.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;

namespace DapperPoc.Samples
{
    internal class SimpleInsertStatement : ISamples
    {
        /// <summary>
        /// Executes the specified database connection.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="args">The arguments.</param>
        /// <exception cref="ArgumentException">Expected object type for parameter 'args' at index 0 is " + typeof(Person)</exception>
        public void Execute(IDbConnection dbConnection, ILogger logger, object[] args = null)
        {
            if(args[0] is Person == false)
                throw new ArgumentException("Expected object type for parameter 'args' at index 0 is " + typeof(Person));

            Person person = args[0] as Person;
            {
                Stopwatch watch1 = new Stopwatch();
                Stopwatch watch2 = new Stopwatch();
                string guid = System.Guid.NewGuid().ToString();

                watch1.Start();
                var queryInsertIntoBusinessEntity = @"insert BusinessEntity (rowguid, ModifiedDate) Output Inserted.BusinessEntityId values('" + guid + "', GETDATE())";
                var id = dbConnection.ExecuteScalar(queryInsertIntoBusinessEntity);
                watch1.Stop();

                Object o = new[] {
                    new {
                        pBusinessEntityId = id,
                        pType = person.PersonType,
                        suffix = person.Suffix,
                        fName = person.FirstName,
                        mName = person.MiddleName,
                        lName = person.LastName,
                        title = person.Title,
                        addContactInfo = person.AdditionalContactInfo
                    }
                };

                watch2.Start();
                var queryInsertIntoPerson = @"INSERT PERSON(BusinessEntityId, PersonType, Suffix, FirstName, MiddleName, LastName, Title) 
                                                values(@pBusinessEntityId, @pType, @suffix, @fName, @mName, @lName, @title)";
                var y = dbConnection.Execute(queryInsertIntoPerson, o);
                watch2.Stop();

                logger.WriteLine($"Query \"{queryInsertIntoBusinessEntity}\" executed in {watch1.ElapsedMilliseconds} ms and inserted one object");
                logger.WriteLine($"Query \"{queryInsertIntoPerson}\" executed in {watch2.ElapsedMilliseconds} ms and inserted one object of type {typeof(Person)}");
            }
        }
    }
}
