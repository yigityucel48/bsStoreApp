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
        public BookManager(IRepositoryManager repositoryManager)
        {
                _repositoryManager = repositoryManager;
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
                throw new Exception($"Book id:{id} is could not found");
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
                throw new Exception($"Book id:{id} is could not found");
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
