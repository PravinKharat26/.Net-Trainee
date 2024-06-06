using Microsoft.Azure.Cosmos;
using System.Drawing;
using System.Reflection;
using Visitor_Security_Clearance_System.Common;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entities;
using Visitor_Security_Clearance_System.Interface;

namespace Visitor_Security_Clearance_System.Services
{
    public class VisitorService:IVisitorService
    {
        public readonly ICosmosDBService _cosmosDBService;
        private readonly EmailSender _emailSender;


        public VisitorService(ICosmosDBService cosmosDBService, EmailSender emailSender)
        {
            _cosmosDBService = cosmosDBService;
            _emailSender = emailSender;
        }

        public async Task <VisitorDTO> CreateVisitor(VisitorDTO visitorDTO)
        {
            VisitorEntity visitor = new VisitorEntity
            {
                UId = visitorDTO.UId,
                Name = visitorDTO.Name,
                Email = visitorDTO.Email,
                Password = visitorDTO.Password,
                PhoneNumber = visitorDTO.PhoneNumber,
                Address = visitorDTO.Address,
                VisitingTo = visitorDTO.VisitingTo,
                Purpose = visitorDTO.Purpose,
                EntryTime = visitorDTO.EntryTime,
                ExitTime = visitorDTO.ExitTime
            };

            visitor.Initialize(true, Credentials.VisitorDocumentType, "pravin", "pravin");

            // Check if the email already exists
            var existMail = _cosmosDBService.CheckExistMail(visitor.Email);
            var acceptStatus = await _cosmosDBService.GetVisitorsByStatus(visitor.UId);
            if (existMail != null)
            {
                // Email already exists, send an email notification
                string fromEmail ="tidkeshubham10@gmail.com";
                string subject = "Email Already Exists";
                string toEmail = visitor.Email;
                string username = visitor.Name;
                string message = "The email you provided is already registered in our system.";

               
                await _emailSender.SendEmail(fromEmail, subject, toEmail, username, message);
                if (acceptStatus.Status == "Accepted")
                {
                    string fromEmailNew2 = "tidkeshubham10@gmail.com";
                    string subjectNew2 = "Request Accepted";
                    string toEmailNew2 = visitor.Email;
                    string usernameNew2 = visitor.Name;
                    string messageNew2 = "Dear" + visitor.Name + "We Confirmed Your Entry Pass,Pass Status-Accepted";


                    await _emailSender.SendEmail(fromEmailNew2, subjectNew2, toEmailNew2, usernameNew2, messageNew2);
                }


            }

            // Email not exist,createnew visitor

            var response = await _cosmosDBService.CreateVisitor(visitor);
            var accept=await _cosmosDBService.GetVisitorsByStatus(visitor.UId);


            //email after visitor is created
            string fromEmailNew = "tidkeshubham10@gmail.com";
            string subjectNew = "Confirmation for Entry Pass";
            string toEmailNew = visitor.Email;
            string usernameNew = visitor.Name;
            string messageNew = "I hope this message finds you well.\r\n\r\nWe have received a new visitor registration request that requires our approval for an entry pass. We have approved your Request for Entry Pass.";


            await _emailSender.SendEmail(fromEmailNew, subjectNew, toEmailNew, usernameNew, messageNew);

            if (accept.Status == "Accepted")
            {
                string fromEmailNew2 = "tidkeshubham10@gmail.com";
                string subjectNew2 = "Request Accepted";
                string toEmailNew2 = visitor.Email;
                string usernameNew2 = visitor.Name;
                string messageNew2 = "Dear" + visitor.Name + "We Confirmed Your Entry Pass,Pass Status-Accepted";


                await _emailSender.SendEmail(fromEmailNew2, subjectNew2, toEmailNew2, usernameNew2, messageNew2);
            }
            return new VisitorDTO
            {
                UId = response.UId,
                Name = response.Name,
                Email = response.Email,
                PhoneNumber = response.PhoneNumber,
                Address = response.Address,
                VisitingTo = response.VisitingTo,
                Purpose = response.Purpose,
                EntryTime = response.EntryTime,
                ExitTime = response.ExitTime
            };

        }

        public async Task<VisitorDTO> ReadVisitorByUId(string UId)
        {
            var response = await _cosmosDBService.ReadVisitorByUId(UId);

            var responseModel = new VisitorDTO();
            responseModel.UId=response.UId;
            responseModel.Name= response.Name;
            responseModel.Email= response.Email;
            responseModel.PhoneNumber= response.PhoneNumber;
            responseModel.Address= response.Address;
            responseModel.VisitingTo=response.VisitingTo;
            responseModel.Purpose= response.Purpose;
            responseModel.EntryTime=response.EntryTime;
            responseModel.ExitTime=response.ExitTime;

            return responseModel;
        }

        public async Task<VisitorDTO> UpdateVisitor(VisitorDTO visitorDTO)
        {
            var existingVisitor = await _cosmosDBService.ReadVisitorByUId(visitorDTO.UId);

            existingVisitor.Active = false;
            existingVisitor.Archived = true;

            await _cosmosDBService.ReplaceAsync(existingVisitor);

            existingVisitor.Initialize(false, Credentials.VisitorDocumentType, "pravin", "pravin");

            existingVisitor.UId = visitorDTO.UId;
            existingVisitor.Name = visitorDTO.Name;
            existingVisitor.Email = visitorDTO.Email;
            existingVisitor.Password= visitorDTO.Password;  
            existingVisitor.PhoneNumber = visitorDTO.PhoneNumber;
            existingVisitor.Address = visitorDTO.Address;
            existingVisitor.VisitingTo = visitorDTO.VisitingTo;
            existingVisitor.Purpose = visitorDTO.Purpose;
            existingVisitor.EntryTime = visitorDTO.EntryTime;
            existingVisitor.ExitTime=visitorDTO.ExitTime;

            var response = await _cosmosDBService.CreateVisitor(existingVisitor);

            var responseModel = new VisitorDTO
            {
                UId = response.UId,
                Name=response.Name,
                Email=response.Email,
                PhoneNumber=response.PhoneNumber,
                Address=response.Address,
                VisitingTo=response.VisitingTo,
                Purpose=response.Purpose,
                EntryTime=response.EntryTime,
                ExitTime=response.ExitTime,

            };

            return responseModel;

        }

        public async Task<string> DeleteVisitor(string UId)
        {
            var visitor = await _cosmosDBService.ReadVisitorByUId(UId);
            visitor.Active = false;
            visitor.Archived = true;

            await _cosmosDBService.ReplaceAsync(visitor);

            visitor.Initialize(false, Credentials.VisitorDocumentType, "pravin", "pravin");
            visitor.Active = false;

            await _cosmosDBService.CreateVisitor(visitor);

            return "Record deleted succesfully";
        }

       
    }
}
