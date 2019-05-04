﻿using System.Drawing;
using Tms.ApplicationCore.Attributes;

namespace Tms.ApplicationCore
{
	public class TmsEnums
	{
		public enum UtbType
		{
			[DbValue("Employee")]
			Employee,
		}

		public enum Repository
		{
			[DbValue("Training")]
			Training,

			[DbValue("TrainingSession")]
			TrainingSessionr,

			[DbValue("TrainingSessionRoster")]
			TrainingSessionRoster,
		}
	}
}
