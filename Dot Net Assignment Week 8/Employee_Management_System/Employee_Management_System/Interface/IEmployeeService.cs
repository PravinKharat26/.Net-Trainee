using Employee_Management_System.DTO;
using Employee_Management_System.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Interface
{
    public interface IEmployeeService
    {
        //Employee Basic Details
        public Task<EmployeeBasicDetailsDTO> AddEmployeeBasicDetails(EmployeeBasicDetailsDTO employeeBasicDTO);
        public Task<List<EmployeeBasicDetailsDTO>> GetAllEmployeesBasicDetails();
        public Task<EmployeeBasicDetailsDTO> GetEmployeeBasicDetailByUId(string UId);
        public Task<EmployeeBasicDetailsDTO> UpdateEmployeeBasicDetails(EmployeeBasicDetailsDTO employeeBasicDetails);
        public Task<string> DeleteEmployeeBasicDetails(string UId);
        public Task<List<EmployeeBasicDetailsDTO>> GetAllEmployeeBasicDetailsByReportingManagerName(string ReportingManagerName);
        public Task<EmployeeBasicFilterCriteria> GetAllEmployeesBasicDetailsByPagination(EmployeeBasicFilterCriteria employeeBasicFilterCriteria);
        public Task<EmployeeBasicDetailsDTO> AddEmployeeBasicDetailByMakePostRequest(EmployeeBasicDetailsDTO employeeBasicDetailsDTO);

        public Task<List<EmployeeBasicDetailsDTO>> GetEmployeeeBasicDetailByMakeGetRequest();

        public Task<SecurityDTO> AddSecurityByMakePostRequest(SecurityDTO securityDTO);
        //Employee Additional Details
        public Task<EmployeeAdditionalDetailsDTO> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsDTO employeeDTO);
        public Task<List<EmployeeAdditionalDetailsDTO>> GetAllEmployeesAdditionalDetails();
        public Task<EmployeeAdditionalDetailsDTO> GetEmployeeAdditionalDetailByUId(string UId);
        public Task<EmployeeAdditionalDetailsDTO> UpdateEmployeeAdditionalDetails(EmployeeAdditionalDetailsDTO employeeAdditionalDetails);
        public Task<string> DeleteEmployeeAdditionalDetails(string UId);
        public Task<List<EmployeeAdditionalDetailsDTO>> GetAllEmployeeAdditionalDetailsByDesignationName(string designationName);
        public Task<EmployeeAdditionalFilterCriteria> GetAllEmployeesAdditionalDetailsByPagination(EmployeeAdditionalFilterCriteria employeeAdditionalFilterCriteria);
        public Task<EmployeeAdditionalDetailsDTO> AddEmployeeAdditionalDetailByMakePostRequest(EmployeeAdditionalDetailsDTO employeeAdditionalDetailsDTO);

        public Task<List<EmployeeAdditionalDetailsDTO>> GetEmployeeeAdditionalDetailByMakeGetRequest();
        public Task<List<SecurityDTO>> GetAllSecurityByMakeGetRequest();
        public Task<EmployeeAdditionalDetailsDTO> GetEmployeeAdditionalDetailsByBasicDetailsUId(string employeeBasicDetailsUid);
        public Task<EmployeeAdditionalDetailsDTO> GetEmployeeAdditionalDetailsByBasicDetailsUIdUsingFilterAttribute(FilterCriteria filterCriteria);
    }
}
