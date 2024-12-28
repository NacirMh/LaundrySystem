using LaundrySystem.Domain.Models;

namespace LaundrySystem.WebApi.Business.Domain.Interfaces
{
    public interface ILaundryDao
    {
        public List<Laundry> GetAll();
        public Laundry GetById(int id);
    }
}
