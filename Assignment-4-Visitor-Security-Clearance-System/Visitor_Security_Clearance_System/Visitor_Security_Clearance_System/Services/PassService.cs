using Visitor_Security_Clearance_System.Common;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entities;
using Visitor_Security_Clearance_System.Interface;

namespace Visitor_Security_Clearance_System.Services
{
    public class PassService:IPassService
    {
        public readonly ICosmosDBService _cosmosDBService;

        public PassService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        public async Task<PassDTO> CreatePass(PassDTO passDTO)
        {
            PassEntity pass = new PassEntity();

            pass.VisitorName=passDTO.VisitorName;
            pass.Email= passDTO.Email;
            pass.Status= passDTO.Status;

            pass.Initialize(true, Credentials.PassDocumentType, "pravin", "pravin");

            var response = await _cosmosDBService.CreatePass(pass);

            var responseModel = new PassDTO();

            responseModel.UId = response.UId;
            responseModel.VisitorName = response.VisitorName;
            responseModel.Email = response.Email;
            responseModel.Status= response.Status;

            return responseModel;

        }

       public async Task<PassDTO> ReadPassByUId(string UId)
        {
            var response = await _cosmosDBService.ReadPassByUId(UId);

            var responseModel = new PassDTO();

            responseModel.UId = response.UId;
            responseModel.VisitorName = response.VisitorName;
            responseModel.Email = response.Email;
            responseModel.Status=response.Status;

            return responseModel;
        }

       public async Task<PassDTO> UpdatePassStatus(PassDTO passDTO)
        {
            var existingPass =await  _cosmosDBService.ReadPassByUId(passDTO.UId);

            existingPass.Active = false;
            existingPass.Archived = true;

            await _cosmosDBService.ReplaceAsync(existingPass);

            existingPass.Initialize(false, Credentials.PassDocumentType, "pravin", "pravin");

            existingPass.UId = passDTO.UId;
            existingPass.VisitorName = passDTO.VisitorName;
            existingPass.Email = passDTO.Email;
            existingPass.Status = passDTO.Status;
            

            var response = await _cosmosDBService.CreatePass(existingPass);

            var responseModel = new PassDTO
            {
                UId = response.UId,
                VisitorName = response.VisitorName,
                Email = response.Email,
                Status = response.Status
               
            };

            return responseModel;

        }
        public async Task<PassDTO> GetVisitorsByStatus(string Status)
        {
            var response = await _cosmosDBService.GetVisitorsByStatus(Status);

            var responseModel = new PassDTO();
            responseModel.UId = response.UId;
            responseModel.VisitorName = response.VisitorName;
            responseModel.Email = response.Email;
            responseModel.Status= response.Status;
            

            return responseModel;

        }
    }
}
