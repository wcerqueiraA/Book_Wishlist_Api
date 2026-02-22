using BookWishlistAPI.Data;
using BookWishlistAPI.Models.Domain;
using BookWishlistAPI.Repositories;
using Microsoft.EntityFrameworkCore;


namespace BookWishlistTest
{
    public class SQLLivroRepositoryTests
    {
        private BookWishlistDbContext _context;
        private SQLLivroRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BookWishlistDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new BookWishlistDbContext(options);
            _repository = new SQLLivroRepository(_context);
        }



        private Livro CriarLivroFake()
        {
            return new Livro
            {
                Titulo = "Clean Code",
                Autor = "Robert C. Martin",
                AnoPublicacao = 2008,
                Editora = "Prentice Hall",
                Genero = "Tecnologia",
                Preco = 120,
                Prioridade = "Baixa",
                DataAdicao = DateTime.UtcNow
            };
        }

        [Test]
        public async Task CriarLivroAsync_DeveCriarLivro()
        {
            var livro = CriarLivroFake();

            var resultado =  await _repository.CriarLivroAsync(livro);

            Assert.IsNotNull(resultado);
            Assert.That(_context.Livros.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task BuscarLivroPorIdAsync_DeveRetornarLivro_QuandoExistir()
        {
            var livro = await _repository.CriarLivroAsync(CriarLivroFake());

            var resultado = await _repository.BuscarLivroPorIdAsync(livro.Id);

            Assert.IsNotNull(resultado);
            Assert.That(resultado.Titulo, Is.EqualTo("Clean Code"));
        }

        [Test]
        public async Task BuscarLivroPorIdAsync_DeveRetornarNull_QuandoNaoExistir()
        {
            var resultado = await _repository.BuscarLivroPorIdAsync(999);

            Assert.IsNull(resultado);
        }

        [Test]
        public async Task ListarLivrosAsync_DeveRetornarListaDeLivros()
        {
            await _repository.CriarLivroAsync(CriarLivroFake());
            await _repository.CriarLivroAsync(CriarLivroFake());

            var resultado = await _repository.ListarLivrosAsync();

            Assert.That(resultado.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task AtualizarLivroAsync_DeveAtualizarLivro_QuandoExistir()
        {
            var livro = await _repository.CriarLivroAsync(CriarLivroFake());

            livro.Titulo = "Clean Code Updated";

            var resultado = await _repository.AtualizarLivroAsync(livro.Id, livro);

            Assert.IsNotNull(resultado);
            Assert.That(resultado.Titulo, Is.EqualTo("Clean Code Updated"));
        }

        [Test]
        public async Task AtualizarLivroAsync_DeveRetornarNull_QuandoNaoExistir()
        {
            var livro = CriarLivroFake();

            var resultado = await _repository.AtualizarLivroAsync(999, livro);

            Assert.IsNull(resultado);
        }

        [Test]
        public async Task DeletarLivroAsync_DeveRemoverLivro_QuandoExistir()
        {
            var livro = await _repository.CriarLivroAsync(CriarLivroFake());

            var resultado = await _repository.DeletarLivroAsync(livro.Id);

            Assert.IsNotNull(resultado);
            Assert.That(_context.Livros.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task DeletarLivroAsync_DeveRetornarNull_QuandoNaoExistir()
        {
            var resultado = await _repository.DeletarLivroAsync(999);

            Assert.IsNull(resultado);
        }



        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

    }
}