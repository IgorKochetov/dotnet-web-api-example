using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Data;
using Books.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksContext _context;
        public BooksController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<List<Book>> Get()
        {
            return await _context.Books.ToListAsync();
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<Book> Get(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task Post([FromBody] Book value)
        {
            _context.Books.Add(value);
            await _context.SaveChangesAsync();
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Book value)
        {
            var bookToEdit = await _context.Books.FindAsync(id);
            if (bookToEdit == null)
            {
                return NotFound($"Book with id {id} is not found");
            }

            // Normally we would implement domain logic to assign/copy all fields
            // or replace object fully
            // This is simplified for the sake of brevity
            bookToEdit.Title = value.Title;
            bookToEdit.NumberOfCopies = value.NumberOfCopies;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var bookToDelete = await _context.Books.FindAsync(id);
            if (bookToDelete == null)
            {
                return NotFound($"Book with id {id} is not found");
            }

            _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
