using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingRate.Core.Configuration;
using ParkingRate.Core.Interfaces;
using ParkingRate.Core.Services;

namespace ParkingRate.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("ratesSettings.json", false)
				.Build();

			services.AddOptions();
			services.Configure<RatesConfiguration>(configuration);

			services.AddScoped<IRateService, RateService>();
			services.AddScoped<IRateCalculatorFactory, RateCalculatorFactory>();

			services.AddScoped<IRateCalculator, FlatRateCalculator>();
			services.AddScoped<IRateCalculator, HourlyRateCalculator>();
			services.AddScoped<IRateCalculator, DefaultRateCalculator>();

			services.AddScoped<IRateCondition, FlatRateCondition>();
			services.AddScoped<IRateCondition, HourlyRateCondition>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			//app.UseHealthChecks("/health");

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
				app.UseResponseCompression();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}