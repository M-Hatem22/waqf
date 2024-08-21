using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EgyVisionCore.Infrastructure
{
    public static class JsonExtentions
    {
        public static string ToJson (this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static JObject fromJson(this string obj)
        {
            return JObject.Parse(obj);
        }

        public static T fromJson<T> (this string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
        
    }
}
