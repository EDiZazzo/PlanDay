using System.Text.RegularExpressions;
using Planday.Schedule.Infrastructure.Providers.Interfaces;

namespace Planday.Schedule.Infrastructure.Providers
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly string _connectionString;
    
        public ConnectionStringProvider(string connectionString)
        {
            _connectionString = ConnectionStringProviderBase.ProcessConnectionString(connectionString);
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}

