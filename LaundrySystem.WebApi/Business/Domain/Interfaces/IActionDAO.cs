using LaundrySystem.Domain.Models;

namespace LaundrySystem.WebApi.Business.Domain.Interfaces
{
    public interface IActionDAO
    {
        public Actionn CreateAction(Actionn action);
    }
}
