﻿using AutoMapper;
using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Models;
using Tms.Web.Helpers;
using Tms.Web.ViewModels;

namespace Tms.Web
{
	public class MappingProfile: Profile
    {
        public MappingProfile()
        {
			CreateMap<SecurityRoleDetail, SecurityRoleViewModel>()
				.ForMember(dest => dest.Actions, opt => opt.MapFrom(src => SecurityHelper.GetPermFlags(src.PermFlag)))
				;

			CreateMap<SecurityRoleDetail, SecurityUserRoleViewModel>()
				.ForMember(dest => dest.Actions, opt => opt.MapFrom(src => SecurityHelper.GetPermFlags(src.PermFlag)))
				;

			CreateMap<SecurityUserRoleViewModel, SecurityRole>()
				.ForMember(dest => dest.PermFlag, opt => opt.MapFrom(src => SecurityHelper.GetPerm(src.Actions)))
				.ForMember(dest => dest.Practice, opt => opt.MapFrom(src => "Audit"))
				;

			CreateMap<SecurityEmployeeDelegationViewModel, SecurityEmployeeDelegation>()
				;
		}
    }
}
