using AutoMapper;
using Employee_Management_System.Common;
using Employee_Management_System.CosmosDB;
using Employee_Management_System.DTO;
using Employee_Management_System.Entities;
using Employee_Management_System.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Employee_Management_System.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly ICosmosDBService _cosmosDBservice;
        public readonly IMapper _mapper;
        public EmployeeService(ICosmosDBService cosmosDBService, IMapper mapper)
        {
            _cosmosDBservice = cosmosDBService;
            _mapper = mapper;
        }

        //Employee Basic Details

        public async Task<EmployeeBasicDetailsDTO> AddEmployeeBasicDetails(EmployeeBasicDetailsDTO employeeBasicDTO)
        {
            var employee = _mapper.Map<EmployeeBasicDetailsEntity>(employeeBasicDTO);

            employee.Initialize(true, Credentials.EmployeeDocumentType, "pravin", "pravin");

            var response = await _cosmosDBservice.AddEmployeeBasicDetails(employee);

            var responseModel = _mapper.Map<EmployeeBasicDetailsDTO>(response);


            return responseModel;
        }

        public async Task<List<EmployeeBasicDetailsDTO>> GetAllEmployeesBasicDetails()
        {
            var employees = await _cosmosDBservice.GetAllEmployeesBasicDetails();

            var employeeModels = new List<EmployeeBasicDetailsDTO>();

            foreach (var employee in employees)
            {

                var employeeResponse = _mapper.Map<EmployeeBasicDetailsDTO>(employee);
                employeeModels.Add(employeeResponse);

            }

            return employeeModels;
        }

        public async Task<EmployeeBasicDetailsDTO> GetEmployeeBasicDetailByUId(string UId)
        {
            var response = await _cosmosDBservice.GetEmployeeBasicDetailByUId(UId);

            var responseModel = _mapper.Map<EmployeeBasicDetailsDTO>(response);

            return responseModel;
        }
        public async Task<EmployeeBasicDetailsDTO> UpdateEmployeeBasicDetails(EmployeeBasicDetailsDTO employeeBasicDetails)
        {
            var existingEmployee = await _cosmosDBservice.GetEmployeeBasicDetailByUId(employeeBasicDetails.UId);

            existingEmployee.Active = false;
            existingEmployee.Archived = true;

            await _cosmosDBservice.ReplaceAsync(existingEmployee);

            existingEmployee.Initialize(false, Credentials.EmployeeDocumentType, "pravin", "pravin");

            _mapper.Map(employeeBasicDetails, existingEmployee);


            var response = await _cosmosDBservice.AddEmployeeBasicDetails(existingEmployee);

            var responseModel = _mapper.Map<EmployeeBasicDetailsDTO>(response);

            return responseModel;
        }

        public async Task<string> DeleteEmployeeBasicDetails(string UId)
        {
            var employee = await _cosmosDBservice.GetEmployeeBasicDetailByUId(UId);
            employee.Active = false;
            employee.Archived = true;

            await _cosmosDBservice.ReplaceAsync(employee);

            employee.Initialize(false, Credentials.EmployeeDocumentType, "pravin", "pravin");
            employee.Active = false;

            await _cosmosDBservice.AddEmployeeBasicDetails(employee);

            return "Record deleted succesfully";
        }

        public async Task<List<EmployeeBasicDetailsDTO>> GetAllEmployeeBasicDetailsByReportingManagerName(string ReportingManagerName)
        {
            var allEmployees = await GetAllEmployeesBasicDetails();

            return allEmployees.FindAll(e=>e.ReportingManagerName== ReportingManagerName);
        }

        public async Task<EmployeeBasicFilterCriteria> GetAllEmployeesBasicDetailsByPagination(EmployeeBasicFilterCriteria employeeBasicFilterCriteria)
        {
            EmployeeBasicFilterCriteria responseObject = new EmployeeBasicFilterCriteria();

            //filter=>ReportingManagerName

            var checkFilter = employeeBasicFilterCriteria.Filters.Any(e => e.FieldName == "role");
            var role = "";

            if (checkFilter)
            {
                role = employeeBasicFilterCriteria.Filters.Find(e => e.FieldName == "role").FieldValue;

            }
            var employees = await GetAllEmployeesBasicDetails();

            var filteredRecords=employees.FindAll(a=>a.Role== role);

            responseObject.totalCount = employees.Count;
            responseObject.Page = employeeBasicFilterCriteria.Page;
            responseObject.PageSize=employeeBasicFilterCriteria.PageSize;

            var skip=employeeBasicFilterCriteria.PageSize * (employeeBasicFilterCriteria.Page-1);

            filteredRecords = filteredRecords.Skip(skip).Take(employeeBasicFilterCriteria.PageSize).ToList();
            foreach (var record in filteredRecords)
            { 
              responseObject.Employees.Add(record);
            }
            return responseObject;
        }
        
        //

        public async Task<EmployeeBasicDetailsDTO> AddEmployeeBasicDetailByMakePostRequest(EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
           //call add employee api by makepostrequest

            var serializedObj=JsonConvert.SerializeObject(employeeBasicDetailsDTO);
            var requestObj = await HttpClientHelper.MakePostRequest(Credentials.EmployeeUrl, Credentials.AddEmployeeBasicEndpoint, serializedObj);

            var responseObj=JsonConvert.DeserializeObject<EmployeeBasicDetailsDTO>(requestObj); 

            return responseObj;
        }

        public async Task<List<EmployeeBasicDetailsDTO>> GetEmployeeeBasicDetailByMakeGetRequest()
        {
            //var serializedObj = JsonConvert.SerializeObject();
            var requestObj = await HttpClientHelper.MakeGetRequest(Credentials.EmployeeUrl, Credentials.GetAllEmployeeBasicEndpoint);

            var responseObj = JsonConvert.DeserializeObject<List<EmployeeBasicDetailsDTO>>(requestObj);

            return responseObj;
        }

        //Employee Additional Details

        public async Task<EmployeeAdditionalDetailsDTO> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsDTO employeeDTO)
        {
            var employee = _mapper.Map<EmployeeAdditionalDetailsEntity>(employeeDTO);

            employee.Initialize(true, Credentials.EmployeeDocumentType, "pravin", "pravin");

            var response = await _cosmosDBservice.AddEmployeeAdditionalDetails(employee);

            var responseModel = _mapper.Map<EmployeeAdditionalDetailsDTO>(response);


            return responseModel;
        }

        public async Task<List<EmployeeAdditionalDetailsDTO>> GetAllEmployeesAdditionalDetails()
        {
            var employees = await _cosmosDBservice.GetAllEmployeesAdditionalDetails();

            var employeeModels = new List<EmployeeAdditionalDetailsDTO>();

            foreach (var employee in employees)
            {

                var employeeResponse = _mapper.Map<EmployeeAdditionalDetailsDTO>(employee);
                employeeModels.Add(employeeResponse);

            }

            return employeeModels;
        }
        public async Task<EmployeeAdditionalDetailsDTO> GetEmployeeAdditionalDetailByUId(string UId)
        {
            var response = await _cosmosDBservice.GetEmployeeAdditionalDetailByUId(UId);

            var responseModel = _mapper.Map<EmployeeAdditionalDetailsDTO>(response);

            return responseModel;
        }

        public async Task<EmployeeAdditionalDetailsDTO> UpdateEmployeeAdditionalDetails(EmployeeAdditionalDetailsDTO employeeAdditionalDetails)
        {
            var existingEmployee = await _cosmosDBservice.GetEmployeeAdditionalDetailByUId(employeeAdditionalDetails.UId);

            existingEmployee.Active = false;
            existingEmployee.Archived = true;

            await _cosmosDBservice.ReplaceAsync(existingEmployee);

            existingEmployee.Initialize(false, Credentials.EmployeeDocumentType, "pravin", "pravin");

            _mapper.Map(employeeAdditionalDetails, existingEmployee);


            var response = await _cosmosDBservice.AddEmployeeAdditionalDetails(existingEmployee);

            var responseModel = _mapper.Map<EmployeeAdditionalDetailsDTO>(response);

            return responseModel;
        }

        public async Task<string> DeleteEmployeeAdditionalDetails(string UId)
        {
            var employee = await _cosmosDBservice.GetEmployeeAdditionalDetailByUId(UId);
            employee.Active = false;
            employee.Archived = true;

            await _cosmosDBservice.ReplaceAsync(employee);

            employee.Initialize(false, Credentials.EmployeeDocumentType, "pravin", "pravin");
            employee.Active = false;

            await _cosmosDBservice.AddEmployeeAdditionalDetails(employee);

            return "Record deleted succesfully";
        }

        public async Task<List<EmployeeAdditionalDetailsDTO>> GetAllEmployeeAdditionalDetailsByDesignationName(string designationName)
        {
            var allEmployees = await GetAllEmployeesAdditionalDetails();

            return allEmployees.FindAll(e => e.WorkInformation.DesignationName == designationName);
        }

        public async Task<EmployeeAdditionalFilterCriteria> GetAllEmployeesAdditionalDetailsByPagination(EmployeeAdditionalFilterCriteria employeeAdditionalFilterCriteria)
        {
            EmployeeAdditionalFilterCriteria responseObject = new EmployeeAdditionalFilterCriteria();

            //filter=>DesignationName

            var checkFilter = employeeAdditionalFilterCriteria.Filters.Any(f => f.FieldName == "employeeStatus");
            var employeeStatus = "";

            if (checkFilter)
            {
                employeeStatus = employeeAdditionalFilterCriteria.Filters.Find(f=> f.FieldName == "employeeStatus").FieldValue;

            }
            var employees = await GetAllEmployeesAdditionalDetails();

            var filteredRecords = employees.FindAll(a => a.WorkInformation.EmployeeStatus == employeeStatus);

            responseObject.totalCount = employees.Count;
            responseObject.Page = employeeAdditionalFilterCriteria.Page;
            responseObject.PageSize = employeeAdditionalFilterCriteria.PageSize;

            var skip = employeeAdditionalFilterCriteria.PageSize * (employeeAdditionalFilterCriteria.Page - 1);

            filteredRecords = filteredRecords.Skip(skip).Take(employeeAdditionalFilterCriteria.PageSize).ToList();
            foreach (var record in filteredRecords)
            {
                responseObject.Employees.Add(record);
            }
            return responseObject;
        }

        //
        public async Task<EmployeeAdditionalDetailsDTO> AddEmployeeAdditionalDetailByMakePostRequest(EmployeeAdditionalDetailsDTO employeeAdditionalDetailsDTO)
        {
            //call add employee api by makepostrequest

            var serializedObj = JsonConvert.SerializeObject(employeeAdditionalDetailsDTO);
            var requestObj = await HttpClientHelper.MakePostRequest(Credentials.EmployeeUrl, Credentials.AddEmployeeAdditionalEndpoint, serializedObj);

            var responseObj = JsonConvert.DeserializeObject<EmployeeAdditionalDetailsDTO>(requestObj);

            return responseObj;
        }

        [HttpGet]
        public async Task<List<EmployeeAdditionalDetailsDTO>> GetEmployeeeAdditionalDetailByMakeGetRequest()
        {
            //var serializedObj = JsonConvert.SerializeObject();
            var requestObj = await HttpClientHelper.MakeGetRequest(Credentials.EmployeeUrl, Credentials.GetAllEmployeeAdditionalEndpoint);

            var responseObj = JsonConvert.DeserializeObject<List<EmployeeAdditionalDetailsDTO>>(requestObj);

            return responseObj;
        }


        //makepostrequest (for Visitor-Security)----
        public async Task<SecurityDTO> AddSecurityByMakePostRequest(SecurityDTO securityDTO)
        {
            //call add employee api by makepostrequest

            var serializedObj = JsonConvert.SerializeObject(securityDTO);
            var requestObj = await HttpClientHelper.MakePostRequest(Credentials.VisitorUrl, Credentials.AddSecurityEndpoint, serializedObj);

            var responseObj = JsonConvert.DeserializeObject<SecurityDTO>(requestObj);

            return responseObj;
        }

        //makegetrequest
        public async Task<List<SecurityDTO>> GetAllSecurityByMakeGetRequest()
        {
            var requestObj = await HttpClientHelper.MakeGetRequest(Credentials.VisitorUrl, Credentials.GetAllSecurityEndpoint);

            var responseObj = JsonConvert.DeserializeObject<List<SecurityDTO>>(requestObj);

            return responseObj;
        } 
    }
}
