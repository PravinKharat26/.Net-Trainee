using Visitor_Security_Clearance_System.Common;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entities;
using Visitor_Security_Clearance_System.Interface;

namespace Visitor_Security_Clearance_System.Services
{
    public class LoginService:ILoginService
    {
        public readonly ICosmosDBService _cosmosDBService;

        public LoginService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        public async Task<string> Login(LoginDTO loginDTO)
        {
            LoginEntity login = new LoginEntity();

           
            login.Email = loginDTO.Email;
            login.Password = loginDTO.Password;

            login.Initialize(true, Credentials.LoginDocumentType, "pravin", "pravin");

            var response = await _cosmosDBService.Login(login);
            return response;
        }
    }
}
