using AutoMapper;
using LibraryWeb.Contracts.Data;
using LibraryWeb.Contracts.DTO;
using LibraryWeb.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Core.Handlers.Queries
{
    public class GetBookByIsbnQuery : IRequest<BookDTO>
    {
        public string ISBN { get; }
        public GetBookByIsbnQuery(string isbn)
        {
            ISBN = isbn;
        }
    }
    public class GetBookByIsbnQueryHandler : IRequestHandler<GetBookByIsbnQuery, BookDTO> 
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork repo;
        public GetBookByIsbnQueryHandler(IMapper mapper, IUnitOfWork unit)
        {
            this.mapper = mapper;
            this.repo = unit;
        }

        public async Task<BookDTO> Handle(GetBookByIsbnQuery request, CancellationToken cancellationToken)
        {
            var book = await Task.FromResult(repo.Books.GetByIsbn(request.ISBN));
            if (book == null)
            {
                throw new EntityNotFoundException($"No Item found for ISBN {request.ISBN}");
            }

            var result = mapper.Map<BookDTO>(book);

            return result;
        }
    }

}
