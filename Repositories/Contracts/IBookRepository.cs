using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IBookRepository:IRepositoryBase<Book>
    {
        public void CreateOneBook(Book book);
        public void UpdateOneBook(Book book);
        public void DeleteOneBook(Book book);
        public IQueryable<Book> GetAllBooks(bool changeTracker);
        public Book GetOneBookById(int id,bool changeTracker);
    }
}
