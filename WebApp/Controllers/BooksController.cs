using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Repositories;

namespace WebApp.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;
        public BooksController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpGet]
        public IActionResult AllBooks()
        {
            try
            {
                var books = _repositoryContext.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message) ;
            }

        }
        [HttpGet("{id}")]
        public IActionResult BookById([FromRoute(Name ="id")] int id)
        {
            try
            {
                var entity = _repositoryContext.Books.SingleOrDefault(b => b.Id == id);
                if (entity is null)
                {
                    return NotFound();
                }
                return Ok(entity);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }
        [HttpPost]
        public IActionResult AddOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest();
                }
                _repositoryContext.Books.Add(book);
                _repositoryContext.SaveChanges();
                return StatusCode(201, book);
            }
            catch (Exception ex )
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult OneBook([FromRoute(Name ="id")]int id ,[FromBody] Book book)
        {
            try
            {
                var entity = _repositoryContext.Books.SingleOrDefault(b => b.Id == id);
                if (entity is null)
                {
                    return NotFound();
                }
                entity.Title = book.Title;
                entity.Price = book.Price;
                _repositoryContext.SaveChanges();
                return StatusCode(200, book);
            }
            catch (Exception ex )
            {

                throw new Exception(ex.Message);
            }


        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteOneBook([FromRoute(Name ="id")]int id)
        {
            try
            {
                var entity = await _repositoryContext.Books.SingleOrDefaultAsync(b => b.Id == id);
                if(entity is null)
                {
                    return NotFound();
                }
                _repositoryContext.Remove(entity);
               await _repositoryContext.SaveChangesAsync() ;
                return NoContent(); 
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [HttpPatch("{id}")]
        public IActionResult OneBookPatch([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookpatch)
        {
            try
            {
                var entity = _repositoryContext.Books.SingleOrDefault(b => b.Id == id);
                if (entity is null)
                {
                    return NotFound();
                }
                bookpatch.ApplyTo(entity);
                _repositoryContext.SaveChanges();
                return NoContent(); //204
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }
    }
}
