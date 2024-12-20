using LaundrySystem.Domain.Models;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.WebApi.Infrastructure.Data;

namespace LaundrySystem.WebApi.Infrastructure.Daos
{
    public class ConfigurationDao : IConfigurationDAO
    {
        private readonly AppDbContext _DbContext;
        public ConfigurationDao(AppDbContext dbCon)
        {
            _DbContext = dbCon;
        }
        public Owner? GetConfigurationById(int id)
        {
            var owner = _DbContext.Owners.FirstOrDefault(x => x.Id == id);
            if (owner is null)
            {
                return null;
            }
            return owner;
        }

        public List<Owner> GetAllConfigurations()
        {
            var configs = _DbContext.Owners.ToList();
            return configs;
        }


    }
}
