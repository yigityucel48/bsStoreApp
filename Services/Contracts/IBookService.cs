using Entities.DTOs;
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
        public IEnumerable<BookDto> GetAllBooks(bool trackChanges);
        public BookDto GetOneBookById(bool trackChanges, int id);
        public BookDto CreateOneBook(CreateBookDto book);
        public void UpdateOneBook(int id, bool trackChanges, UpdateBookDto bookDto);
        public void DeleteOneBook(int id,bool trackChanges);
    }
}
