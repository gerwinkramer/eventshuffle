using System.Threading.Tasks;
using Eventshuffle.Application.Features.Events;
using Newtonsoft.Json;
using NodaTime;
using Shouldly;
using Xunit;

namespace Eventshuffle.Api.IntegrationTests.Events
{
    public class GetByIdTests
    {
        [Fact]
        public async Task ShouldGetEvent()
        {
            var factory = new CustomWebApplicationFactory<Eventshuffle.Api.Startup>();
            var client = factory.CreateClient();

            var response = await client.GetAsync("/api/v1/events/1");

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EventDto>(responseString);
            result.Id.ShouldBe(1);
            result.Name.ShouldBe("Name1");
            result.Dates.ShouldHaveSingleItem();
            result.Dates.ShouldContain(date => date == new LocalDate(2000, 01, 01));
        }
    }
}
