﻿namespace Tms.ApplicationCore.Interfaces
{
	public interface IUnitOfWork
	{
		ITrainingRepository TrainingRepository { get; }
		ITrainingSessionRepository TrainingSessionRepository { get; }
		ISecurityRoleRepository SecurityRoleRepository { get; }
		ISecurityUserRoleRepository SecurityUserRoleRepository { get; }
		ISecurityEmployeeDelegationRepository SecurityEmployeeDelegationRepository { get; }
	}
}
