using LaundrySystem.Domain.Models;

namespace LaundrySystem.WebApi.Business.Domain.Interfaces
{
    public interface ICycleDAO
    {
        public List<Cycle> GetAll();
        public Cycle GetById(int id);
    }
}
