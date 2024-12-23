using LaundrySystem.Domain.Models;

namespace LaundrySystem.WebApi.Business.Domain.Interfaces
{
    public interface IOwnerDao
    {
        public Owner? Login(string id, string password);
    }
}
