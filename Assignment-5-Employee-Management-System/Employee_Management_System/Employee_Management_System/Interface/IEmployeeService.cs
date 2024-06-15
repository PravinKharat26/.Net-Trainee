﻿using Employee_Management_System.DTO;
using Employee_Management_System.Entities;

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

        //Employee Additional Details
        public Task<EmployeeAdditionalDetailsDTO> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsDTO employeeDTO);
        public Task<List<EmployeeAdditionalDetailsDTO>> GetAllEmployeesAdditionalDetails();
        public Task<EmployeeAdditionalDetailsDTO> GetEmployeeAdditionalDetailByUId(string UId);
        public Task<EmployeeAdditionalDetailsDTO> UpdateEmployeeAdditionalDetails(EmployeeAdditionalDetailsDTO employeeAdditionalDetails);
        public Task<string> DeleteEmployeeAdditionalDetails(string UId);
        public Task<List<EmployeeAdditionalDetailsDTO>> GetAllEmployeeAdditionalDetailsByDesignationName(string designationName);
        public Task<EmployeeAdditionalFilterCriteria> GetAllEmployeesAdditionalDetailsByPagination(EmployeeAdditionalFilterCriteria employeeAdditionalFilterCriteria);
    }
}