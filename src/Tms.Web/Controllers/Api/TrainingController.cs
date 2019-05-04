using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;
using Tms.Web.Interfaces;

namespace Tms.Web.Controllers.Api
{
	public class TrainingController : BaseApiController
	{
		private readonly ICachedTrainingService _trainingService;

		public TrainingController(ICachedTrainingService trainingService) => _trainingService = trainingService;

		[HttpGet]
		[Authorize(Policy = "TrainingViewers")]
		public async Task<IActionResult> List()
		{
			var catalogModel = await _trainingService.ListTrainings();
			return Ok(catalogModel);
		}

		[HttpPost]
		public async Task<IActionResult> SaveData(string tableName, string columnName, string colValue, int id)
		{
			await _trainingService.SaveData(tableName, columnName, colValue, id);
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteRecord(string tableName, int id)
		{
			await _trainingService.DeleteRecord(tableName, id);
			return Ok(true);
		}

		#region Training

		[HttpGet]
		[Authorize(Policy = "TrainingViewers")]
		public async Task<ActionResult<Training>> GetTraining(int trainingId)
		{
			var training = await _trainingService.GetTraining(trainingId);
			if (training == null)
				return NotFound();
			return training;
		}

		[HttpPost]
		[Authorize(Policy = "TrainingViewers")]
		public async Task<ActionResult<Training>> AddTraining(Training training)
		{
			return await _trainingService.AddTraining(training);
		}

		[HttpPost]
		[Authorize(Policy = "TrainingViewers")]
		public async Task<ActionResult<Training>> UpdateTraining(Training training)
		{
			return await _trainingService.UpdateTraining(training);
		}

		[HttpDelete]
		[Authorize(Policy = "TrainingViewers")]
		public async Task<IActionResult> DeleteTraining(int trainingId)
		{
			await _trainingService.DeleteTraining(trainingId);
			return NoContent();
		}

		#endregion
	}
}
