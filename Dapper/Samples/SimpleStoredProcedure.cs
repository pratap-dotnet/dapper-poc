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
    class SimpleStoredProcedure : ISamples
    {
        public void Execute(IDbConnection dbConnection, ILogger logger, object[] args = null)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var results = dbConnection.Query<dynamic>("dbo.uspGetEmployeeManagers", new { BusinessEntityID = 50 },
                commandType: CommandType.StoredProcedure);
            watch.Stop();

            logger.WriteLine($"Stored procedure \"dbo.uspGetEmployeeManagers\" was executed in {watch.ElapsedMilliseconds} ms and {results.Count()} dynamic objects were retrieved");

        }
    }
}
