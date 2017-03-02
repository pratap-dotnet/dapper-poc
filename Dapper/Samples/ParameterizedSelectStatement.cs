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
    class ParameterizedSelectStatement : ISamples
    {
        public void Execute(IDbConnection dbConnection, ILogger logger, object[] args = null)
        {
            Stopwatch watch = new Stopwatch();
            var query = @"SELECT * FROM Production.Product where ProductId = @Id";
            watch.Start();
            var dynamicEntity = dbConnection.Query<dynamic>(query, new { Id = 100 }).FirstOrDefault();
            watch.Stop();

            logger.WriteLine($"Executed {query} in {watch.ElapsedMilliseconds} ms");
        }
    }
}
