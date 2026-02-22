using AutoMapper;
using BookWishlistAPI.Controllers;
using BookWishlistAPI.Models.Domain;
using BookWishlistAPI.Models.DTO;
using BookWishlistAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookWishlistTest;

[TestFixture]
public class LivrosControllerTest
{
    private Mock<ILivroRepository> _livroRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private LivrosController _controller;

    [SetUp]
    public void Setup()
    {
        _livroRepositoryMock = new Mock<ILivroRepository>();
        _mapperMock = new Mock<IMapper>();

        _controller = new LivrosController(
            _livroRepositoryMock.Object,
            _mapperMock.Object);
    }

    [Test]
    public async Task ListarLivrosAsync_DeveRetornarOk_ComListaDeLivros()
    {
        var livrosDomain = new List<Livro> { new Livro { Id = 1, Titulo = "Teste" } };
        var livrosDto = new List<LivroDTO> { new LivroDTO { Id = 1, Titulo = "Teste" } };

        _livroRepositoryMock.Setup(r => r.ListarLivrosAsync())
            .ReturnsAsync(livrosDomain);

        _mapperMock.Setup(m => m.Map<List<LivroDTO>>(livrosDomain))
            .Returns(livrosDto);

        var resultado = await _controller.ListarLivrosAsync();

        Assert.IsInstanceOf<OkObjectResult>(resultado);

        var okResult = resultado as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(livrosDto));
    }

    [Test]
    public async Task BuscarLivroPorIdAsync_DeveRetornarOk_QuandoLivroExiste()
    {
        var livroDomain = new Livro { Id = 1, Titulo = "Teste" };
        var livroDto = new LivroDTO { Id = 1, Titulo = "Teste" };

        _livroRepositoryMock.Setup(r => r.BuscarLivroPorIdAsync(1))
            .ReturnsAsync(livroDomain);

        _mapperMock.Setup(m => m.Map<LivroDTO>(livroDomain))
            .Returns(livroDto);

        var resultado = await _controller.BuscarLivroPorIdAsync(1);

        Assert.IsInstanceOf<OkObjectResult>(resultado);
    }

    [Test]
    public async Task BuscarLivroPorIdAsync_DeveRetornarNotFound_QuandoNaoExiste()
    {
        _livroRepositoryMock.Setup(r => r.BuscarLivroPorIdAsync(1))
            .ReturnsAsync(null as Livro);

        var resultado = await _controller.BuscarLivroPorIdAsync(1);

        Assert.IsInstanceOf<NotFoundResult>(resultado);
    }

    [Test]
    public async Task CriarLivroAsync_DeveRetornarCreatedAtAction()
    {
        var requisicao = new RequisicaoCriacaoLivroDTO();
        var livroDomain = new Livro { Id = 1 };
        var livroDto = new LivroDTO { Id = 1 };

        _mapperMock.Setup(m => m.Map<Livro>(requisicao))
            .Returns(livroDomain);

        _livroRepositoryMock.Setup(r => r.CriarLivroAsync(livroDomain))
            .ReturnsAsync(livroDomain);

        _mapperMock.Setup(m => m.Map<LivroDTO>(livroDomain))
            .Returns(livroDto);

        var resultado = await _controller.CriarLivroAsync(requisicao);

        Assert.IsInstanceOf<CreatedAtActionResult>(resultado);
    }

    [Test]
    public async Task AtualizarLivroAsync_DeveRetornarOk_QuandoAtualiza()
    {
        var requisicao = new RequisicaoAtualizacaoLivroDTO();
        var livroDomain = new Livro { Id = 1 };
        var livroDto = new LivroDTO { Id = 1 };

        _mapperMock.Setup(m => m.Map<Livro>(requisicao))
            .Returns(livroDomain);

        _livroRepositoryMock.Setup(r => r.AtualizarLivroAsync(1, livroDomain))
            .ReturnsAsync(livroDomain);

        _mapperMock.Setup(m => m.Map<LivroDTO>(livroDomain))
            .Returns(livroDto);

        var resultado = await _controller.AtualizarLivroAsync(1, requisicao);

        Assert.IsInstanceOf<OkObjectResult>(resultado);
    }

    [Test]
    public async Task AtualizarLivroAsync_DeveRetornarNotFound_QuandoNaoExiste()
    {
        var requisicao = new RequisicaoAtualizacaoLivroDTO();
        var livroDomain = new Livro();

        _mapperMock.Setup(m => m.Map<Livro>(requisicao))
            .Returns(livroDomain);

        _livroRepositoryMock.Setup(r => r.AtualizarLivroAsync(1, livroDomain))
            .ReturnsAsync(null as Livro);

        var resultado = await _controller.AtualizarLivroAsync(1, requisicao);

        Assert.IsInstanceOf<NotFoundResult>(resultado);
    }

    [Test]
    public async Task DeletarLivroAsync_DeveRetornarOk_QuandoRemove()
    {
        var livroDomain = new Livro { Id = 1 };
        var livroDto = new LivroDTO { Id = 1 };

        _livroRepositoryMock.Setup(r => r.DeletarLivroAsync(1))
            .ReturnsAsync(livroDomain);

        _mapperMock.Setup(m => m.Map<LivroDTO>(livroDomain))
            .Returns(livroDto);

        var resultado = await _controller.DeletarLivroAsync(1);

        Assert.IsInstanceOf<OkObjectResult>(resultado);
    }

    [Test]
    public async Task DeletarLivroAsync_DeveRetornarNotFound_QuandoNaoExiste()
    {
        _livroRepositoryMock.Setup(r => r.DeletarLivroAsync(1))
            .ReturnsAsync(null as Livro);

        var resultado = await _controller.DeletarLivroAsync(1);

        Assert.IsInstanceOf<NotFoundResult>(resultado);
    }
}
