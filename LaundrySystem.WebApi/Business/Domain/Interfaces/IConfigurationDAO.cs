using LaundrySystem.Domain.Models;
namespace LaundrySystem.WebApi.Business.Domain.Interfaces
{
    public interface IConfigurationDAO
    {
        public Owner? GetConfigurationById(string id);
        public List<Owner> GetAllConfigurations();
        
    }
}
