using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MinimalAPIs.Features
{
    public static class TestController 
    {
        public static void MapTestEndPoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("/minimal/test");
            endpoints.MapGet("/", () => "Hello World!");
            endpoints.MapPost("/", () => "Hello World!");
            endpoints.MapPut("/", () => "Hello World!");
            endpoints.MapGet("/test", () => async (int abc) => await Task.FromResult("Hello World!"));
        }
    }
}
