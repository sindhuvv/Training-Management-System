namespace Tms.Infrastructure.Data
{
	public partial class Sql
	{
		public const string TrainingPopulation_GetAuditSeniorsandAssociateslevel2and3 = @"
		--DECLARE @AuditSubGeoKey VARCHAR(MAX)

		SELECT [UPN]
		FROM [Hive].[dbo].[Kata_Employee_Audit_Active] emp
		INNER JOIN [Hive].[dbo].[Kata_BusinessUnit] bu ON bu.BusinessUnitKey = emp.BusinessUnitKey
		WHERE ([RankId] = 41 OR ([RankId] = 43 AND Sublevel in(2,3)))
		AND (@AuditSubGeoKey IS NULL OR @AuditSubGeoKey = bu.AuditSubGeoKey)
		";

		public const string TrainingPopulation_GetAuditAssociatesLevel0and1 = @"
		--DECLARE @AuditSubGeoKey VARCHAR(MAX)

		SELECT [UPN]
		FROM [Hive].[dbo].[Kata_Employee_Audit_Active]  emp
		INNER JOIN [Hive].[dbo].[Kata_BusinessUnit] bu ON bu.BusinessUnitKey = emp.BusinessUnitKey
		WHERE ([RankId] = 43 AND [Sublevel] in (0,1))
		AND (@AuditSubGeoKey IS NULL OR @AuditSubGeoKey = bu.AuditSubGeoKey)
		";

		public const string TrainingPopulation_GetAuditSeniorsandAssociates = @"
		--DECLARE @AuditSubGeoKey VARCHAR(MAX)

		SELECT [UPN]
		FROM [Hive].[dbo].[Kata_Employee_Audit_Active] emp
		INNER JOIN [Hive].[dbo].[Kata_BusinessUnit] bu ON bu.BusinessUnitKey = emp.BusinessUnitKey
		WHERE ([RankId] in (41, 43))
		AND (@AuditSubGeoKey IS NULL OR @AuditSubGeoKey = bu.AuditSubGeoKey)
		";

		public const string TrainingPopulation_GetAuditSeniorsLevel2andGreater = @"
		--DECLARE @AuditSubGeoKey VARCHAR(MAX)

		SELECT [UPN]
		FROM [Hive].[dbo].[Kata_Employee_Audit_Active] emp
		INNER JOIN [Hive].[dbo].[Kata_BusinessUnit] bu ON bu.BusinessUnitKey = emp.BusinessUnitKey
		WHERE ([RankId] in (41) and Sublevel >= 2)
		AND (@AuditSubGeoKey IS NULL OR @AuditSubGeoKey = bu.AuditSubGeoKey)
		";

		public const string TrainingPopulation_GetAuditSeniorsLevel0 = @"
		--DECLARE @AuditSubGeoKey VARCHAR(MAX)

		SELECT [UPN]
		FROM [Hive].[dbo].[Kata_Employee_Audit_Active] emp
		INNER JOIN [Hive].[dbo].[Kata_BusinessUnit] bu ON bu.BusinessUnitKey = emp.BusinessUnitKey
		WHERE ([RankId] in (41) and Sublevel = 0)
		AND (@AuditSubGeoKey IS NULL OR @AuditSubGeoKey = bu.AuditSubGeoKey)
		";

		public const string TrainingPopulation_GetAuditSeniorsLevel1 = @"
		--DECLARE @AuditSubGeoKey VARCHAR(MAX)

		SELECT [UPN]
		FROM [Hive].[dbo].[Kata_Employee_Audit_Active] emp
		INNER JOIN [Hive].[dbo].[Kata_BusinessUnit] bu ON bu.BusinessUnitKey = emp.BusinessUnitKey
		WHERE ([RankId] in (41) and Sublevel = 1)
		AND (@AuditSubGeoKey IS NULL OR @AuditSubGeoKey = bu.AuditSubGeoKey)
		";

		public const string TrainingPopulation_GetAuditAssociatesLevel2andGreater = @"
		--DECLARE @AuditSubGeoKey VARCHAR(MAX)

		SELECT [UPN]
		FROM [Hive].[dbo].[Kata_Employee_Audit_Active] emp
		INNER JOIN [Hive].[dbo].[Kata_BusinessUnit] bu ON bu.BusinessUnitKey = emp.BusinessUnitKey
		WHERE ([RankId] in (43) and Sublevel >= 2)
		AND (@AuditSubGeoKey IS NULL OR @AuditSubGeoKey = bu.AuditSubGeoKey)
		";
	}
}