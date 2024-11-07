using Entities.DTOs;
using Entities.Exceptions;
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
        var books = _services.BookService.GetAllBooks(false);
        return Ok(books);

    }
    [HttpGet("{id}")]
    public IActionResult BookById([FromRoute(Name = "id")] int id)
    {
        var entity = _services.BookService.GetOneBookById(false, id);
        return Ok(entity);

    }
    [HttpPost]
    public IActionResult AddOneBook([FromBody] CreateBookDto bookDto)
    {
        if (bookDto is null)
        {
            return BadRequest();
        }
       var book = _services.BookService.CreateOneBook(bookDto);
        return StatusCode(201, book);

    }

    [HttpPut("{id}")]
    public IActionResult OneBook([FromRoute(Name = "id")] int id, [FromBody] UpdateBookDto bookDto)
    {
        if (bookDto is null)
        {
            return BadRequest();
        }
        _services.BookService.UpdateOneBook(id, false, bookDto);
        return NoContent();


    }
    [HttpDelete("{id}")]

    public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
    {
        _services.BookService.DeleteOneBook(id, false);
        return NoContent();

    }
    [HttpPatch("{id}")]
    public IActionResult OneBookPatch([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookDto> bookpatch)
    {
        var bookDto = _services.BookService.GetOneBookById(true, id);
        bookpatch.ApplyTo(bookDto);
        _services.BookService.UpdateOneBook(id, false, new UpdateBookDto {Id=bookDto.Id,Price=bookDto.Price,Title=bookDto.Title });
        return NoContent(); //204


    }
}
