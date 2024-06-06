using Visitor_Security_Clearance_System.Common;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entities;
using Visitor_Security_Clearance_System.Interface;


namespace Visitor_Security_Clearance_System.Services
{
    public class SecurityService : ISecurityService
    {
        public readonly ICosmosDBService _cosmosDBService;

        public SecurityService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService; 
        }
    
        public async Task<SecurityDTO> CreateSecurity(SecurityDTO securityDTO)
        {
            SecurityEntity security = new SecurityEntity();

            security.UId = securityDTO.UId;
            security.Name = securityDTO.Name;
            security.Email = securityDTO.Email;
            security.Password = securityDTO.Password;
            security.PhoneNumber = securityDTO.PhoneNumber;
            security.Address = securityDTO.Address;
            security.Age= securityDTO.Age;

            security.Initialize(true, Credentials.SecurityDocumentType, "pravin", "pravin");

            var response = await _cosmosDBService.CreateSecurity(security);

            var responseModel = new SecurityDTO();

            responseModel.UId = response.UId;
            responseModel.Name = response.Name;
            responseModel.Email = response.Email;
            responseModel.PhoneNumber = response.PhoneNumber;
            responseModel.Address = response.Address;
            responseModel.Age = response.Age;
           

            return responseModel;

        }

        public async Task<SecurityDTO> ReadSecurityByUId (string UId)
        {
            var response = await _cosmosDBService.ReadSecurityByUId (UId);

            var responseModel = new SecurityDTO();

            responseModel.UId = response.UId;
            responseModel.Name = response.Name;
            responseModel.Email = response.Email;
            responseModel.PhoneNumber = response.PhoneNumber;
            responseModel.Address = response.Address;
            responseModel.Age= response.Age; 

            return responseModel;
        }

        public async Task<SecurityDTO> UpdateSecurity(SecurityDTO securityDTO)
        {
            var existingSecurity = await _cosmosDBService.ReadSecurityByUId(securityDTO.UId);

            existingSecurity.Active = false;
            existingSecurity.Archived = true;

            await _cosmosDBService.ReplaceAsync(existingSecurity);

            existingSecurity.Initialize(false, Credentials.SecurityDocumentType, "pravin", "pravin");

            existingSecurity.UId = securityDTO.UId;
            existingSecurity.Name = securityDTO.Name;
            existingSecurity.Email= securityDTO.Email;
            existingSecurity.Password= securityDTO.Password;
            existingSecurity.PhoneNumber = securityDTO.PhoneNumber;
            existingSecurity.Address = securityDTO.Address;
            existingSecurity.Age = securityDTO.Age;


            var response = await _cosmosDBService.CreateSecurity(existingSecurity);

            var responseModel = new SecurityDTO
            {
                UId = response.UId,
                Name = response.Name,
                Email = response.Email,
                PhoneNumber = response.PhoneNumber,
                Address = response.Address,
                Age = response.Age
            };

            return responseModel;

        }

        public async Task<string> DeleteSecurity(string UId) 
        {
            var security = await _cosmosDBService.ReadSecurityByUId(UId);
            security.Active = false;
            security.Archived = true;

            await _cosmosDBService.ReplaceAsync(security);

            security.Initialize(false, Credentials.SecurityDocumentType, "pravin", "pravin");
            security.Active = false;

            await _cosmosDBService.CreateSecurity(security);

            return "Record deleted succesfully";

        }

    }
}
