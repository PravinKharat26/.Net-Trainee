using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interface
{
    public interface IManagerService
    {
        Task<ManagerDTO> CreateManager(ManagerDTO managerDTO);

        Task<ManagerDTO> ReadManagerByUId(string UId);
        Task<ManagerDTO> UpdateManager(ManagerDTO managerDTO);
        Task<string> DeleteManager(string UId);
    }
}
