using Tms.ApplicationCore.Interfaces;

namespace Tms.Web
{
	public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ITrainingRepository trainingRepository, ITrainingSessionRepository sessionRepository,
			ISecurityRoleRepository securityRoleRepository, ISecurityUserRoleRepository securityUserRoleRepository,
			ISecurityEmployeeDelegationRepository securityEmployeeDelegationRepository)
        {
            TrainingRepository = trainingRepository;
            TrainingSessionRepository = sessionRepository;
			SecurityRoleRepository = securityRoleRepository;
			SecurityUserRoleRepository = securityUserRoleRepository;
			SecurityEmployeeDelegationRepository = securityEmployeeDelegationRepository;
		}

        public ITrainingRepository TrainingRepository { get; private set; }
        public ITrainingSessionRepository TrainingSessionRepository { get; private set; }
		public ISecurityRoleRepository SecurityRoleRepository { get; private set; }
		public ISecurityUserRoleRepository SecurityUserRoleRepository { get; private set; }
		public ISecurityEmployeeDelegationRepository SecurityEmployeeDelegationRepository { get; private set; }
	}
}
