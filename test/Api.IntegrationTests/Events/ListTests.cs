using System.Threading.Tasks;
using Eventshuffle.Application.Features.Events.Queries.GetAllEvents;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Eventshuffle.Api.IntegrationTests.Events
{
    public class ListTests
    {
        [Fact]
        public async Task ShouldListEvents()
        {
            var factory = new CustomWebApplicationFactory<Eventshuffle.Api.Startup>();
            var client = factory.CreateClient();

            var response = await client.GetAsync("/api/v1/events");

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EventListDto>(responseString);
            result.Events.Count.ShouldBe(2);
            result.Events.ShouldContain(e => e.Name == "Name1");
            result.Events.ShouldContain(e => e.Name == "Name2");
        }
    }
}
