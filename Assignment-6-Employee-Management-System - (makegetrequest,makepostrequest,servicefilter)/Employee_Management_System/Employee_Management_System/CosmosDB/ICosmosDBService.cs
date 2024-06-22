using Employee_Management_System.DTO;
using Employee_Management_System.Entities;

namespace Employee_Management_System.CosmosDB
{
    public interface ICosmosDBService
    {
        //Employee Basic Details

        Task<EmployeeBasicDetailsEntity> AddEmployeeBasicDetails(EmployeeBasicDetailsEntity employeeBasicEntity);
        Task<List<EmployeeBasicDetailsEntity>> GetAllEmployeesBasicDetails();
        Task<EmployeeBasicDetailsEntity> GetEmployeeBasicDetailByUId(string UId);

        //Employee Additional Details

        Task<EmployeeAdditionalDetailsEntity> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsEntity employeeEntity);
        Task<List<EmployeeAdditionalDetailsEntity>> GetAllEmployeesAdditionalDetails();
        Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDetailByUId(string UId);

        Task ReplaceAsync(dynamic entity);
    }
}
