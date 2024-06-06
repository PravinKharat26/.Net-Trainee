using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interface
{
    public interface IVisitorService
    {
        
        public Task<VisitorDTO> CreateVisitor(VisitorDTO visitorDTO);
        public Task<VisitorDTO> ReadVisitorByUId(string UId);

        public Task<VisitorDTO> UpdateVisitor(VisitorDTO visitorDTO);

        public  Task<string> DeleteVisitor(string UId);
        
    }
}
