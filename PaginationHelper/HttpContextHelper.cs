﻿namespace ProductApi.PaginationHelper;

public static class HttpContextHelper
{
    private static IHttpContextAccessor? _accessor;
    public static void Configure(IHttpContextAccessor? accessor)
    {
        _accessor = accessor;
    }
    public static HttpContext? Current => _accessor?.HttpContext;
    public static void AddResponseHeader(string key, string value)
    {
        if (Current == null || Current.Response.Headers.Keys.Contains(key))
        {
            return;
        }
        Current.Response.Headers.Remove(key);
        Current.Response.Headers.Add("Access-Control-Expose-Headers", key);
        Current.Response.Headers.Add(key, value);
    }
}