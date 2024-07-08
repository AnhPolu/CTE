using AutoMapper;
using CTEMS.Lib.Model;
using CTEMS.Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTEMS.Service.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeVM>().ReverseMap();
            CreateMap<User, UserVM>().ReverseMap();

        }
    }
}
