using WebApp.Controllers;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using WebApp.DAL.Interfaces;
using WebApp.DAL;
using WebApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Test
{
    public class BooksControllerTest
    {
        public Book? book { get; private set; }
        private BooksController booksController;
        private BooksRepository _booksRepository;
        private Result result;
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection();
            services.AddDbContext<DataContext>(options =>
                options.UseInMemoryDatabase("TestDb")
            );
            _serviceProvider = services.BuildServiceProvider();

            var scope = _serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var dbContext = scopedServices.GetRequiredService<DataContext>();

            _booksRepository = new BooksRepository(dbContext);
            booksController = new BooksController(_booksRepository);
            result = new Result();
        }

        [Test]
        public void GetBooks()
        {
            //Act
            result = booksController.Get().Result;

            //Assert
            Assert.IsTrue(result.Success);
        }

        [TestCase(0, "", "", "", "2023-10-1", "", "", "2023-10-1")]
        public void Post_Should_ReturnFalse_When_InputIsInvalid(int id, string name, string subject, string author, DateTime createdOn, string createdBy, string lastModifiedBy, DateTime lastModifiedOn)
        {
            //Arrange
            book = BuildBodyRequest(id, name, subject, author, createdOn, createdBy, lastModifiedBy, lastModifiedOn);

            //Act
            result = booksController.Post(book).Result; 

            //Assert
            Assert.IsFalse(result.Success);
        }

        [TestCase(1, "", "", "", "2023-10-1", "", "", "2023-10-1")]
        public void Post_Should_ReturnFalse_When_IdIsDuplicate(int id, string name, string subject, string author, DateTime createdOn, string createdBy, string lastModifiedBy, DateTime lastModifiedOn)
        {
            //Arrange
            Book bookTest = BuildBodyRequest(1, "Pirates", "Literature", "John", DateTime.Now, "Admin", "", DateTime.Now);
            Result resultTest = booksController.Post(bookTest).Result;
            book = BuildBodyRequest(id, name, subject, author, createdOn, createdBy, lastModifiedBy, lastModifiedOn);

            //Act
            result = booksController.Post(book).Result;

            //Assert
            Assert.IsFalse(result.Success);
        }

        [TestCase(1, "", "", "", "2023-10-1", "", "", "2023-10-1")]
        public void UpdateBook(int id, string name, string subject, string author, DateTime createdOn, string createdBy, string lastModifiedBy, DateTime lastModifiedOn)
        {
            //Arrange
            Book bookTest = BuildBodyRequest(1, "Pirates", "Literature", "John", DateTime.Now, "Admin", "", DateTime.Now);
            Result resultTest = booksController.Post(bookTest).Result;
            book = BuildBodyRequest(id, name, subject, author, createdOn, createdBy, lastModifiedBy, lastModifiedOn);

            //Act
            result = booksController.Put(book).Result;

            //Assert
            Assert.IsTrue(result.Success);
        }

        [TestCase(2, "", "", "", "2023-10-1", "", "", "2023-10-1")]
        public void Update_Should_ReturnFalse_WhenRecordDoesNotExist(int id, string name, string subject, string author, DateTime createdOn, string createdBy, string lastModifiedBy, DateTime lastModifiedOn)
        {
            //Arrange
            Book bookTest = BuildBodyRequest(1, "Pirates", "Literature", "John", DateTime.Now, "Admin", "", DateTime.Now);
            Result resultTest = booksController.Post(bookTest).Result;
            book = BuildBodyRequest(id, name, subject, author, createdOn, createdBy, lastModifiedBy, lastModifiedOn);

            //Act
            result = booksController.Put(book).Result;

            //Assert
            Assert.IsFalse(result.Success);
        }

        [TestCase(1)]
        public void Delete(int id)
        {
            //Arrange
            Book bookTest = BuildBodyRequest(1, "Pirates", "Literature", "John", DateTime.Now, "Admin", "", DateTime.Now);
            Result resultTest = booksController.Post(bookTest).Result;

            //Act
            result = booksController.Delete(id).Result;

            //Assert
            Assert.That(result.Message, Is.EqualTo("Success"));
        }

        [TestCase(2)]
        public void Should_ReturnFalse_When_TheRecordToDeleteDoesNotExist(int id)
        {
            //Arrange
            Book bookTest = BuildBodyRequest(1, "Pirates", "Literature", "John", DateTime.Now, "Admin", "", DateTime.Now);
            Result resultTest = booksController.Post(bookTest).Result;

            //Act
            result = booksController.Delete(id).Result;

            //Assert
            Assert.IsFalse(result.Success);
        }

        [TestCase(1)]
        public void Should_ReturnTrue_When_BooksFoundAgainstUserId(int userId)
        {
            //Arrange
            Book bookTest = BuildBodyRequest(1, "Pirates", "Literature", "John", DateTime.Now, "Admin", "", DateTime.Now, 1);
            Result resultTest = booksController.Post(bookTest).Result;

            //Act
            result = booksController.GetBooksByUserId(userId).Result;

            //Assert
            Assert.IsTrue(result.Success);
        }

        public static Book BuildBodyRequest(int id, string name, string subject, string author, DateTime createdOn, string createdBy, string lastModifiedBy, DateTime lastModifiedOn, int userId=1) 
        {
            return new Book
            {
                Id = id,
                Name = name,
                Subject = subject,
                Author = author,
                CreatedOn = createdOn,
                CreatedBy = createdBy,
                LastModifiedBy = lastModifiedBy,
                LastModifiedOn = lastModifiedOn,
                UserId = userId
            };
        }

        [TearDown]
        public void Cleanup()
        {
            var dbContext = _serviceProvider.GetService<DataContext>();

            dbContext?.Database.EnsureDeleted();
            _serviceProvider.Dispose();
        }
    }
}