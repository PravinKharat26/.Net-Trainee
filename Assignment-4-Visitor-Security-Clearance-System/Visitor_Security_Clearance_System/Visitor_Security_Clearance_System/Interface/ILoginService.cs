using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interface
{
    public interface ILoginService
    {
        Task<string> Login(LoginDTO loginDTO);
    }
}
