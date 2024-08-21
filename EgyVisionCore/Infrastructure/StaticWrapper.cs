using Newtonsoft.Json;
using System;

namespace EgyVisionCore.Infrastructure
{
    public interface IStaticWrapper
    {
        DateTime UTCNow();
        Guid newGuid();
        string JsonSerialize(object obj);
        T JsonDeserialize<T>(string json);
    }
    public class StaticWrapper : IStaticWrapper
    {
        public virtual DateTime UTCNow()
        {
            return DateTime.UtcNow;
        }

        public virtual Guid newGuid()
        {
            return Guid.NewGuid();
        }

        public virtual string JsonSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public virtual T JsonDeserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
