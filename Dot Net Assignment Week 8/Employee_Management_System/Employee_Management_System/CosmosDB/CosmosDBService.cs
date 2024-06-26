using Employee_Management_System.Common;
using Employee_Management_System.DTO;
using Employee_Management_System.Entities;
using Microsoft.Azure.Cosmos;

namespace Employee_Management_System.CosmosDB
{
    public class CosmosDBService : ICosmosDBService
    {
        public CosmosClient _cosmosClient;
        private readonly Container _container;
        public CosmosDBService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndpoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.databaseName, Credentials.containerName);
        }

        //Employee Basic Details
        public async Task<EmployeeBasicDetailsEntity> AddEmployeeBasicDetails(EmployeeBasicDetailsEntity employeeBasicEntity)
        {
            var employee = await _container.CreateItemAsync(employeeBasicEntity);
            return employee;
        }

        public async Task<List<EmployeeBasicDetailsEntity>> GetAllEmployeesBasicDetails()
        {
            var response = _container.GetItemLinqQueryable<EmployeeBasicDetailsEntity>(true).Where(e => e.Active == true && e.Archived == false
           && e.DocumentType == Credentials.EmployeeDocumentType && e.FirstName != null).ToList();

            return response;
        }

        public async Task<EmployeeBasicDetailsEntity> GetEmployeeBasicDetailByUId(string UId)
        {
            var response = _container.GetItemLinqQueryable<EmployeeBasicDetailsEntity>(true).Where(e => e.Active == true && e.Archived == false && e.UId == UId
           && e.DocumentType == Credentials.EmployeeDocumentType && e.FirstName != null).FirstOrDefault();

            return response;
        }

        public async Task ReplaceAsync(dynamic entity)
        {
            var response = await _container.ReplaceItemAsync(entity, entity.Id);

        }

        //Employee Additional Details
        public async Task<EmployeeAdditionalDetailsEntity> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsEntity employeeEntity)
        {
            var employee = await _container.CreateItemAsync(employeeEntity);
            return employee;
        }
        public async Task<List<EmployeeAdditionalDetailsEntity>> GetAllEmployeesAdditionalDetails()
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditionalDetailsEntity>(true).Where(e => e.Active == true && e.Archived == false
           && e.DocumentType == Credentials.EmployeeDocumentType && e.WorkInformation != null).ToList();

            return response;
        }
        public async Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDetailByUId(string UId)
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditionalDetailsEntity>(true).Where(e => e.Active == true && e.Archived == false && e.UId == UId
           && e.DocumentType == Credentials.EmployeeDocumentType && e.WorkInformation != null).FirstOrDefault();

            return response;
        }

        public async Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDetailsByBasicDetailsUId(string employeeBasicDetailsUid)
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditionalDetailsEntity>(true).Where(e => e.Active == true && e.Archived == false && e.EmployeeBasicDetailsUId == employeeBasicDetailsUid
            && e.DocumentType == Credentials.EmployeeDocumentType && e.WorkInformation != null).FirstOrDefault();
            return response;
        }
    }
}
