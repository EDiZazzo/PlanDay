using System.Text.RegularExpressions;
using Planday.Schedule.Infrastructure.Providers.Interfaces;

namespace Planday.Schedule.Infrastructure.Providers
{
    public class ExternalApiStringProvider : IExternalApiStringProvider
    {
        private readonly string _uri;
        private readonly string _key;

    
        public ExternalApiStringProvider(string connectionString, string key)
        {
            _uri = ConnectionStringProviderBase.ProcessConnectionString(connectionString);
            _key = key;
        }

        public string GetUri() => _uri;

        public string GetKey() => _key;
    }    
}

