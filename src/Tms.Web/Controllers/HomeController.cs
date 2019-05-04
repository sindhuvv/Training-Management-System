using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using Tms.ApplicationCore;
using Tms.Infrastructure.Logging;
using Tms.Web.Services.Api;
using Tms.Web.Models;
using Tms.Web.ViewModels;

namespace Tms.Web.Controllers
{
	public class HomeController : Controller
	{
		private ITmsClient _apiClient;

		public HomeController(ITmsClient apiClient)
		{
			_apiClient = apiClient;
		}

		public IActionResult Index()
		{
			return View();
		}

		[Authorize(Policy = "TrainingViewers")]
		public async Task<IActionResult> Trainings()
		{
			var model = await _apiClient.ListTrainings();
			return View(model);
		}

        [HttpGet]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var model = new ErrorViewModel()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Details = new LogDetails()
                {
                    Message = TmsLogger.GetMessageFromException(exceptionHandlerPathFeature.Error),
                    Product = "TMS",
                    Location = exceptionHandlerPathFeature.Path,
                    Hostname = Environment.MachineName,
                    User = Environment.UserName,
                    Exception = exceptionHandlerPathFeature.Error
                },
            };
            return View(model);
        }

		public IActionResult Sample()
		{
			var model = new SampleViewModel();

			model.LineChart = new LineChart()
			{
				Labels = new List<string>() { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" },
				Title = "Testing",
				X_Lable = "X Axis",
				Y_Lable = "Y Axis",
				DataSet = new List<LineChartDataSet>()
					{
						new LineChartDataSet()
						{
							Data = new List<int>() { 12, 19, 3, 5, 2, 3 },
							BackgroundColor = TmsUtility.GetRGBStringValue(Color.Red),
							BorderColor = TmsUtility.GetRGBStringValue(Color.Red),
							Data_Lable = "Red",
							IsFill = false
						},
						new LineChartDataSet()
						{
							Data = new List<int>() { 2, 20, 9, 0, 1, 3 },
							BackgroundColor = TmsUtility.GetRGBStringValue(Color.Blue),
							BorderColor = TmsUtility.GetRGBStringValue(Color.Blue),
							Data_Lable = "Blue",
							IsFill = false
						}
					}
			};

			model.PieChart = new PieDoughnutChart()
			{
				Labels = new List<string>() { "Red", "Blue", "Yellow" },
				Title = "Pie Chart",
				DataSet = new List<PieDoughnutChartDataSet>()
				{
					new PieDoughnutChartDataSet()
					{
						Data = new List<int>() { 12, 19, 3},
						BackgroundColor =  new List<string>() { TmsUtility.GetRGBStringValue(Color.Plum), TmsUtility.GetRGBStringValue(Color.Yellow),TmsUtility.GetRGBStringValue(Color.Wheat)},
						BorderColor =  new List<string>() { TmsUtility.GetRGBStringValue(Color.Plum), TmsUtility.GetRGBStringValue(Color.Yellow),TmsUtility.GetRGBStringValue(Color.Wheat)},
						Data_Lable = "Red",
					},
					new PieDoughnutChartDataSet()
					{
						Data = new List<int>() { 2, 20, 9, 0, 1, 3 },
						BackgroundColor = new List<string>() { TmsUtility.GetRGBStringValue(Color.Blue), TmsUtility.GetRGBStringValue(Color.Red),TmsUtility.GetRGBStringValue(Color.PowderBlue)},
						BorderColor = new List<string>() { TmsUtility.GetRGBStringValue(Color.Blue), TmsUtility.GetRGBStringValue(Color.Red),TmsUtility.GetRGBStringValue(Color.PowderBlue) },
						Data_Lable = "Blue",
					}
				}
			};

			model.DoughnutChart = new PieDoughnutChart()
			{
				Labels = new List<string>() { "Red", "Blue", "Yellow" },
				Title = "Doughnut Chart",
				DataSet = new List<PieDoughnutChartDataSet>()
				{
					new PieDoughnutChartDataSet()
					{
						Data = new List<int>() { 12, 19, 3},
						BackgroundColor =  new List<string>() { TmsUtility.GetRGBStringValue(Color.Red), TmsUtility.GetRGBStringValue(Color.Yellow),TmsUtility.GetRGBStringValue(Color.Blue)},
						BorderColor =  new List<string>() { TmsUtility.GetRGBStringValue(Color.Red), TmsUtility.GetRGBStringValue(Color.Yellow),TmsUtility.GetRGBStringValue(Color.Blue) },
						Data_Lable = "Red",
					},
					new PieDoughnutChartDataSet()
					{
						Data = new List<int>() { 2, 20, 9, 0, 1, 3 },
						BackgroundColor = new List<string>() { TmsUtility.GetRGBStringValue(Color.Blue), TmsUtility.GetRGBStringValue(Color.Red),TmsUtility.GetRGBStringValue(Color.PowderBlue)},
						BorderColor = new List<string>() { TmsUtility.GetRGBStringValue(Color.Blue), TmsUtility.GetRGBStringValue(Color.Red),TmsUtility.GetRGBStringValue(Color.PowderBlue) },
						Data_Lable = "Blue",
					}
				}
			};

			return View(model);
		}
	}
}
