using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Interfaces;
using Tms.ApplicationCore.Models;
using Tms.Infrastructure.Data;
using Tms.Web.Interfaces;

namespace Tms.Web.Services
{
	public class TrainingService : ITrainingService
	{
		private IUnitOfWork _unitOfWork;
		private ITmsDapper _dapper;

		public TrainingService(IUnitOfWork unitOfWork, ITmsDapper dapper)
		{
			_unitOfWork = unitOfWork;
			_dapper = dapper;
		}

		async Task<Training> ITrainingService.GetTraining(int trainingId)
		{
			return await _unitOfWork.TrainingRepository.GetByIdAsync(trainingId);
		}

		async Task<Training> ITrainingService.AddTraining(Training training)
		{
			await _unitOfWork.TrainingRepository.AddAsync(training);
			return training;
		}

		async Task<Training> ITrainingService.UpdateTraining(Training training)
		{
			await _unitOfWork.TrainingRepository.UpdateAsync(training);
			return training;
		}

		async Task<IReadOnlyList<Training>> ITrainingService.ListTrainings()
		{
			return await _unitOfWork.TrainingRepository.ListAllAsync();
		}

		async Task<IReadOnlyList<TrainingSession>> ITrainingService.ListSessions()
		{
			return await _unitOfWork.TrainingSessionRepository.ListAllAsync();
		}

		async Task ITrainingService.SaveData(string tableName, string columnName, object colValue, int id)
		{
			var sql = @"UPDATE dbo." + tableName + " SET " + columnName + " = @colValue WHERE ID = @id";
			await _dapper.QueryNonQuery(sql, new { colValue = colValue, id = id });
		}

		async Task ITrainingService.DeleteRecord(string tableName, int id)
		{
			var sql = @"DELETE FROM dbo." + tableName + " WHERE ID = @id";
			await _dapper.QueryNonQuery(sql, new { id = id });
		}

		async Task ITrainingService.DeleteTraining(int trainingId)
		{
			var sql = @"DELETE FROM dbo.Training WHERE ID = @id";
			await _dapper.QueryNonQuery(sql, new { id = trainingId });
		}

		async Task<IReadOnlyList<int>> ITrainingService.GetTrainingPopulationUPNs(TrainingPopulationKey trainingPopulationKey, string auditSubGeoKey)
		{
			var sqlString = "";

			switch (trainingPopulationKey)
			{
				case TrainingPopulationKey.AuditSeniorsandAssociatesLevel2and3:
					sqlString = Sql.TrainingPopulation_GetAuditSeniorsandAssociateslevel2and3;
					break;
				case TrainingPopulationKey.AuditAssociatesLevel0and1:
					sqlString = Sql.TrainingPopulation_GetAuditAssociatesLevel0and1;
					break;
				case TrainingPopulationKey.AuditSeniorsandAssociates:
					sqlString = Sql.TrainingPopulation_GetAuditSeniorsandAssociates;
					break;
				case TrainingPopulationKey.AuditSeniorsLevel2andGreater:
					sqlString = Sql.TrainingPopulation_GetAuditSeniorsLevel2andGreater;
					break;
				case TrainingPopulationKey.AuditSeniorsLevel0:
					sqlString = Sql.TrainingPopulation_GetAuditSeniorsLevel0;
					break;
				case TrainingPopulationKey.AuditSeniorsLevel1:
					sqlString = Sql.TrainingPopulation_GetAuditSeniorsLevel1;
					break;
				case TrainingPopulationKey.AuditAssociatesLevel2andGreater:
					sqlString = Sql.TrainingPopulation_GetAuditAssociatesLevel2andGreater;
					break;
				default:
					throw new ArgumentOutOfRangeException("Unknown Training Population Key: " + trainingPopulationKey);
			}
			var upns = await _dapper.Query<int>(sqlString, new { AuditSubGeoKey = auditSubGeoKey });
			return upns.ToList();
		}
	}
}
