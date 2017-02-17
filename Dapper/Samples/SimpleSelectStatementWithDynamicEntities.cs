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
        public void Execute(IDbConnection dbConnection, ILogger listBoxAdapter)
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
