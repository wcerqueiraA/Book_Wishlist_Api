using AutoMapper;
using BookWishlistAPI.CustomActionFilters;
using BookWishlistAPI.Models.Domain;
using BookWishlistAPI.Models.DTO;
using BookWishlistAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookWishlistAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {

        private readonly ILivroRepository _livroRepository;
        private readonly IMapper _mapper;

        public LivrosController(ILivroRepository livroRepository, IMapper mapper)
        {
            _livroRepository = livroRepository;
            _mapper = mapper;
        }

        // Listar todos os Livros
        // GET: https://localhost:portnumber/api/livros
        [HttpGet]
        public async Task<IActionResult> ListarLivrosAsync()
        {
            var livrosDomain = await _livroRepository.ListarLivrosAsync();

            return Ok(_mapper.Map<List<LivroDTO>>(livrosDomain));
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

            return Ok(_mapper.Map<LivroDTO>(livroDomain));
        }

        // Criar livro
        // POST: https://localhost:portnumber/api/livros
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CriarLivroAsync([FromBody] RequisicaoCriacaoLivroDTO requisicaoCriacaoLivroDto)
        {
            var livroDomain = _mapper.Map<Livro>(requisicaoCriacaoLivroDto);

            livroDomain = await _livroRepository.CriarLivroAsync(livroDomain);

            var livroDto = _mapper.Map<LivroDTO>(livroDomain);

            return CreatedAtAction(nameof(BuscarLivroPorIdAsync), new { id = livroDto.Id }, livroDto);
        }

        // Atualizar livro
        // PUT: https://localhost:portnumber/api/livros/{id}
        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> AtualizarLivroAsync([FromRoute] int id, [FromBody] RequisicaoAtualizacaoLivroDTO requisicaoAtualizacaoLivroDTO)
        {

            var livroDomain = _mapper.Map<Livro>(requisicaoAtualizacaoLivroDTO);

            livroDomain =  await _livroRepository.AtualizarLivroAsync(id, livroDomain);

            if(livroDomain == null)
            {
                return NotFound();
            }

            var livroDto = _mapper.Map<LivroDTO>(livroDomain);

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

            var livroDto = _mapper.Map<LivroDTO>(livroDomain);

            return Ok(livroDto);
        }

    }
}
