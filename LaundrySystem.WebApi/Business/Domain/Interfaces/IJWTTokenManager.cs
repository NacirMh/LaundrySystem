
namespace LaundrySystem.WebApi.Business.Domain.Interfaces
{
    public interface IJWTTokenManager
    {
        public bool Authenticate(string id, string password);
        public bool verifyToken(string Token);
        public string NewToken(string name);
    }
}
