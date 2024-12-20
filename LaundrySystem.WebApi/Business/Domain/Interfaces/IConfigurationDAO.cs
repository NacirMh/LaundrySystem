using LaundrySystem.Domain.Models;
namespace LaundrySystem.WebApi.Business.Domain.Interfaces
{
    public interface IConfigurationDAO
    {
        public Owner? GetConfigurationById(int id);
        public List<Owner> GetAllConfigurations();
        
    }
}
