using System.ComponentModel.DataAnnotations;

namespace BookWishlistAPI.Models.DTO
{
    public class RequisicaoAtualizacaoLivroDTO
    {
        [Required]
        [MinLength(1, ErrorMessage = "Code has to be minimum of 1 character")]
        public string Titulo { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Code has to be minimum of 6 characters")]
        public string Autor { get; set; }
        [Required]
        public int AnoPublicacao { get; set; }
        [Required]
        public string Editora { get; set; }
        [Required]
        public string Genero { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        public string Prioridade { get; set; }
        [Required]
        public DateTime DataAdicao { get; set; }
    }
}
