using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ParkingRate.Core.Configuration;
using ParkingRate.Core.Interfaces;
using ParkingRate.Core.Models;

namespace ParkingRate.Core.Services
{
	public class RateCalculatorFactory : IRateCalculatorFactory
	{
		private readonly ILogger _logger;

		private readonly Dictionary<IRateCondition, IRateCalculator> _rateManagers =
			new Dictionary<IRateCondition, IRateCalculator>();

		private readonly RatesConfiguration _ratesConfiguration;

		public RateCalculatorFactory(IOptions<RatesConfiguration> ratesConfiguration,
			ILogger<RateCalculatorFactory> logger)
		{
			_logger = logger;

			_ratesConfiguration =
				ratesConfiguration.Value ?? throw new ArgumentNullException(nameof(RatesConfiguration));

			if (_ratesConfiguration.DefaultRate == null) throw new ArgumentNullException(nameof(DefaultRate));

			AddRateManagers();
		}

		public IRateCalculator GetRateCalculator(ParkingParameters parameters)
		{
			var rateCalculator = _rateManagers.FirstOrDefault(condition => condition.Key.IsValid(parameters));
			return rateCalculator.Value ?? new DefaultRateCalculator(_ratesConfiguration.DefaultRate);
		}

		private void AddRateManagers()
		{
			foreach (var parameters in _ratesConfiguration.FlatRates ?? Enumerable.Empty<FlatRate>())
			{
				_rateManagers.Add(new FlatRateCondition(parameters), new FlatRateCalculator(parameters));
				_logger.LogInformation($"Rate Added:{parameters.Name}");
			}

			if (_ratesConfiguration.HourlyRate != null)
				_rateManagers.Add(
					new HourlyRateCondition(_ratesConfiguration.HourlyRate),
					new HourlyRateCalculator(_ratesConfiguration.HourlyRate));
		}
	}
}