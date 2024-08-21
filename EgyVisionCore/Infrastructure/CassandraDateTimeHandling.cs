using System;
using System.Globalization;
using System.Text;

namespace EgyVisionCore.Infrastructure
{
    public interface ICassandraDateTimeHandling
    {
        // Methods
        string convertToCassandrDateTime(DateTime dateTime);
        string getDateTimeString(DateTime dt, bool utc);
    }

    public class CassandraDateTimeHandling : ICassandraDateTimeHandling
    {
        // Methods
        public virtual string convertToCassandrDateTime(DateTime dateTime)
        {
            GregorianCalendar calendar = new GregorianCalendar();
            StringBuilder builder = new StringBuilder();
            dateTime = dateTime.ToUniversalTime();
            builder.Append($"{calendar.GetYear(dateTime):d4}-{calendar.GetMonth(dateTime):d2}-{calendar.GetDayOfMonth(dateTime):d2} {calendar.GetHour(dateTime):d2}:{calendar.GetMinute(dateTime):d2}:{calendar.GetSecond(dateTime):d2}+{0:d2}{0:d2}");
            return builder.ToString();
        }

        public virtual string getDateTimeString(DateTime dt, bool utc)
        {
            GregorianCalendar calendar = new GregorianCalendar();
            if (utc && ((dt > DateTime.MinValue) && (dt < DateTime.MaxValue)))
            {
                dt = dt.ToUniversalTime();
            }
            return $"{calendar.GetYear(dt):d4}{calendar.GetMonth(dt):d2}{calendar.GetDayOfMonth(dt):d2}{calendar.GetHour(dt):d2}{calendar.GetMinute(dt):d2}{calendar.GetSecond(dt):d2}";
        }
    }
}




