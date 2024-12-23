using LaundrySystem.Domain.Models;

namespace LaundrySystem.WebApi.Business.Domain.Interfaces
{
    public interface IConfigurationService
    {
        public Owner GetConfigurations(string id);
    }
}
