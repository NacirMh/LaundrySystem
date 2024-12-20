using LaundrySystem.WebApi.Presentation.QueryObjects;

namespace LaundrySystem.WebApi.Presentation.Mappers
{
    public static class DateMapper
    {
        public static DateOnly ToDateOnlyFromDateQuery(this DateQuery dateQuery)
        {
            return new DateOnly(dateQuery.Year, dateQuery.Month, dateQuery.Day );
        }

    }
}
