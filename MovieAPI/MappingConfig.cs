using AutoMapper;
using MovieAPI.Models;
using MovieAPI.Models.Dto;

namespace MovieAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<MovieDto, Movie>();
                config.CreateMap<Movie, MovieDto>();
            });
            return mappingConfig;
        }
    }
}
