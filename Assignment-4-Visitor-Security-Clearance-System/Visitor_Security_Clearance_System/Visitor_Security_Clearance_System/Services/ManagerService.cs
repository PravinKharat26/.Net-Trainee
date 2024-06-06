using Visitor_Security_Clearance_System.Common;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entities;
using Visitor_Security_Clearance_System.Interface;

namespace Visitor_Security_Clearance_System.Services
{
    public class ManagerService : IManagerService

    {

        public readonly ICosmosDBService _cosmosDBService;

        public ManagerService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }
        public async Task<ManagerDTO> CreateManager(ManagerDTO managerDTO)
        {
            ManagerEntity manager = new ManagerEntity();

            manager.UId = managerDTO.UId;
            manager.Name = managerDTO.Name;
            manager.Email = managerDTO.Email;
            manager.Password = managerDTO.Password;
            manager.PhoneNumber = managerDTO.PhoneNumber;
            manager.Address = managerDTO.Address;
            manager.Age = managerDTO.Age;

            manager.Initialize(true, Credentials.ManagerDocumentType, "pravin", "pravin");

            var response = await _cosmosDBService.CreateManager(manager);

            var responseModel = new ManagerDTO();

            responseModel.UId = response.UId;
            responseModel.Name = response.Name;
            responseModel.Email = response.Email;
            responseModel.PhoneNumber = response.PhoneNumber;
            responseModel.Address = response.Address;
            responseModel.Age = response.Age;


            return responseModel;

        }

        public async Task<ManagerDTO> ReadManagerByUId(string UId)
        {
            var response = await _cosmosDBService.ReadManagerByUId(UId);

            var responseModel = new ManagerDTO();
            responseModel.UId = response.UId;
            responseModel.Name = response.Name;
            responseModel.Email = response.Email;
            responseModel.PhoneNumber = response.PhoneNumber;
            responseModel.Address = response.Address;
            responseModel.Age= response.Age;

            return responseModel;

        }

        public async Task<ManagerDTO> UpdateManager(ManagerDTO managerDTO)
        {
            var existingManager = await _cosmosDBService.ReadManagerByUId(managerDTO.UId);

            existingManager.Active = false;
            existingManager.Archived = true;

            await _cosmosDBService.ReplaceAsync(existingManager);

            existingManager.Initialize(false, Credentials.ManagerDocumentType, "pravin", "pravin");

            existingManager.UId = managerDTO.UId;
            existingManager.Name = managerDTO.Name;
            existingManager.Email = managerDTO.Email;
            existingManager.Password= managerDTO.Password;
            existingManager.PhoneNumber = managerDTO.PhoneNumber;
            existingManager.Address = managerDTO.Address;
            existingManager.Age=managerDTO.Age;

            var response = await _cosmosDBService.CreateManager(existingManager);

            var responseModel = new ManagerDTO
            {
                UId = response.UId,
                Name = response.Name,
                Email = response.Email,
                PhoneNumber = response.PhoneNumber,
                Address = response.Address,
                Age= response.Age,  

            };

            return responseModel;

        }

        public async Task<string> DeleteManager(string UId)
        {
            var manager = await _cosmosDBService.ReadManagerByUId(UId);
            manager.Active = false;
            manager.Archived = true;

            await _cosmosDBService.ReplaceAsync(manager);

            manager.Initialize(false, Credentials.ManagerDocumentType, "pravin", "pravin");
            manager.Active = false;

            await _cosmosDBService.CreateManager(manager);

            return "Record deleted succesfully";

        }
    }
}
