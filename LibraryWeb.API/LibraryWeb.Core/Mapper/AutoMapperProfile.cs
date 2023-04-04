using AutoMapper;
using LibraryWeb.Contracts.Data.Entities;
using LibraryWeb.Contracts.DTO;

namespace LibraryWeb.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookDTO>();
            CreateMap<CreateOrUpdateBookDTO, Book>();
        }
    }
}
