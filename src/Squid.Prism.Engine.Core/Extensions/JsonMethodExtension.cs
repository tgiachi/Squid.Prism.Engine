using System.Text.Json;
using Squid.Prism.Engine.Core.Utils;

namespace Squid.Prism.Engine.Core.Extensions;

public static class JsonMethodExtension
{
    public static string ToJson<T>(this T obj, bool formatted = true) =>
        JsonSerializer.Serialize(obj, JsonUtils.GetDefaultJsonSettings());

    public static T FromJson<T>(this string json) => JsonSerializer.Deserialize<T>(json, JsonUtils.GetDefaultJsonSettings());

    public static object FromJson(this string json, Type type) =>
        JsonSerializer.Deserialize(json, type, JsonUtils.GetDefaultJsonSettings());
}
