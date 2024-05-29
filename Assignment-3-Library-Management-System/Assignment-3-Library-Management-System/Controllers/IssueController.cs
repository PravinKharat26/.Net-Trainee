using Assignment_3_Library_Management_System.Entities;
using Assignment_3_Library_Management_System.Model;
using Microsoft.AspNetCore.Mvc;
using Container = Microsoft.Azure.Cosmos.Container;
using Microsoft.Azure.Cosmos;
using System;
using Azure;

namespace Assignment_3_Library_Management_System.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class IssueController : Controller
    {
        public Container IssueContainer;
        public Container BookContainer;

        public IssueController()
        {
            IssueContainer = GetContainer("Issue");
            BookContainer = GetContainer("Book");
        }

        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManagement";

        [HttpPost]

        public async Task<IssueModel> AddIssueEntity(IssueModel issueModel)
        {
            BookEntity bookEntity = new BookEntity();

            //create obj of entity and mapp all the fields from model to entity

            IssueEntity issueEntity = new IssueEntity();
            issueEntity.UId = issueModel.UId;
            issueEntity.BookId = issueModel.BookId;
            issueEntity.MemberId = issueModel.MemberId;
            issueEntity.IssueDate = issueModel.IssueDate;
            issueEntity.ReturnDate = issueModel.ReturnDate;
            issueEntity.isReturned = issueModel.isReturned;

            //assign values to mandatory fields

            issueEntity.Id = Guid.NewGuid().ToString();
            issueEntity.UId = issueEntity.Id;
            issueEntity.DocumentType = "issue";
            issueEntity.CreatedOn = DateTime.Now;
            issueEntity.CreatedBy = "pravin";
            issueEntity.UpdatedBy = "";
            issueEntity.UpdatedOn = DateTime.Now;
            issueEntity.Version = 1;
            issueEntity.Active = true;
            issueEntity.Archived = false;

            //add data to db

            IssueEntity response = await IssueContainer.CreateItemAsync(issueEntity);

            //return to model

            IssueModel responseModel = new IssueModel();
            responseModel.UId = response.UId;
            responseModel.BookId = response.BookId;
            responseModel.MemberId = response.MemberId;
            responseModel.IssueDate = response.IssueDate;
            responseModel.ReturnDate = response.ReturnDate;
            responseModel.isReturned = response.isReturned;


            //fetch the existing book record by id

            var existingBook = BookContainer.GetItemLinqQueryable<BookEntity>(true).Where(b => b.Id == response.BookId && b.Active == true && b.Archived == false && b.DocumentType == response.DocumentType).AsEnumerable().FirstOrDefault();

            if (existingBook != null)
            {
                if (response.isReturned == false)
                {
                    // Book is issued
                    existingBook.IsIssued = true;
                }
                else
                {
                    // Book is returned
                    existingBook.IsIssued = false;
                    response.isReturned = true;
                }

                await BookContainer.ReplaceItemAsync(existingBook, existingBook.Id, new PartitionKey(existingBook.DocumentType));
            }

            return responseModel;

        }


        [HttpGet]
        public async Task<IssueModel> GetIssueByUID(string UId)
        {
            var issue = IssueContainer.GetItemLinqQueryable<IssueEntity>(true).Where(i => i.UId == UId && i.Active == true && i.Archived == false).FirstOrDefault();

            IssueModel issueModel = new IssueModel();
            issueModel.UId = issue.UId;
            issueModel.BookId = issue.BookId;
            issueModel.MemberId = issue.MemberId;
            issueModel.IssueDate = issue.IssueDate;
            issueModel.ReturnDate = issue.ReturnDate;
            issueModel.ReturnDate = issue.ReturnDate;
            issueModel.isReturned = issue.isReturned;

            return issueModel;
        }

        [HttpPost]

        public async Task<IssueModel> UpdateIssue(IssueModel issue)
        {
            BookEntity bookEntity = new BookEntity();

            //get the existing records by UId
            var existingIssue = IssueContainer.GetItemLinqQueryable<IssueEntity>(true).Where(b => b.UId == issue.UId && b.Active == true && b.Archived == false).FirstOrDefault();

            //replace the records
            existingIssue.Archived = true;
            existingIssue.Active = false;

            await IssueContainer.ReplaceItemAsync(existingIssue, existingIssue.Id);


            //assign the values for mandatory fields

            existingIssue.Id = Guid.NewGuid().ToString();

            existingIssue.UpdatedBy = "pravin";
            existingIssue.UpdatedOn = DateTime.Now;
            existingIssue.Version = existingIssue.Version + 1;
            existingIssue.Active = true;
            existingIssue.Archived = false;

            //assign the values to the fields which we will get from request obj

            existingIssue.UId = issue.UId;
            existingIssue.BookId = issue.BookId;
            existingIssue.MemberId = issue.MemberId;
            existingIssue.IssueDate = issue.IssueDate;
            existingIssue.ReturnDate = issue.ReturnDate;
            existingIssue.ReturnDate = issue.ReturnDate;
            existingIssue.isReturned = issue.isReturned;



            //add the data to db
            existingIssue = await IssueContainer.CreateItemAsync(existingIssue);

            //return

            IssueModel responseModel = new IssueModel();
            existingIssue.UId = responseModel.UId;
            existingIssue.BookId = responseModel.BookId;
            existingIssue.MemberId = responseModel.MemberId;
            existingIssue.IssueDate = responseModel.IssueDate;
            existingIssue.ReturnDate = responseModel.ReturnDate;
            existingIssue.isReturned = responseModel.isReturned;

            var existingBook = BookContainer.GetItemLinqQueryable<BookEntity>(true).Where(b => b.Id == existingIssue.BookId && b.Active == true && b.Archived == false && b.DocumentType == existingIssue.DocumentType).AsEnumerable().FirstOrDefault();

            if (existingBook != null)
            {
                if (existingIssue.isReturned == false)
                {
                    // Book is issued
                    existingBook.IsIssued = true;
                }
                else
                {
                    // Book is returned
                    existingBook.IsIssued = false;
                    existingIssue.isReturned = true;
                }

                await BookContainer.ReplaceItemAsync(existingBook, existingBook.Id, new PartitionKey(existingBook.DocumentType));
            }

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
