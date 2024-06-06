using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interface
{
    public interface IPassService
    {
        Task<PassDTO> CreatePass(PassDTO passDTO);
        Task<PassDTO> ReadPassByUId(string UId);
        Task<PassDTO> UpdatePassStatus(PassDTO passDTO);

        Task<PassDTO> GetVisitorsByStatus(string Status);
    }
}
