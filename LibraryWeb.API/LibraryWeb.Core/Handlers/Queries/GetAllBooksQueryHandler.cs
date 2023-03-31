using AutoMapper;
using LibraryWeb.Contracts.Data;
using LibraryWeb.Contracts.Data.Repositories;
using LibraryWeb.Contracts.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Core.Handlers.Queries
{

    public class GetAllBooksQuery : IRequest<IEnumerable<BookDTO>>
    {
    }

    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        public GetAllBooksQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BookDTO>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.Books.GetAll());
            var result = _mapper.Map<IEnumerable<BookDTO>>(entities);
            return result;
        }
    }
}
