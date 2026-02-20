using BookWishlistAPI.Data;
using BookWishlistAPI.Models.Domain;
using BookWishlistAPI.Models.DTO;
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
            var livrosDomain = _dbContext.Livros.ToList();

            var livrosDto = new List<LivroDTO>();
            foreach (var livroDomain in livrosDomain)
            {
                livrosDto.Add(new LivroDTO()
                {
                    Id = livroDomain.Id,
                    Titulo = livroDomain.Titulo,
                    Autor = livroDomain.Autor,
                    AnoPublicacao = livroDomain.AnoPublicacao,
                    Editora = livroDomain.Editora,
                    Genero = livroDomain.Genero,
                    Preco = livroDomain.Preco,
                    Prioridade = livroDomain.Prioridade,
                    DataAdicao = livroDomain.DataAdicao
                });
            }

            return Ok(livrosDto);
        }

        // Buscar livro por Id
        // GET: https://localhost:portnumber/api/livros/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult BuscarLivroPorId([FromRoute] int id)
        {
            var livroDomain = _dbContext.Livros.FirstOrDefault(livro => livro.Id == id);

            if (livroDomain == null)
            {
                return NotFound();
            }

            var livroDto = new LivroDTO
            {
                Id = livroDomain.Id,
                Titulo = livroDomain.Titulo,
                Autor = livroDomain.Autor,
                AnoPublicacao = livroDomain.AnoPublicacao,
                Editora = livroDomain.Editora,
                Genero = livroDomain.Genero,
                Preco = livroDomain.Preco,
                Prioridade = livroDomain.Prioridade,
                DataAdicao = livroDomain.DataAdicao
            };

            return Ok(livroDto);
        }

        // Criar livro
        // POST: https://localhost:portnumber/api/livros
        [HttpPost]
        public IActionResult CriarLivro([FromBody] RequisicaoCriacaoLivroDTO requisicaoCriacaoLivroDto)
        {
            var livroDomain = new Livro
            {
                Titulo = requisicaoCriacaoLivroDto.Titulo,
                Autor = requisicaoCriacaoLivroDto.Autor,
                AnoPublicacao = requisicaoCriacaoLivroDto.AnoPublicacao,
                Editora = requisicaoCriacaoLivroDto.Editora,
                Genero = requisicaoCriacaoLivroDto.Genero,
                Preco = requisicaoCriacaoLivroDto.Preco,
                Prioridade = requisicaoCriacaoLivroDto.Prioridade,
                DataAdicao = requisicaoCriacaoLivroDto.DataAdicao
            };

            _dbContext.Livros.Add(livroDomain);
            _dbContext.SaveChanges();

            var livroDto = new LivroDTO
            {
                Id = livroDomain.Id,
                Titulo = livroDomain.Titulo,
                Autor = livroDomain.Autor,
                AnoPublicacao = livroDomain.AnoPublicacao,
                Editora = livroDomain.Editora,
                Genero = livroDomain.Genero,
                Preco = livroDomain.Preco,
                Prioridade = livroDomain.Prioridade,
                DataAdicao = livroDomain.DataAdicao
            };

            return CreatedAtAction(nameof(BuscarLivroPorId), new { id = livroDto.Id }, livroDto);
        }

        // Atualizar livro
        // PUT: https://localhost:portnumber/api/livros/{id}
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Atualizacao([FromRoute] int id, [FromBody] RequisicaoAtualizacaoLivroDTO requisicaoAtualizacaoLivroDTO)
        {
            var livroDomain = _dbContext.Livros.FirstOrDefault(livro => livro.Id == id);

            if(livroDomain == null)
            {
                return NotFound();
            }

            livroDomain.Titulo = requisicaoAtualizacaoLivroDTO.Titulo;
            livroDomain.Autor = requisicaoAtualizacaoLivroDTO.Autor;
            livroDomain.AnoPublicacao = requisicaoAtualizacaoLivroDTO.AnoPublicacao;
            livroDomain.Editora = requisicaoAtualizacaoLivroDTO.Editora;
            livroDomain.Genero = requisicaoAtualizacaoLivroDTO.Genero;
            livroDomain.Preco = requisicaoAtualizacaoLivroDTO.Preco;
            livroDomain.Prioridade = requisicaoAtualizacaoLivroDTO.Prioridade;
            livroDomain.DataAdicao = requisicaoAtualizacaoLivroDTO.DataAdicao;

            _dbContext.SaveChanges();

            var livroDto = new LivroDTO
            {
                Id = livroDomain.Id,
                Titulo = livroDomain.Titulo,
                Autor = livroDomain.Autor,
                AnoPublicacao = livroDomain.AnoPublicacao,
                Editora = livroDomain.Editora,
                Genero = livroDomain.Genero,
                Preco = livroDomain.Preco,
                Prioridade = livroDomain.Prioridade,
                DataAdicao = livroDomain.DataAdicao
            };

            return Ok(livroDto);

        }

        // Deletar livro
        // DELETE: https://https://localhost:portnumber/api/livros/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            var livroDomain = _dbContext.Livros.FirstOrDefault(livro => livro.Id == id);

            if(livroDomain == null)
            {
                return NotFound();
            }

            _dbContext.Livros.Remove(livroDomain);
            _dbContext.SaveChanges();

            var livroDto = new LivroDTO
            {
                Id = livroDomain.Id,
                Titulo = livroDomain.Titulo,
                Autor = livroDomain.Autor,
                AnoPublicacao = livroDomain.AnoPublicacao,
                Editora = livroDomain.Editora,
                Genero = livroDomain.Genero,
                Preco = livroDomain.Preco,
                Prioridade = livroDomain.Prioridade,
                DataAdicao = livroDomain.DataAdicao
            };

            return Ok(livroDto);
        }

    }
}
