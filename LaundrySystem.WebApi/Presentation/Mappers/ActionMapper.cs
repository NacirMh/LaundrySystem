using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.Dtos.Action;

namespace LaundrySystem.WebApi.Presentation.Mappers
{
    public static class ActionMapper
    {

        public static ActionDTO ToActionDTO(this Actionn action)
        {
            return new ActionDTO
            {
                CycleId = action.CycleId,
                Id = action.Id,
                Date = action.Date,
            };
        }
    }
}
