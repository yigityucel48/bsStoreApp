using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers;

[Route("api/books")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IServiceManager _services;
    public BooksController(IServiceManager services)
    {
        _services = services;
    }
    [HttpGet]
    public IActionResult AllBooks()
    {
        try
        {
            var books = _services.BookService.GetAllBooks(false);
            return Ok(books);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }
    [HttpGet("{id}")]
    public IActionResult BookById([FromRoute(Name = "id")] int id)
    {
        try
        {
            throw new Exception("!!!");
            var entity = _services.BookService.GetOneBookById(false, id);
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
            _services.BookService.CreateOneBook(book);
            return StatusCode(201, book);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }

    [HttpPut("{id}")]
    public IActionResult OneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
    {
        try
        {
            if (book is null)
            {
                return BadRequest();
            }
            _services.BookService.UpdateOneBook(id, true, book);
            return NoContent();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }


    }
    [HttpDelete("{id}")]

    public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
    {
        try
        {
            _services.BookService.DeleteOneBook(id, false);
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
            var entity = _services.BookService.GetOneBookById(true, id);
            if (entity is null)
            {
                return NotFound();
            }
            bookpatch.ApplyTo(entity);
            _services.BookService.UpdateOneBook(id, true, entity);
            return NoContent(); //204
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }


    }
}
