namespace Planday.Schedule.Infrastructure.Providers.Interfaces
{
    public interface IExternalApiStringProvider
    {
        string GetUri();
        string GetKey();
    }    
}

