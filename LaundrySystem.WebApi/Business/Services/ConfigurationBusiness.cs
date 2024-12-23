using LaundrySystem.Domain.Models;
using LaundrySystem.WebApi.Business.Domain.Interfaces;

namespace LaundrySystem.WebApi.Business.Services
{
    public class ConfigurationBusiness : IConfigurationService
    {
        private readonly IConfigurationDAO _dao;
        public ConfigurationBusiness(IConfigurationDAO dao)
        {
            _dao = dao;
        }

        public Owner GetConfigurations(string id)
        {
            var Owners = _dao.GetConfigurationById(id);
            return Owners;
        }
    }
}
