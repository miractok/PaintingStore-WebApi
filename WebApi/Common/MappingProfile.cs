using AutoMapper;
using WebApi.Entities;
using WebApi.Application.PaintingOperations.Queries.GetPaintings;
using WebApi.Application.PaintingOperations.Queries.GetPaintingDetails;
using WebApi.Application.PaintingOperations.Commands.CreatePainting;
using WebApi.Application.ArtistOperations.Commands.CreateArtist;
using WebApi.Application.ArtistOperations.Queries.GetArtists;
using WebApi.Application.ArtistOperations.Queries.GetArtistDetails;
using WebApi.Application.ArtistPaintingOperations.Queries.GetArtistPaintings;
using WebApi.Application.ArtistPaintingOperations.Queries.GetArtistPaintingDetails;
using WebApi.Application.ArtistPaintingOperations.Commands.CreateArtistPainting;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.StyleOperations.Commands.CreateStyle;
using WebApi.Application.StyleOperations.Queries.GetStyleDetails;
using WebApi.Application.StyleOperations.Queries.GetStyles;
using WebApi.Application.MediumOperations.Queries.GetMediums;
using WebApi.Application.MediumOperations.Queries.GetMediumDetails;
using WebApi.Application.MediumOperations.Commands.CreateMedium;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.OrderOperations.Queries.GetOrder;
using WebApi.Application.OrderOperations.Queries.GetOrderDetails;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetails;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Painting Operations Queries Get
            CreateMap<Painting, PaintingsViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate))
                .ForMember(dest => dest.Style, opt => opt.MapFrom(src => src.Style.Name))
                .ForMember(dest => dest.Medium, opt => opt.MapFrom(src => src.Medium.Name))
                .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist.NameSurname));

            //Painting Operations Queries Get Details
            CreateMap<Painting, PaintingViewIdModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate))
                .ForMember(dest => dest.Style, opt => opt.MapFrom(src => src.Style.Name))
                .ForMember(dest => dest.Medium, opt => opt.MapFrom(src => src.Medium.Name))
                .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist.NameSurname));

            //Painting Operations Commands Create Painting
            CreateMap<CreatePaintingModel, Painting>();

            //Artist Operations Queries Get
            CreateMap<Artist, ArtistsViewModel>()
                .ForMember(dest => dest.NameSurname, opt => opt.MapFrom(src => src.NameSurname))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Paintings, opt => opt.MapFrom(m => m.ArtistPainting.Select(s => s.Painting.Name)));

            //Artist Operations Queries Get Details
            CreateMap<Artist, ArtistViewIdModel>()
                .ForMember(dest => dest.NameSurname, opt => opt.MapFrom(src => src.NameSurname))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Paintings, opt => opt.MapFrom(m => m.ArtistPainting.Select(s => s.Painting.Name)));

            //Artist Operations Commands Create Artist
            CreateMap<CreateArtistViewModel, Artist>();

            //ArtistPainting Operations Queries Get
            CreateMap<ArtistPainting, ArtistPaintingViewModel>();

            //ArtistPainting Operations Queries Get Details
            CreateMap<ArtistPainting, ArtistPaintingViewIdModel>();

            //ArtistPainting Operations Commands Create 
            CreateMap<CreateArtistPaintingViewModel, ArtistPainting>();

            //Genre Operations Queries Get
            CreateMap<Genre, GenresViewModel>();

            //Genre Operations Queries Get Details
            CreateMap<Genre, GenresViewIdModel>();

            //Genre Operations Commands Create Genres
            CreateMap<CreateGenreViewModel, Genre>();

            //Style Operations Queries Get
            CreateMap<Style, StylesViewModel>();

            //Style Operations Queries Get Details
            CreateMap<Style, StyleViewIdModel>();

            //Style Operations Commands Create Style
            CreateMap<CreateStyleViewModel, Style>();

            //Medium Operations Queries Get
            CreateMap<Medium, MediumsViewModel>();

            //Medium Operations Queries Get Details
            CreateMap<Medium, MediumViewIdModel>();

            //Medium Operations Commands Create Medium
            CreateMap<CreateMediumViewModel, Medium>();

            //Customer Operations Queries Get Details
            CreateMap<Customer, CustomerViewIdModel>()
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(m => m.Orders.Select(s => s.Painting.Name)));

            //Customer Operations Commands Create
            CreateMap<CreateCustomerModel, Customer>();

            //Order Operations Queries Get
            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.NameSurname, opt => opt.MapFrom(src => src.Customer.NameSurname))
                .ForMember(dest => dest.Painting, opt => opt.MapFrom(src => src.Painting.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Painting.Price))
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate));

            //Order Operations Queries Get Details
            CreateMap<Order, OrderViewIdModel>()
                .ForMember(dest => dest.NameSurname, opt => opt.MapFrom(src => src.Customer.NameSurname))
                .ForMember(dest => dest.Painting, opt => opt.MapFrom(src => src.Painting.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Painting.Price))
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate));

            //Order Operations Commands Create 
            CreateMap<CreateOrderViewModel, Order>();
        }
    }
}