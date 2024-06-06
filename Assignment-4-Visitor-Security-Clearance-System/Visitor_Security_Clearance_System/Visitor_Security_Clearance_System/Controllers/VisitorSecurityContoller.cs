using Microsoft.AspNetCore.Mvc;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Interface;
using Visitor_Security_Clearance_System.Services;

namespace Visitor_Security_Clearance_System.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class VisitorSecurityContoller : Controller
    {
        public readonly IVisitorService _visitorService;
        public readonly ISecurityService _securityService;
        public readonly IManagerService _managerService;
        public readonly IOfficeService _officeService;
        public readonly ILoginService _loginService;
        public readonly IPassService _passService;

        public VisitorSecurityContoller (IVisitorService visitorService,ISecurityService securityService,
            IManagerService managerService,IOfficeService officeService, ILoginService loginService,IPassService passService)
        {
            _visitorService = visitorService;
            _securityService = securityService;
            _managerService = managerService;
            _officeService= officeService;
            _loginService= loginService;
            _passService= passService;

        }

        
        //Visitor

        [HttpPost]

        public async Task<VisitorDTO> CreateVisitor(VisitorDTO visitorDTO)
        {
         
            var response = await _visitorService.CreateVisitor(visitorDTO);
            //return the response
            return response;
        }

        [HttpGet]

        public async Task <VisitorDTO> ReadVisitorByUId(string UId)
        {
            var response = await _visitorService.ReadVisitorByUId(UId);
            return response;
        }

        [HttpPost]
        public async Task<VisitorDTO> UpdateVisitor(VisitorDTO visitorDTO)
        {
            var response = await _visitorService.UpdateVisitor(visitorDTO);
            return response;
        }

        [HttpPost]
        public async Task<string> DeleteVisitor(string UId)
        {
            var response = await _visitorService.DeleteVisitor(UId);
            return response;
        }

        //Security

        [HttpPost]

        public async Task<SecurityDTO> CreateSecurity(SecurityDTO securityDTO)
        {
            
            var response = await _securityService.CreateSecurity(securityDTO);
            //return the response
            return response;
        }

       [HttpGet]

        public async Task<SecurityDTO> ReadSecurityByUId(string UId)
        {
            var response = await _securityService.ReadSecurityByUId(UId);
            return response;
        }

        [HttpPost]
        public async Task<SecurityDTO> UpdateSecurity(SecurityDTO securityDTO)
        {
            var response = await _securityService.UpdateSecurity(securityDTO);
            return response;
        }
       
        [HttpPost]
        public async Task<string> DeleteSecurity(string UId)
        {
            var response = await _securityService.DeleteSecurity(UId);
            return response;
        }

        //Manager

        [HttpPost]

        public async Task<ManagerDTO> CreateManager(ManagerDTO managerDTO)
        {
            
            var response = await _managerService.CreateManager(managerDTO);
            //return the response
            return response;
        }

       [HttpGet]

        public async Task<ManagerDTO> ReadManagerByUId(string UId)
        {
            var response = await _managerService.ReadManagerByUId(UId);
            return response;
        }

        [HttpPost]
        public async Task<ManagerDTO> UpdateManager(ManagerDTO managerDTO)
        {
            var response = await _managerService.UpdateManager(managerDTO);
            return response;
        }

       [HttpPost]
        public async Task<string> DeleteManager(string UId)
        {
            var response = await _managerService.DeleteManager(UId);
            return response;
        }

        //Office

        [HttpPost]

        public async Task<OfficeDTO> CreateOffice(OfficeDTO officeDTO)
        {
            var response = await _officeService.CreateOffice(officeDTO);
            //return the response
            return response;
        }

        [HttpGet]

        public async Task<OfficeDTO> ReadOfficeByUId(string UId)
        {
            var response = await _officeService.ReadOfficeByUId(UId);
            return response;
        }

        [HttpPost]
        public async Task<OfficeDTO> UpdateOffice(OfficeDTO officeDTO)
        {
            var response = await _officeService.UpdateOffice(officeDTO);
            return response;
        }

        [HttpPost]
        public async Task<string> DeleteOffice(string UId)
        {
            var response = await _officeService.DeleteOffice(UId);
            return response;
        }

        //Login

        [HttpPost]

        public async Task<string>Login(LoginDTO loginDTO)
        {
            var response = await _loginService.Login(loginDTO);
            return response;
        }

        //Pass
        [HttpPost]
        public async Task<PassDTO> CreatePass(PassDTO passDTO)
        {
            var response = await _passService.CreatePass(passDTO);
            //return the response
            return response;
        }

        [HttpGet]

        public async Task<PassDTO> ReadPassByUId(string UId)
        {
            var response = await _passService.ReadPassByUId(UId);
            return response;
        }

        [HttpPost]
        public async Task<PassDTO> UpdatePassStatus(PassDTO passDTO)
        {
            var response = await _passService.UpdatePassStatus(passDTO);
            return response;
        }

        [HttpGet]
        public async Task<PassDTO> GetVisitorsByStatus(string Status)
        {
            var response = await _passService.GetVisitorsByStatus(Status);
            return response;
        }

    }
}
