using Visitor_Security_Clearance_System.Common;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entities;
using Visitor_Security_Clearance_System.Interface;

namespace Visitor_Security_Clearance_System.Services
{
    public class OfficeService:IOfficeService
    {
        public readonly ICosmosDBService _cosmosDBService;

        public OfficeService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        public async Task<OfficeDTO> CreateOffice(OfficeDTO officeDTO)
        {
            OfficeEntity office = new OfficeEntity();

            office.UId = officeDTO.UId;
            office.OfficeName = officeDTO.OfficeName;
            office.Email = officeDTO.Email;
            office.Password= officeDTO.Password;
            office.PhoneNumber = officeDTO.PhoneNumber;
            office.Address = officeDTO.Address;
            office.Floor = officeDTO.Floor;


            office.Initialize(true, Credentials.OfficeDocumentType, "pravin", "pravin");

            var response = await _cosmosDBService.CreateOffice(office);

            var responseModel = new OfficeDTO();

            responseModel.UId = response.UId;
            responseModel.OfficeName = response.OfficeName;
            responseModel.Email = response.Email;
            responseModel.PhoneNumber = response.PhoneNumber;
            responseModel.Address = response.Address;
            responseModel.Floor = response.Floor;


            return responseModel;

        }

        public async Task<OfficeDTO> ReadOfficeByUId(string UId)
        {
            var response = await _cosmosDBService.ReadOfficeByUId(UId);

            var responseModel = new OfficeDTO();
            responseModel.UId = response.UId;
            responseModel.OfficeName = response.OfficeName;
            responseModel.Email = response.Email;
            responseModel.PhoneNumber = response.PhoneNumber;
            responseModel.Address = response.Address;
            responseModel.Floor = response.Floor;

            return responseModel;

        }
        public async Task<OfficeDTO> UpdateOffice(OfficeDTO officeDTO)
        {
            var existingOffice = await _cosmosDBService.ReadOfficeByUId(officeDTO.UId);

            existingOffice.Active = false;
            existingOffice.Archived = true;

            await _cosmosDBService.ReplaceAsync(existingOffice);

            existingOffice.Initialize(false, Credentials.OfficeDocumentType, "pravin", "pravin");

            existingOffice.UId = officeDTO.UId;
            existingOffice.OfficeName = officeDTO.OfficeName;
            existingOffice.Email = officeDTO.Email;
            existingOffice.Password=officeDTO.Password;
            existingOffice.PhoneNumber = officeDTO.PhoneNumber;
            existingOffice.Address = officeDTO.Address;
            existingOffice.Floor = officeDTO.Floor;

            var response = await _cosmosDBService.CreateOffice(existingOffice);

            var responseModel = new OfficeDTO
            {
                UId = response.UId,
                OfficeName = response.OfficeName,
                Email = response.Email,
                PhoneNumber = response.PhoneNumber,
                Address = response.Address,
                Floor = response.Floor,

            };

            return responseModel;

        }
        public async Task<string> DeleteOffice(string UId)
        {
            var office = await _cosmosDBService.ReadOfficeByUId(UId);
            office.Active = false;
            office.Archived = true;

            await _cosmosDBService.ReplaceAsync(office);

            office.Initialize(false, Credentials.OfficeDocumentType, "pravin", "pravin");
            office.Active = false;

            await _cosmosDBService.CreateOffice(office);

            return "Record deleted succesfully";
        }
    }
}
