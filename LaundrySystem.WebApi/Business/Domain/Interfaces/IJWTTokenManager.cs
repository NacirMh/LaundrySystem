
using LaundrySystem.Domain.Models;

namespace LaundrySystem.WebApi.Business.Domain.Interfaces
{
    public interface IJWTTokenManager
    {
        public Owner? Authenticate(string name, string password);
        public bool verifyToken(string Token);
        public string NewToken(string name);
    }
}
