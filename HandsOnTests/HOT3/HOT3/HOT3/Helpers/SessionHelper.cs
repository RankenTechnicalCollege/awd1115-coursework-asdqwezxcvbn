﻿using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace HOT3.Helpers
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
            => session.SetString(key, JsonSerializer.Serialize(value));

        public static T? GetObjectFromJson<T>(this ISession session, string key)
            => session.GetString(key) is string json ? JsonSerializer.Deserialize<T>(json) : default;
    }
}