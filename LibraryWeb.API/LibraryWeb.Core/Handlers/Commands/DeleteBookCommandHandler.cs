using LibraryWeb.Contracts.Data;
using LibraryWeb.Contracts.Data.Repositories;
using LibraryWeb.Contracts.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Core.Handlers.Commands
{
    public class DeleteBookCommand: IRequest<int>
    {
        public int Id { get; set; }
        public DeleteBookCommand(int id)
        { this.Id = id; }
    }
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, int>
    {
        private readonly IUnitOfWork _repo;

        public DeleteBookCommandHandler(IUnitOfWork repo)
        {
            _repo = repo;
        }
        public async Task<int> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            _repo.Books.Delete(request.Id);
            await _repo.CommitAsync();
            return request.Id;
        }
    }
}
