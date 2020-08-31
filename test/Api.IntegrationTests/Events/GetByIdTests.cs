using System.Net.Http;
using System.Threading.Tasks;
using Eventshuffle.Application.Features.Events;
using Newtonsoft.Json;
using NodaTime;
using Shouldly;
using Xunit;

namespace Eventshuffle.Api.IntegrationTests.Events
{
    public class GetByIdTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetByIdTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ShouldGetEvent()
        {
            var response = await _client.GetAsync("/api/v1/events/1");

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EventDto>(responseString);
            result.Id.ShouldBe(1);
            result.Name.ShouldBe("Name1");
            result.Dates.ShouldHaveSingleItem();
            result.Dates.ShouldContain(date => date == new LocalDate(2000, 01, 01));
        }
    }
}
