using BookWishlistAPI.Data;
using BookWishlistAPI.Models.Domain;
using BookWishlistAPI.Models.DTO;
using BookWishlistAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookWishlistAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly BookWishlistDbContext _dbContext;

        private readonly ILivroRepository _livroRepository;

        public LivrosController(BookWishlistDbContext dbContext, ILivroRepository livroRepository)
        {
            _dbContext = dbContext;
            _livroRepository = livroRepository;
        }

        // Listar todos os Livros
        // GET: https://localhost:portnumber/api/livros
        [HttpGet]
        public async Task<IActionResult> ListarLivrosAsync()
        {
            var livrosDomain = await _livroRepository.ListarLivrosAsync();

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
        public async Task<IActionResult> BuscarLivroPorIdAsync([FromRoute] int id)
        {
            var livroDomain = await _livroRepository.BuscarLivroPorIdAsync(id);

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
        public async Task<IActionResult> CriarLivroAsync([FromBody] RequisicaoCriacaoLivroDTO requisicaoCriacaoLivroDto)
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

            livroDomain = await _livroRepository.CriarLivroAsync(livroDomain);

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

            return CreatedAtAction(nameof(BuscarLivroPorIdAsync), new { id = livroDto.Id }, livroDto);
        }

        // Atualizar livro
        // PUT: https://localhost:portnumber/api/livros/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> AtualizarLivroAsync([FromRoute] int id, [FromBody] RequisicaoAtualizacaoLivroDTO requisicaoAtualizacaoLivroDTO)
        {

            var livroDomain = new Livro
            {
                Titulo = requisicaoAtualizacaoLivroDTO.Titulo,
                Autor = requisicaoAtualizacaoLivroDTO.Autor,
                AnoPublicacao = requisicaoAtualizacaoLivroDTO.AnoPublicacao,
                Editora = requisicaoAtualizacaoLivroDTO.Editora,
                Genero = requisicaoAtualizacaoLivroDTO.Genero,
                Preco = requisicaoAtualizacaoLivroDTO.Preco,
                Prioridade = requisicaoAtualizacaoLivroDTO.Prioridade,
                DataAdicao = requisicaoAtualizacaoLivroDTO.DataAdicao
            };

            livroDomain =  await _livroRepository.AtualizarLivroAsync(id, livroDomain);

            if(livroDomain == null)
            {
                return NotFound();
            }

            await _dbContext.SaveChangesAsync();

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
        public async Task<IActionResult> DeletarLivroAsync([FromRoute] int id)
        {
            var livroDomain = await _livroRepository.DeletarLivroAsync(id);

            if(livroDomain == null)
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

    }
}
