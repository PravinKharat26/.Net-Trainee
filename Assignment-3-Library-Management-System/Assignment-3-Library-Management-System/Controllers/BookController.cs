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
    public class BookController : Controller
    {


        public Container BookContainer;
        public Container IssueContainer;

        public BookController()
        {
            BookContainer = GetContainer("Book");

            IssueContainer = GetContainer("Issue");
        }

        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManagement";

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

            BookEntity response = await BookContainer.CreateItemAsync(book);

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
            var book = BookContainer.GetItemLinqQueryable<BookEntity>(true).Where(b => b.UId == UId && b.Active == true && b.Archived == false).FirstOrDefault();

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
            var book = BookContainer.GetItemLinqQueryable<BookEntity>(true).Where(b => b.Title == Title && b.Active == true && b.Archived == false).FirstOrDefault();

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
            var books = BookContainer.GetItemLinqQueryable<BookEntity>(true).Where
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
            var books = BookContainer.GetItemLinqQueryable<BookEntity>(true).Where
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
            var books = BookContainer.GetItemLinqQueryable<BookEntity>(true).Where
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
            var existingBook = BookContainer.GetItemLinqQueryable<BookEntity>(true).Where(b => b.UId == book.UId && b.Active == true && b.Archived == false).FirstOrDefault();

            //replace the records
            existingBook.Archived = true;
            existingBook.Active = false;

            await BookContainer.ReplaceItemAsync(existingBook, existingBook.Id);


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
            existingBook = await BookContainer.CreateItemAsync(existingBook);

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



        private Container GetContainer(string containerName)
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(containerName);
            return container;
        }
    }
}
