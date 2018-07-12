using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Stringly.Models;
using Stringly.DTO;

namespace Stringly.App_Start
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Guitar, GuitarDTO>();
			CreateMap<GuitarDTO, Guitar>();
		}
	}
}