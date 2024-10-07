using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBookService
    {
        public IEnumerable<Book> GetAllBooks(bool trackChanges);
        public Book GetOneBookById(bool trackChanges, int id);
        public Book CreateOneBook(Book book);
        public void UpdateOneBook(int id,bool trackChanges, Book book);
        public void DeleteOneBook(int id,bool trackChanges);
    }
}
