using System.Data;

namespace DapperPoc.Samples
{
    interface ISamples
    {
        /// <summary>
        /// Executes the specified database connection.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="args">The arguments.</param>
        void Execute(IDbConnection dbConnection, ILogger logger, object[] args = null);
    }
}
