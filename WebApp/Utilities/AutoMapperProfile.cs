using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace WebApp.Utilities
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            BookMap();
        }
        private void BookMap()
        {
            CreateMap<UpdateBookDto, Book>().ReverseMap();
            CreateMap<CreateBookDto, Book>().ReverseMap();
            CreateMap<BookDto, Book>().ReverseMap();
        }
    }
}
