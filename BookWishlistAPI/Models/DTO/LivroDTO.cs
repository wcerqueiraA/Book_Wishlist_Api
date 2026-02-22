using System.ComponentModel.DataAnnotations;

namespace BookWishlistAPI.Models.DTO
{
    public class LivroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnoPublicacao { get; set; }
        public string Editora { get; set; }
        public string Genero { get; set; }
        public decimal Preco { get; set; }
        public string Prioridade { get; set; }
        public DateTime DataAdicao { get; set; }
    }
}
