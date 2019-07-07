using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using ParkingRate.Web;

namespace ParkingRate.Tests.Integration
{
	public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services => { });
		}
	}
}