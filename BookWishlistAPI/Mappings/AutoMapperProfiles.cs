using AutoMapper;
using BookWishlistAPI.Models.Domain;
using BookWishlistAPI.Models.DTO;

namespace BookWishlistAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Livro, LivroDTO>().ReverseMap();
            CreateMap<RequisicaoCriacaoLivroDTO, Livro>().ReverseMap();
            CreateMap<RequisicaoAtualizacaoLivroDTO, Livro>().ReverseMap();
        }
    }
}
