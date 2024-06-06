using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entities;

namespace Visitor_Security_Clearance_System.CosmosDB
{
    public interface ICosmosDBService
    {
       
        public Task<VisitorEntity> CreateVisitor(VisitorEntity visitor);

        public Task<VisitorEntity> ReadVisitorByUId(string UId);
        Task ReplaceAsync(dynamic entity);
        /*public Task<String> CheckEmail(string email);*/

        public Task<SecurityEntity> CreateSecurity(SecurityEntity security);

        public Task<SecurityEntity> ReadSecurityByUId(string UId);
        public Task<ManagerEntity> CreateManager(ManagerEntity manager);

        public Task<ManagerEntity> ReadManagerByUId(string UId);

        public Task<OfficeEntity> CreateOffice(OfficeEntity office);
        public Task<OfficeEntity> ReadOfficeByUId(string UId);

        public Task<string> Login(LoginEntity login);

        public Task CheckExistMail(string email);
        Task<PassEntity> CreatePass(PassEntity pass);
        Task<PassEntity> ReadPassByUId(string UId);
        Task<PassEntity> GetVisitorsByStatus(string Status);
    }
}
