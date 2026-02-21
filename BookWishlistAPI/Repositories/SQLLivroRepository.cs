using BookWishlistAPI.Data;
using BookWishlistAPI.Models.Domain;
using BookWishlistAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BookWishlistAPI.Repositories
{
    public class SQLLivroRepository : ILivroRepository
    {
        private readonly BookWishlistDbContext _dbContext;
        public SQLLivroRepository(BookWishlistDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Livro?> AtualizarLivroAsync(int id, Livro livro)
        {
            var livroExiste = await _dbContext.Livros.FirstOrDefaultAsync(livro => livro.Id == id);
            if (livroExiste == null) 
            {
                return null;
            }

            livroExiste.Titulo = livro.Titulo;
            livroExiste.Autor = livro.Autor;
            livroExiste.AnoPublicacao = livro.AnoPublicacao;
            livroExiste.Editora = livro.Editora;
            livroExiste.Genero = livro.Genero;
            livroExiste.Preco = livro.Preco;
            livroExiste.Prioridade = livro.Prioridade;
            livroExiste.DataAdicao = livro.DataAdicao;

            await _dbContext.SaveChangesAsync();
            return livroExiste;
        }

        public async Task<Livro?> BuscarLivroPorIdAsync(int id)
        {
            return await _dbContext.Livros.FirstOrDefaultAsync(livro => livro.Id == id);
        }

        public async Task<Livro> CriarLivroAsync(Livro livro)
        {
            await _dbContext.Livros.AddAsync(livro);
            await _dbContext.SaveChangesAsync();
            return livro;
            
        }

        public async Task<Livro?> DeletarLivroAsync(int id)
        {
            var livroExiste = await _dbContext.Livros.FirstOrDefaultAsync(livro => livro.Id == id);
            if(livroExiste == null)
            {
                return null;
            }

            _dbContext.Livros.Remove(livroExiste);
            await _dbContext.SaveChangesAsync();
            return livroExiste;
        }

        public async Task<List<Livro>> ListarLivrosAsync()
        {
            return await _dbContext.Livros.ToListAsync();
        }
    }
}
