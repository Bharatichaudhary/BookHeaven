using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace WebApplication4.data.Extensions
{
    public static class SessionExtensions
    {
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }

        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

    }
}
