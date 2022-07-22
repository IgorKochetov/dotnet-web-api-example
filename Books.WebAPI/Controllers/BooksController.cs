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
            throw new NotImplementedException();
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task Post([FromBody] Book value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Book value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
