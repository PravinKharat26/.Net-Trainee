
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.VisualBasic;
using Visitor_Security_Clearance_System.Common;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entities;
using Visitor_Security_Clearance_System.Services;


namespace Visitor_Security_Clearance_System.CosmosDB
{
    public class CosmosDBService:ICosmosDBService
    {
        public CosmosClient _cosmosClient;
        private readonly Container _container;
        //private readonly EmailSender _emailSender;
        public CosmosDBService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndpoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.databaseName, Credentials.containerName);
            //_emailSender = emailSender;
        }

        public async Task<VisitorEntity> CreateVisitor(VisitorEntity visitor)
        {

                //CheckEmail(visitor.Email);
                var response = await _container.CreateItemAsync(visitor);
                return response;

        }


        public async Task<VisitorEntity> ReadVisitorByUId(string UId)
        {
            var visitor= _container.GetItemLinqQueryable<VisitorEntity>(true).Where(v=>v.Active==true && v.Archived==false && v.UId==UId &&
            v.DocumentType==Credentials.VisitorDocumentType).FirstOrDefault();

            return visitor;
        }

        public async Task ReplaceAsync(dynamic entity)
        {
            var response = await _container.ReplaceItemAsync(entity, entity.Id);

        }

        //security

        public async Task<SecurityEntity> CreateSecurity(SecurityEntity security)
        {
            var response = await _container.CreateItemAsync(security);
            return response;
        }

        public async Task<SecurityEntity> ReadSecurityByUId(string UId)
        {
            var security = _container.GetItemLinqQueryable<SecurityEntity>(true).Where(v => v.Active == true && v.Archived == false && v.UId == UId &&
                  v.DocumentType == Credentials.SecurityDocumentType).FirstOrDefault();

            return security;
        }

        //manager

        public async Task<ManagerEntity> CreateManager(ManagerEntity manager)
        {
            var response = await _container.CreateItemAsync(manager);
            return response;
        }

        public async Task<ManagerEntity> ReadManagerByUId(string UId)
        {
            var manager = _container.GetItemLinqQueryable<ManagerEntity>(true).Where(v => v.Active == true && v.Archived == false && v.UId == UId &&
                  v.DocumentType == Credentials.SecurityDocumentType).FirstOrDefault();

            return manager;
        }

        //office

        public async Task<OfficeEntity> CreateOffice(OfficeEntity office)
        {
            var response = await _container.CreateItemAsync(office);
            return response;
     
        }

        public async Task<OfficeEntity> ReadOfficeByUId(string UId)
        {
            var office = _container.GetItemLinqQueryable<OfficeEntity>(true).Where(v => v.Active == true && v.Archived == false && v.UId == UId &&
                 v.DocumentType == Credentials.OfficeDocumentType).FirstOrDefault();

            return office;
        }

        //login

        public async Task<string> Login(LoginEntity login)
        {
            //check esixsting email
            var email = login.Email;
            var password = login.Password;

            var security =  _container.GetItemLinqQueryable<SecurityEntity>(true).Where(v => v.Active == true &&
            v.Archived == false && v.Email == email && v.Password == password && v.DocumentType == Credentials.SecurityDocumentType).FirstOrDefault();

            var manager = _container.GetItemLinqQueryable<ManagerEntity>(true).Where(v => v.Active == true &&
            v.Archived == false && v.Email == email && v.Password == password && v.DocumentType == Credentials.ManagerDocumentType).FirstOrDefault();
            
            var office = _container.GetItemLinqQueryable<OfficeEntity>(true).Where(v => v.Active == true &&
                        v.Archived == false && v.Email == email && v.Password == password && v.DocumentType == Credentials.OfficeDocumentType).FirstOrDefault();

            if (security != null && security.Password == password)
            {
                return "Login Succesfull";
            }

            else if(manager != null && manager.Password == password)
            {
                return "Login Succesfull";
            }

            else if (office != null && office.Password == password)
            {
                return "Login Succesfull";
            }
            return "Login failed";
        }

        public async Task CheckExistMail(string email)
        {
            var existMail = _container.GetItemLinqQueryable<VisitorEntity>(true).Where(v => v.Active == true && v.Archived == false &&
            v.DocumentType == Credentials.VisitorDocumentType && v.Email == email).FirstOrDefault();
          


        }
        public async Task<PassEntity> CreatePass(PassEntity pass)
        {
            var response = await _container.CreateItemAsync(pass);
            return response;
        }

        public async Task<PassEntity> ReadPassByUId(string UId)
        {
            var pass = _container.GetItemLinqQueryable<PassEntity>(true).Where(v => v.Active == true && v.Archived == false && v.UId == UId &&
                v.DocumentType == Credentials.PassDocumentType).FirstOrDefault();

            return pass;
        }

        public async Task<PassEntity> GetVisitorsByStatus(string Status)
        {
            var pass = _container.GetItemLinqQueryable<PassEntity>(true).Where(v => v.Active == true && v.Archived == false && v.Status == Status &&
                v.DocumentType == Credentials.PassDocumentType).FirstOrDefault();

            return pass;
        }
    }
}
