using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.MappingProfile
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ForMember(d=>d.Name ,O=>O.MapFrom(S=>S.EmpName)).ReverseMap();
        }
    }
}
