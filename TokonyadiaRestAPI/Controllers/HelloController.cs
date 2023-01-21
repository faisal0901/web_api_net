using Microsoft.AspNetCore.Mvc;

namespace TokonyadiaRestAPI.Controllers;

[ApiController]
[Route("hello")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public string GetHello()
    {
        return "Hello World";
    }

    // path variable, biasanya untuk mencari data spesifik
    // /Hello/{id}
    [HttpGet("{id}")]
    public string GetHelloWithId(string id)
    {
        return $"Hello {id}";
    }

    // query params
    // query string ? -> filtering (page, filter, searching)
    // https://git.enigmacamp.com/batch-5-online?page=1
    // http://localhost:8080/query-param?name=rifqi&isActive=true
    [HttpGet("query-param")]
    public string GetHelloWithQueryParam([FromQuery] string name, [FromQuery] bool isActive)
    {
        return $"Hello {name}, Active: {isActive}";
    }

    [HttpGet("object")]
    public object GetHelloWithObject()
    {
        return new
        {
            Id = Guid.NewGuid(),
            Name = "Rifqi Ramadhan",
            IsActive = true
        };
    }

    [HttpGet("array")]
    public List<object> GetHelloWithArray()
    {
        return new List<object>
        {
            new
            {
                Id = Guid.NewGuid(),
                Name = "Rifqi Ramadhan",
                IsActive = true
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Fadhil Fadhlih",
                IsActive = true
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Royan Syihab",
                IsActive = true
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Faisal Satrio",
                IsActive = true
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Sarah Azfa",
                IsActive = true
            },
        };
    }

    [HttpPost]
    public object PostString([FromBody] object name)
    {
        return name;
    }
}