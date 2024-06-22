using AutoMapper;
using Employee_Management_System.DTO;
using Employee_Management_System.Entities;

namespace Employee_Management_System.Common
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        { 
            CreateMap<EmployeeBasicDetailsEntity,EmployeeBasicDetailsDTO>().ReverseMap();

            CreateMap<EmployeeAdditionalDetailsEntity, EmployeeAdditionalDetailsDTO>().ReverseMap();

            CreateMap<Employee_Management_System.Entities.WorkInfo_, Employee_Management_System.DTO.WorkInfo_>().ReverseMap();
            CreateMap<Employee_Management_System.Entities.PersonalDetails_, Employee_Management_System.DTO.PersonalDetails_>().ReverseMap();
            CreateMap<Employee_Management_System.Entities.IdentityInfo_, Employee_Management_System.DTO.IdentityInfo_>().ReverseMap();
        }
    }
}
