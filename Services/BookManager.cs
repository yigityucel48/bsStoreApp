using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerService _logger;
        public BookManager(IRepositoryManager repositoryManager,ILoggerService loggerService)
        {
                _repositoryManager = repositoryManager;
                _logger = loggerService;
        }
        public Book CreateOneBook(Book book)
        {
            _repositoryManager.Book.CreateOneBook(book);
            _repositoryManager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            var entity =_repositoryManager.Book.GetOneBookById(id,false);
            if(entity is null) 
            {
                string message = $"The book with id : {id} could not found";
                _logger.LogInfo(message);
                throw new Exception(message);
            }
             _repositoryManager.Book.DeleteOneBook(entity);
            _repositoryManager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _repositoryManager.Book.GetAllBooks(trackChanges);
            
        }

        public Book GetOneBookById(bool trackChanges, int id)
        {
            return _repositoryManager.Book.GetOneBookById(id, false);
        }

        public void UpdateOneBook(int id, bool trackChanges, Book book)
        {
            var entity = _repositoryManager.Book.GetOneBookById(id, true);

            if (entity is null)
            {
                string message = $"Book id:{id} is could not found";
                _logger.LogInfo(message);
                throw new Exception(message);
            }
            if (book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            entity.Title = book.Title;
            entity.Price = book.Price;
            _repositoryManager.Save();
        }
    }
}
