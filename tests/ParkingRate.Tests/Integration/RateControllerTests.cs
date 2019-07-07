using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkingRate.Core.Models;
using ParkingRate.Web;
using Xunit;

namespace ParkingRate.Tests.Integration
{
	public class RateControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
	{
		public RateControllerTests(CustomWebApplicationFactory<Startup> factory)
		{
			_client = factory.CreateClient();
		}

		private readonly HttpClient _client;

		private const string StandardRateName = "Standard Rate";
		private const string EarlyBirdRateName = "Early Bird";
		private const string WeekendRateName = "Weekend Rate";
		private const string NightRateName = "Night Rate";

		private async Task<Ticket> StartRequest(DateTime entry, DateTime exit)
		{
			var response = await _client.GetAsync($"/api/rate/{entry:O}/{exit:O}");

			response.EnsureSuccessStatusCode();
			var stringResponse = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<Ticket>(stringResponse);
			return result;
		}

		[Fact]
		public async Task ReturnsBadRequest()
		{
			var response =
				await _client.GetAsync(
					$"/api/rate/{new DateTime(2019, 07, 04, 12, 00, 00):O}/{new DateTime(2019, 07, 03, 16, 00, 00):O}");

			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
			var stringResponse = await response.Content.ReadAsStringAsync();
			Assert.Equal("Exit date must be before entry date", stringResponse);
		}

		[Fact]
		public async Task ReturnsDefaultFareFor1Day()
		{
			var result = await StartRequest(new DateTime(2019, 07, 07, 08, 00, 00),
				new DateTime(2019, 07, 08, 06, 00, 00));

			Assert.Equal(StandardRateName, result.RateName);
			Assert.Equal(20.00m, result.Amount);
		}

		[Fact]
		public async Task ReturnsDefaultFareFor1FullDay()
		{
			var result = await StartRequest(new DateTime(2019, 07, 04, 08, 00, 00),
				new DateTime(2019, 07, 05, 08, 00, 00));

			Assert.Equal(StandardRateName, result.RateName);
			Assert.Equal(20.00m, result.Amount);
		}

		[Fact]
		public async Task ReturnsDefaultFareFor4Days()
		{
			var result = await StartRequest(new DateTime(2019, 07, 04, 08, 00, 00),
				new DateTime(2019, 07, 08, 06, 00, 00));

			Assert.Equal(StandardRateName, result.RateName);
			Assert.Equal(80.00m, result.Amount);
		}

		[Fact]
		public async Task ReturnsDefaultFareFor4HoursWeekDay()
		{
			var result = await StartRequest(new DateTime(2019, 07, 03, 12, 00, 00),
				new DateTime(2019, 07, 03, 16, 00, 00));

			Assert.Equal(StandardRateName, result.RateName);
			Assert.Equal(20.00m, result.Amount);
		}

		[Fact]
		public async Task ReturnsDefaultFareWithNightFareButMultipleDays()
		{
			var result = await StartRequest(new DateTime(2019, 07, 03, 17, 00, 00),
				new DateTime(2019, 07, 05, 16, 30, 00));

			Assert.Equal(StandardRateName, result.RateName);
			Assert.Equal(40.00m, result.Amount);
		}

		[Fact]
		public async Task ReturnsDefaultStandardFareWithEarlyBirdIn2Days()
		{
			var result = await StartRequest(new DateTime(2019, 07, 03, 08, 30, 23),
				new DateTime(2019, 07, 04, 17, 30, 23));

			Assert.Equal(StandardRateName, result.RateName);
			Assert.Equal(40.00m, result.Amount);
		}

		[Fact]
		public async Task ReturnsEarlyBirdFareWithLowerRange()
		{
			var result = await StartRequest(new DateTime(2019, 07, 03, 06, 00, 00),
				new DateTime(2019, 07, 03, 15, 30, 00));

			Assert.Equal(EarlyBirdRateName, result.RateName);
			Assert.Equal(130.00m, result.Amount);
		}

		[Fact]
		public async Task ReturnsEarlyBirdFareWithUpperRange()
		{
			var result = await StartRequest(new DateTime(2019, 07, 03, 09, 00, 00),
				new DateTime(2019, 07, 03, 23, 30, 00));

			Assert.Equal(EarlyBirdRateName, result.RateName);
			Assert.Equal(130.00m, result.Amount);
		}

		[Fact]
		public async Task ReturnsNightFareBetweenRange()
		{
			var result = await StartRequest(new DateTime(2019, 07, 03, 19, 00, 00),
				new DateTime(2019, 07, 04, 04, 30, 00));

			Assert.Equal(NightRateName, result.RateName);
			Assert.Equal(6.5m, result.Amount);
		}

		[Fact]
		public async Task ReturnsNightFareWithFridayNightEntrySaturdayMorningExit()
		{
			var result = await StartRequest(new DateTime(2019, 07, 05, 19, 00, 00),
				new DateTime(2019, 07, 06, 11, 00, 00));

			Assert.Equal(NightRateName, result.RateName);
			Assert.Equal(6.5m, result.Amount);
		}

		[Fact]
		public async Task ReturnsNightFareWithLowerRange()
		{
			var result = await StartRequest(new DateTime(2019, 07, 03, 18, 00, 00),
				new DateTime(2019, 07, 04, 03, 30, 00));

			Assert.Equal(NightRateName, result.RateName);
			Assert.Equal(6.5m, result.Amount);
		}

		[Fact]
		public async Task ReturnsNightFareWithUpperRange()
		{
			var result = await StartRequest(new DateTime(2019, 07, 04, 00, 00, 00),
				new DateTime(2019, 07, 04, 11, 30, 00));

			Assert.Equal(NightRateName, result.RateName);
			Assert.Equal(6.5m, result.Amount);
		}

		[Fact]
		public async Task ReturnsWeekendFareBetweenRange()
		{
			var result = await StartRequest(new DateTime(2019, 07, 06, 08, 00, 00),
				new DateTime(2019, 07, 07, 23, 00, 00));

			Assert.Equal(WeekendRateName, result.RateName);
			Assert.Equal(10.00m, result.Amount);
		}

		[Fact]
		public async Task ReturnsWeekendFareWithLongerRange()
		{
			var result = await StartRequest(new DateTime(2019, 07, 06, 00, 00, 00),
				new DateTime(2019, 07, 07, 00, 00, 00));

			Assert.Equal(WeekendRateName, result.RateName);
			Assert.Equal(10.00m, result.Amount);
		}

		[Fact]
		public async Task ReturnsWeekendRateWithEarlyBirdParameters()
		{
			var result = await StartRequest(new DateTime(2019, 07, 06, 06, 30, 23),
				new DateTime(2019, 07, 07, 16, 30, 23));

			Assert.Equal(WeekendRateName, result.RateName);
			Assert.Equal(10.00m, result.Amount);
		}
	}
}