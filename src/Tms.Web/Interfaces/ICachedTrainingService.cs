using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Models;

namespace Tms.Web.Interfaces
{
	public interface ICachedTrainingService
	{
		Task<IReadOnlyList<Training>> ListTrainings();
		Task<IReadOnlyList<TrainingSession>> ListSessions();
		Task SaveData(string tableName, string columnName, object colValue, int id);
		Task DeleteRecord(string tableName, int id);
		Task<Training> AddTraining(Training training);
		Task<Training> UpdateTraining(Training training);
		Task DeleteTraining(int trainingId);
		Task<Training> GetTraining(int trainingId);
		Task RefreshTrainig(int trainingId);
		Task<IReadOnlyList<int>> GetTrainingPopulationUPNs(TrainingPopulationKey trainingPopulationKey, string auditSubGeoKey);
	}
}
