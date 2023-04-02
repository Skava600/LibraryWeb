using AutoMapper;
using LibraryWeb.Contracts.Data.Entities;
using LibraryWeb.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
