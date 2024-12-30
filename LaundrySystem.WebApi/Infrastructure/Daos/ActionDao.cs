using LaundrySystem.Domain.Models;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.WebApi.Infrastructure.Data;

namespace LaundrySystem.WebApi.Infrastructure.Daos
{
    public class ActionDao : IActionDAO
    {
        private readonly AppDbContext _dbContext;

        public ActionDao(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Actionn CreateAction(Actionn action)
        {
            _dbContext.Actions.Add(action);
            _dbContext.SaveChanges();
            return action;
        }

    }
}
