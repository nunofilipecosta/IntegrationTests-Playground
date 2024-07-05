using System.Net.Http.Json;

namespace SimpleWebApi.IntegrationTests;

public class WeatherForecastControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _customWebApplicationFactory;

    public WeatherForecastControllerTests(CustomWebApplicationFactory customWebApplicationFactory)
    {
        _customWebApplicationFactory = customWebApplicationFactory;
    }


    [Fact]
    public async Task Test1Async()
    {
        var client = _customWebApplicationFactory.CreateClient();

        WeatherForecast[]? weatherForecasts = await client.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");

        Assert.NotNull(weatherForecasts);
        Assert.Equal(5, weatherForecasts.Length);

    }
}