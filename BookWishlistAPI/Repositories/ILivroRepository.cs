using BookWishlistAPI.Models.Domain;

namespace BookWishlistAPI.Repositories
{
    public interface ILivroRepository
    {
        Task<List<Livro>> ListarLivrosAsync();
        Task<Livro?> BuscarLivroPorIdAsync(int id);
        Task<Livro> CriarLivroAsync(Livro livro);
        Task<Livro?> AtualizarLivroAsync(int id, Livro livro);
        Task<Livro?> DeletarLivroAsync(int id);
    }
}
