using System.Data;

namespace DapperPoc.Samples
{
    interface ISamples
    {
        void Execute(IDbConnection dbConnection, ILogger logger);
    }
}
