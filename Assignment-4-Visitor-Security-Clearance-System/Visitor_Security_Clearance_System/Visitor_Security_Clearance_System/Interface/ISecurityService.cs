using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interface
{
    public interface ISecurityService
    {
        Task<SecurityDTO> CreateSecurity(SecurityDTO securityDTO);

        Task<SecurityDTO> ReadSecurityByUId (string UId);

        Task<SecurityDTO> UpdateSecurity(SecurityDTO securityDTO);

        Task<string> DeleteSecurity(string uId);
    }
}
