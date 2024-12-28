using LaundrySystem.WebApi.Business.Domain.Interfaces;

namespace LaundrySystem.WebApi.Business.Services
{
    public class LaundryManagement : ILaundryService
    {
        private readonly IMachineService _machineService;
        private readonly ILaundryDao _laundryDao;
        public LaundryManagement(IMachineService machineService ,ILaundryDao laundryDao)
        {
            _machineService = machineService;
            _laundryDao = laundryDao;
        }

        public decimal CalculateMonthIncomes(int laundryId)
        {
            var laundry = _laundryDao.GetById(laundryId);
            decimal result = 0; 
            foreach(var machine in laundry.Machines)
            {
                result += _machineService.CalculateMonthIncomes(machine.Id);
            }
            return result;
            
        }

        public decimal CalculateTodayIncomes(int laundryId)
        {
            var laundry = _laundryDao.GetById(laundryId);
            decimal result = 0;
            foreach (var machine in laundry.Machines)
            {
                result += _machineService.CalculateTodayIncomes(machine.Id);
            }
            return result;
        }

        public decimal CalculateTotalIncomes(int laundryId)
        {
            var laundry = _laundryDao.GetById(laundryId);
            decimal result = 0;
            foreach (var machine in laundry.Machines)
            {
                result += _machineService.CalculateTotalIncomes(machine.Id);
            }
            return result;
        }
    }
}
