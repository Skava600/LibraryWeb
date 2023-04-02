using AutoMapper;
using FluentValidation;
using LibraryWeb.Contracts.Data;
using LibraryWeb.Contracts.Data.Entities;
using LibraryWeb.Contracts.DTO;
using LibraryWeb.Core.Exceptions;
using MediatR;

namespace LibraryWeb.Core.Handlers.Commands
{

    public class CreateBookCommand : IRequest<BookDTO>
    {
        public CreateOrUpdateBookDTO Model { get; }

        public CreateBookCommand(CreateOrUpdateBookDTO model)
        {
            Model = model;
        }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookDTO>
    {
        private readonly IUnitOfWork _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrUpdateBookDTO> _validator;
        public CreateBookCommandHandler(IUnitOfWork repo, IMapper mapper, IValidator<CreateOrUpdateBookDTO> validator)
        {
            _repo = repo;
            this._mapper = mapper;
            _validator = validator;
        }

        public async Task<BookDTO> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            CreateOrUpdateBookDTO model = request.Model;

            var result = await _validator.ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new InvalidRequestBodyException
                {
                    Errors = result.Errors.Select(e => e.ErrorMessage).ToArray()
                };
            }

            var entity = _mapper.Map<Book>(model);

            _repo.Books.Add(entity);

            await _repo.CommitAsync();

            return _mapper.Map<BookDTO>(entity);
        }
    }
}
