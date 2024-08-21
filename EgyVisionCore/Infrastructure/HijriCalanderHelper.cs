using System.Collections.Generic;

namespace EgyVisionCore.Infrastructure
{
    public static class HijriCalanderHelper
    {
        public static Dictionary<string, string> getDays()
        {
            Dictionary<string, string> days = new Dictionary<string, string>();
            days.Add("يوم", "00");
            for (int i = 1; i < 31; i++)
            {
                days.Add(i.ToString().PadLeft(2, '0'), i.ToString().PadLeft(2, '0'));
            }
            return days;
        }
        public static Dictionary<string, string> getMonths()
        {
            Dictionary<string, string> days = new Dictionary<string, string>();
            days.Add("شهر", "00");
            days.Add("محرم", "01");
            days.Add("صفر", "02");
            days.Add("ربيع أول", "03");
            days.Add("ربيع ثان", "04");
            days.Add("جمادي أول", "05");
            days.Add("جمادي ثان", "06");
            days.Add("رجب", "07");
            days.Add("شعبان", "08");
            days.Add("رمضان", "09");
            days.Add("شوال", "10");
            days.Add("ذو القعده", "11");
            days.Add("ذو الحجة", "12");

            return days;
        }
        public static Dictionary<string, string> getYears()
        {
            Dictionary<string, string> days = new Dictionary<string, string>();
            days.Add("سنة","0000");
            for (int i = 1320; i < 1500; i++)
            {
                days.Add(i.ToString(), i.ToString());
            }
            return days;
        }
    }
}
