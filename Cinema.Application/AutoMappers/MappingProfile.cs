using AutoMapper;
using Cinema.Application.Features.Genre.Commands.Create;
using Cinema.Application.Features.Theater.Commands.Create;
using Cinema.Domain.Enitities;
using SeedWorks.Models.Booking;
using SeedWorks.Models.Genre;
using SeedWorks.Models.Movie;
using SeedWorks.Models.Showtime;
using SeedWorks.Models.Theater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie , CreateMovieDto>().ReverseMap();
            CreateMap<Booking, CreateBookingModel>().ReverseMap();
            CreateMap<Genre, CreateGenreModel>().ReverseMap();
            CreateMap<Genre,UpdateGenreModel>().ReverseMap();
            CreateMap<Movie, CreateMovieDto>().ReverseMap();
            CreateMap<Movie, UpdateMovieModel>().ReverseMap();
            CreateMap<Showtime, CreateShowtimeModel>().ReverseMap();
            CreateMap<Showtime, UpdateShowtimeModel>().ReverseMap();
            CreateMap<Theater, CreateTheaterModel>().ReverseMap();
            CreateMap<Theater, UpdateTheaterModel>().ReverseMap();
        }
    }
}
