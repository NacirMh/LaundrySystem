namespace LaundrySystem.Domain.Dtos.Action
{
    public class ActionDTO
    {

        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        public int CycleId { get; set; }
    }
}
