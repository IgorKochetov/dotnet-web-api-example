using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Domain;
using Microsoft.AspNetCore.Mvc;


namespace Books.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksController()
        {
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IList<Book>> Get()
        {
            throw new NotImplementedException();
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
