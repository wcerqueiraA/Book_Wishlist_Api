using BookWishlistAPI.Models.Domain;

namespace BookWishlistAPI.Repositories
{
    public interface ILivroRepository
    {
        Task<List<Livro>> ListarLivrosAsync();
        Task<Livro?> BuscarLivroPorIdAsync(int id);
        Task CriarLivroAsync();
        Task AtualizarLivroAsync();
        Task DeletarLivroAsync();
    }
}
