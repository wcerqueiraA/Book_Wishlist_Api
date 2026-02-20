using BookWishlistAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookWishlistAPI.Data
{
    public class BookWishlistDbContext : DbContext
    {
        public BookWishlistDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {

        }
        
        public DbSet<Livro> Livros { get; set; }
    }
}
