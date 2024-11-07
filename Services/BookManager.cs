using AutoMapper;
using Entities.DTOs;
using Entities.Exceptions;
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
        private readonly IMapper _mapper;
        public BookManager(IRepositoryManager repositoryManager, ILoggerService loggerService,IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerService;
            _mapper= mapper;
        }
        public BookDto CreateOneBook(CreateBookDto bookDto)
        {   
            var entity = _mapper.Map<Book>(bookDto);
            _repositoryManager.Book.CreateOneBook(entity);
            _repositoryManager.Save();
            return _mapper.Map<BookDto>(entity);
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            var entity = _repositoryManager.Book.GetOneBookById(id, false);
            if (entity is null)
            {
                throw new BookNotFoundException(id);
            }
            _repositoryManager.Book.DeleteOneBook(entity);
            _repositoryManager.Save();
        }

        public IEnumerable<BookDto> GetAllBooks(bool trackChanges)
        {
            var books = _repositoryManager.Book.GetAllBooks(trackChanges); //dbden gelen datayı 
           return _mapper.Map<IEnumerable<BookDto>>(books);// dtoya mapliyor.

        }

        public BookDto GetOneBookById(bool trackChanges, int id)
        {
            var book = _repositoryManager.Book.GetOneBookById(id, false);
            if (book is null)
            {
                throw new BookNotFoundException(id);
            }
            return _mapper.Map<BookDto>(book);
        }

        public void UpdateOneBook(int id, bool trackChanges, UpdateBookDto bookDto)
        {
            var entity = _repositoryManager.Book.GetOneBookById(id, trackChanges);

            if (entity is null)
            {
                throw new BookNotFoundException(id);
            }
            //entity = _mapper.Map(bookDto,entity);
            entity = _mapper.Map<Book>(bookDto);
            _repositoryManager.Book.Update(entity);
            _repositoryManager.Save();
        }
    }
}
