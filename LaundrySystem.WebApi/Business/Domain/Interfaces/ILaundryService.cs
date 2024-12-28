namespace LaundrySystem.WebApi.Business.Domain.Interfaces
{
    public interface ILaundryService
    {
        public decimal CalculateMonthIncomes(int laundryId);
        public decimal CalculateTodayIncomes(int laundryId);
        public decimal CalculateTotalIncomes(int laundryId);
    }
}
