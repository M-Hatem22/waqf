namespace EgyVisionCore.Infrastructure
{
    public static class StringExtentions
    {
        public static string ToCassandraReady(this string input)
        {
            string ret = input == null ? "null" : "'" + input.Replace("'","''") + "'";
            return ret;
        }
    }
}
