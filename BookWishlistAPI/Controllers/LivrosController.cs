using BookWishlistAPI.Data;
using BookWishlistAPI.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookWishlistAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly BookWishlistDbContext _dbContext;

        public LivrosController(BookWishlistDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Listar todos os Livros
        // GET: https://localhost:portnumber/api/livros
        [HttpGet]
        public IActionResult ListarLivros()
        {
            var livros = _dbContext.Livros.ToList();


            return Ok();
        }

        // Buscar livro por Id
        // GET: https://localhost:portnumber/api/livros/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult BuscarLivroPorId([FromRoute] int id)
        {
            var livro = _dbContext.Livros.FirstOrDefault(livro => livro.Id == id);

            if(livro == null)
            {
                return NotFound();
            }

            return Ok(livro);
        }
    }
}
