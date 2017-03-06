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
    class SimpleSelectStatementWithDynamicEntities : ISamples
    {
        /// <summary>
        /// Executes the specified database connection.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="listBoxAdapter">The list box adapter.</param>
        /// <param name="args">The arguments.</param>
        public void Execute(IDbConnection dbConnection, ILogger listBoxAdapter, object[] args = null)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var query = @"SELECT FirstName,MiddleName,LastName FROM Person.Person";
            IEnumerable<dynamic> customers = dbConnection.Query<dynamic>(query);
            watch.Stop();
            listBoxAdapter.WriteLine($"Query {query} executed in {watch.ElapsedMilliseconds} ms and returned {customers.Count()} objects");
        }
    }
}
