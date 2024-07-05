using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SimpleWebApi.Model;

namespace SimpleWebApi.IntegrationTests;
public class MoviesControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _customWebApplicationFactory;

    public MoviesControllerTests(CustomWebApplicationFactory customWebApplicationFactory)
    {
        _customWebApplicationFactory = customWebApplicationFactory;
    }

    [Fact]
    public async Task Test1Async()
    {
        var client = _customWebApplicationFactory.CreateClient();

        Movie[]? movies = await client.GetFromJsonAsync<Movie[]>("api/Movies");

        Assert.NotNull(movies);
        Assert.Equal(1, movies.Length);

    }
}
