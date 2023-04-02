using AutoMapper;
using LibraryWeb.Contracts.Data;
using LibraryWeb.Contracts.DTO;
using LibraryWeb.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Core.Handlers.Queries
{
    public class GetBookByIdQuery : IRequest<BookDTO>
    {
        public int BookId { get; }
        public GetBookByIdQuery(int id)
        {
            BookId = id;
        }
    }
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDTO>
    {

        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        public GetBookByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookDTO> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await Task.FromResult(_repository.Books.Get(request.BookId));
            if (book == null)
            {
                throw new EntityNotFoundException($"No Item found for Id {request.BookId}");
            }
            
            var result = _mapper.Map<BookDTO>(book);

            return result;
        }
    }
}
