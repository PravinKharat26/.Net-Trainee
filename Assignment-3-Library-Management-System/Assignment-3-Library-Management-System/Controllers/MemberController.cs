using Assignment_3_Library_Management_System.Entities;
using Assignment_3_Library_Management_System.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Assignment_3_Library_Management_System.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class MemberController : Controller
    {
        public Container MemberContainer;

        public MemberController()
        {
            MemberContainer = GetContainer("Member");
        }

        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManagement";
        

        [HttpPost]

        public async Task<MemberModel> AddNewMember(MemberModel memberModel)
        {
            MemberEntity member = new MemberEntity();
            member.UId = memberModel.UId;
            member.Name = memberModel.Name;
            member.DateOfBirth = memberModel.DateOfBirth;
            member.Email = memberModel.Email;

            member.Id = Guid.NewGuid().ToString();
            member.UId = member.Id;
            member.DocumentType = "member";
            member.CreatedOn = DateTime.Now;
            member.CreatedBy = "pravin";
            member.UpdatedBy = "";
            member.UpdatedOn = DateTime.Now;
            member.Version = 1;
            member.Active = true;
            member.Archived = false;

            MemberEntity response = await MemberContainer.CreateItemAsync(member);

            MemberModel responseModel = new MemberModel();

            responseModel.UId = member.UId;
            responseModel.Name = member.Name;
            responseModel.DateOfBirth = member.DateOfBirth;
            responseModel.Email = member.Email;

            return responseModel;

        }

        [HttpGet]

        public async Task<MemberModel> GetMemberByUID(string UId)
        {
            var member = MemberContainer.GetItemLinqQueryable<MemberEntity>(true).Where(m => m.UId == UId && m.Active == true && m.Archived == false).FirstOrDefault();

            MemberModel memberModel = new MemberModel();
            memberModel.UId = member.UId;
            memberModel.Name = member.Name;
            memberModel.DateOfBirth = member.DateOfBirth;
            memberModel.Email = member.Email;

            return memberModel;
        }

        [HttpGet]

        public async Task<List<MemberModel>> GetAllMembers()
        {
            //fetch records
            var members = MemberContainer.GetItemLinqQueryable<MemberEntity>(true).Where
                (b => b.Active == true && b.Archived == false && b.DocumentType == "member").ToList();

            //map the fields to model
            List<MemberModel> memberModels = new List<MemberModel>();

            foreach (var member in members)
            {
                MemberModel model = new MemberModel();
                model.UId = member.UId;
                model.Name = member.Name;
                model.DateOfBirth = member.DateOfBirth;
                model.Email = member.Email;

                memberModels.Add(model);

            }

            //return

            return memberModels;
        }

        [HttpPost]

        public async Task<MemberModel> UpdateMember(MemberModel member)
        {
            //get the existing records by UId
            var existingMember = MemberContainer.GetItemLinqQueryable<MemberEntity>(true).Where(b => b.UId == member.UId && b.Active == true && b.Archived == false).FirstOrDefault();

            //replace the records
            existingMember.Archived = true;
            existingMember.Active = false;

            await MemberContainer.ReplaceItemAsync(existingMember, existingMember.Id);


            //assign the values for mandatory fields

            existingMember.Id = Guid.NewGuid().ToString();
            existingMember.UpdatedBy = "pravin";
            existingMember.UpdatedOn = DateTime.Now;
            existingMember.Version = existingMember.Version + 1;
            existingMember.Active = true;
            existingMember.Archived = false;

            //assign the values to the fields which we will get from request obj

            existingMember.Name = member.Name;
            existingMember.DateOfBirth = member.DateOfBirth;
            existingMember.Email = member.Email;



            //add the data to db
            existingMember = await MemberContainer.CreateItemAsync(existingMember);

            //return

            MemberModel responseModel = new MemberModel();
            existingMember.UId = responseModel.UId;
            existingMember.Name = responseModel.Name;
            existingMember.DateOfBirth = responseModel.DateOfBirth;
            existingMember.Email = responseModel.Email;

            return responseModel;
        }

        private Container GetContainer(string containerName)
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(containerName);
            return container;
        }
    }
}

