using AutoMapper;
using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.DTO.RequestDTO;
using HUUTRUNG.Models.DTO.ResponseDTO;

namespace HUUTRUNG_WEBAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Series, SeriesDTO>().ReverseMap();
            CreateMap<Series, AddSeriesRequestDTO>().ReverseMap();
            CreateMap<Series, UpdateSeriesRequestDTO>().ReverseMap();

            CreateMap<Comic, ComicDTO>().ReverseMap();
            CreateMap<AddComicRequestDTO,Comic >().ReverseMap();
            CreateMap<Comic, UpdateComicRequestDTO>().ReverseMap();
            CreateMap<Bookmark, BookmarkRequestDTO>().ReverseMap();
            //CreateMap<Comment, CommentRequestDTO>().ReverseMap();
            //CreateMap<Comment, CommentResponseDTO>().ReverseMap();
            CreateMap<Rating, RatingRequestDTO>().ReverseMap();

			CreateMap<Comment, CommentResponseDTO>()
		   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email)) // Lấy Email từ ApplicationUser
		   .ReverseMap();

		}
    }
}
       //.ForMember(dest => dest.Pages, opt => opt.MapFrom(src => src.Pages))
       //     .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
       //     .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings))