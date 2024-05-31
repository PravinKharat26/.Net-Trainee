using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Microsoft.Azure;
using Microsoft.Azure.Cosmos;
using Container = Microsoft.Azure.Cosmos.Container;
using Assignment_3_Library_Management_System.Entities;
using Assignment_3_Library_Management_System.Model;

namespace Assignment_3_Library_Management_System.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class LibraryController : Controller
    {
        public Container Container;

        public LibraryController()
        {
            Container = GetContainer();
        }

        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManagement";
        public string ContainerName = "Library";

        //Books-

        [HttpPost]

        public async Task<BookModel> AddBooks(BookModel bookModel)
        {
            //create obj of entity and mapp all the fields from model to entity

            BookEntity book = new BookEntity();
            book.UId = bookModel.UId;
            book.Title = bookModel.Title;
            book.Author = bookModel.Author;
            book.PublishedDate = bookModel.PublishedDate;
            book.ISBN = bookModel.ISBN;
            book.IsIssued = bookModel.IsIssued;

            //assign values to mandatory fields

            book.Id = Guid.NewGuid().ToString();
            book.UId = book.Id;
            book.DocumentType = "book";
            book.CreatedOn = DateTime.Now;
            book.CreatedBy = "pravin";
            book.UpdatedBy = "";
            book.UpdatedOn = DateTime.Now;
            book.Version = 1;
            book.Active = true;
            book.Archived = false;

            //add data to db

            BookEntity response = await Container.CreateItemAsync(book);

            //return to model

            BookModel responseModel = new BookModel();
            responseModel.UId = response.UId;
            responseModel.Title = response.Title;
            responseModel.Author = response.Author;
            responseModel.PublishedDate = response.PublishedDate;
            responseModel.ISBN = response.ISBN;
            responseModel.IsIssued = response.IsIssued;

            return responseModel;

        }

        [HttpGet]

        public async Task<BookModel> GetbookByUID(string UId)
        {
            var book = Container.GetItemLinqQueryable<BookEntity>(true).Where(b => b.UId == UId && b.DocumentType=="book" && b.Active == true && b.Archived == false ).FirstOrDefault();

            BookModel bookModel = new BookModel();
            bookModel.UId = book.UId;
            bookModel.Title = book.Title;
            bookModel.Author = book.Author;
            bookModel.PublishedDate = book.PublishedDate;
            bookModel.ISBN = book.ISBN;
            bookModel.IsIssued = book.IsIssued;

            return bookModel;
        }

        [HttpGet]

        public async Task<BookModel> GetbookByName(string Title)
        {
            var book = Container.GetItemLinqQueryable<BookEntity>(true).Where(b => b.Title == Title && b.DocumentType == "book" && b.Active == true && b.Archived == false).FirstOrDefault();

            BookModel bookModel = new BookModel();
            bookModel.UId = book.UId;
            bookModel.Title = book.Title;
            bookModel.Author = book.Author;
            bookModel.PublishedDate = book.PublishedDate;
            bookModel.ISBN = book.ISBN;
            bookModel.IsIssued = book.IsIssued;

            return bookModel;
        }


        [HttpGet]

        public async Task<List<BookModel>> GetAllBooks()
        {
            //fetch records
            var books = Container.GetItemLinqQueryable<BookEntity>(true).Where
                (b => b.Active == true && b.Archived == false && b.DocumentType == "book").ToList();

            //map the fields to model
            List<BookModel> bookModels = new List<BookModel>();

            foreach (var book in books)
            {
                BookModel model = new BookModel();
                model.UId = book.UId;
                model.Title = book.Title;
                model.Author = book.Author;
                model.PublishedDate = book.PublishedDate;
                model.ISBN = book.ISBN;
                model.IsIssued = book.IsIssued;

                bookModels.Add(model);

            }

            //return

            return bookModels;
        }

        [HttpGet]

        public async Task<List<BookModel>> GetAllNotIssuedBooks()
        {
            //fetch records
            var books = Container.GetItemLinqQueryable<BookEntity>(true).Where
                (b => b.Active == true && b.Archived == false && b.IsIssued == false && b.DocumentType == "book").ToList();

            //map the fields to model
            List<BookModel> bookModels = new List<BookModel>();

            foreach (var book in books)
            {
                BookModel model = new BookModel();
                model.UId = book.UId;
                model.Title = book.Title;
                model.Author = book.Author;
                model.PublishedDate = book.PublishedDate;
                model.ISBN = book.ISBN;
                model.IsIssued = book.IsIssued;

                bookModels.Add(model);

            }

            //return

            return bookModels;
        }


        [HttpGet]


        public async Task<List<BookModel>> GetAllIssuedBooks()
        {
            //fetch records
            var books = Container.GetItemLinqQueryable<BookEntity>(true).Where
                (b => b.Active == true && b.Archived == false && b.IsIssued == true && b.DocumentType == "book").ToList();

            //map the fields to model
            List<BookModel> bookModels = new List<BookModel>();

            foreach (var book in books)
            {
                BookModel model = new BookModel();
                model.UId = book.UId;
                model.Title = book.Title;
                model.Author = book.Author;
                model.PublishedDate = book.PublishedDate;
                model.ISBN = book.ISBN;
                model.IsIssued = book.IsIssued;

                bookModels.Add(model);

            }

            //return

            return bookModels;
        }

        [HttpPost]

        public async Task<BookModel> UpdateBook(BookModel book)
        {
            //get the existing records by UId
            var existingBook = Container.GetItemLinqQueryable<BookEntity>(true).Where(b => b.UId == book.UId && b.Active == true && b.Archived == false).FirstOrDefault();

            //replace the records
            existingBook.Archived = true;
            existingBook.Active = false;

            await Container.ReplaceItemAsync(existingBook, existingBook.Id);


            //assign the values for mandatory fields

            existingBook.Id = Guid.NewGuid().ToString();
            existingBook.UpdatedBy = "pravin";
            existingBook.UpdatedOn = DateTime.Now;
            existingBook.Version = existingBook.Version + 1;
            existingBook.Active = true;
            existingBook.Archived = false;

            //assign the values to the fields which we will get from request obj

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.ISBN = book.ISBN;
            existingBook.IsIssued = book.IsIssued;

            //add the data to db
            existingBook = await Container.CreateItemAsync(existingBook);

            //return

            BookModel responseModel = new BookModel();
            existingBook.UId = responseModel.UId;
            existingBook.Title = responseModel.Title;
            existingBook.Author = responseModel.Author;
            existingBook.PublishedDate = responseModel.PublishedDate;
            existingBook.ISBN = responseModel.ISBN;
            existingBook.IsIssued = responseModel.IsIssued;

            return responseModel;
        }

        //Member-

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

            MemberEntity response = await Container.CreateItemAsync(member);

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
            var member = Container.GetItemLinqQueryable<MemberEntity>(true).Where(m => m.UId == UId && m.Active == true && m.Archived == false && m.DocumentType=="member").FirstOrDefault();

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
            var members = Container.GetItemLinqQueryable<MemberEntity>(true).Where
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
            var existingMember = Container.GetItemLinqQueryable<MemberEntity>(true).Where(b => b.UId == member.UId && b.Active == true && b.Archived == false && b.DocumentType=="member").FirstOrDefault();

            //replace the records
            existingMember.Archived = true;
            existingMember.Active = false;

            await Container.ReplaceItemAsync(existingMember, existingMember.Id);


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
            existingMember = await Container.CreateItemAsync(existingMember);

            //return

            MemberModel responseModel = new MemberModel();
            existingMember.UId = responseModel.UId;
            existingMember.Name = responseModel.Name;
            existingMember.DateOfBirth = responseModel.DateOfBirth;
            existingMember.Email = responseModel.Email;

            return responseModel;
        }

        //issue-

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

            IssueEntity response = await Container.CreateItemAsync(issueEntity);

            //return to model

            IssueModel responseModel = new IssueModel();
            responseModel.UId = response.UId;
            responseModel.BookId = response.BookId;
            responseModel.MemberId = response.MemberId;
            responseModel.IssueDate = response.IssueDate;
            responseModel.ReturnDate = response.ReturnDate;
            responseModel.isReturned = response.isReturned;


            //fetch the existing book record by id

            var existingBook = Container.GetItemLinqQueryable<BookEntity>(true).Where(b => b.Id == response.BookId && b.Active == true && b.Archived == false && b.DocumentType == response.DocumentType).AsEnumerable().FirstOrDefault();

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

                await Container.ReplaceItemAsync(existingBook, existingBook.Id, new PartitionKey(existingBook.DocumentType));
            }

            return responseModel;

        }


        [HttpGet]
        public async Task<IssueModel> GetIssueByUID(string UId)
        {
            var issue = Container.GetItemLinqQueryable<IssueEntity>(true).Where(i => i.UId == UId && i.Active == true && i.Archived == false && i.DocumentType=="issue").FirstOrDefault();

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
            var existingIssue = Container.GetItemLinqQueryable<IssueEntity>(true).Where(b => b.UId == issue.UId && b.Active == true && b.Archived == false && b.DocumentType=="issue").FirstOrDefault();

            //replace the records
            existingIssue.Archived = true;
            existingIssue.Active = false;

            await Container.ReplaceItemAsync(existingIssue, existingIssue.Id);


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
            existingIssue = await Container.CreateItemAsync(existingIssue);

            //return

            IssueModel responseModel = new IssueModel();
            existingIssue.UId = responseModel.UId;
            existingIssue.BookId = responseModel.BookId;
            existingIssue.MemberId = responseModel.MemberId;
            existingIssue.IssueDate = responseModel.IssueDate;
            existingIssue.ReturnDate = responseModel.ReturnDate;
            existingIssue.isReturned = responseModel.isReturned;

            var existingBook = Container.GetItemLinqQueryable<BookEntity>(true).Where(b => b.Id == existingIssue.BookId && b.Active == true && b.Archived == false && b.DocumentType == existingIssue.DocumentType).AsEnumerable().FirstOrDefault();

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

                await Container.ReplaceItemAsync(existingBook, existingBook.Id, new PartitionKey(existingBook.DocumentType));
            }

            return responseModel;
        }




        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }

    }
}
