using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interface
{
    public interface IOfficeService
    {
        Task<OfficeDTO> CreateOffice(OfficeDTO officeDTO);
        Task<OfficeDTO> ReadOfficeByUId(string UId);
        Task<OfficeDTO> UpdateOffice(OfficeDTO officeDTO);
        Task<string> DeleteOffice(string UId);
    }
}
