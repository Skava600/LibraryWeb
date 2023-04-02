using AutoMapper;
using FluentValidation;
using LibraryWeb.Contracts.Data;
using LibraryWeb.Contracts.DTO;
using LibraryWeb.Core.Exceptions;
using MediatR;

namespace LibraryWeb.Core.Handlers.Commands
{
    public class UpdateBookCommand :IRequest<BookDTO>
    {
        public int Id { get; set; }
        public CreateOrUpdateBookDTO Model{ get; set; }

        public UpdateBookCommand(CreateOrUpdateBookDTO model, int id) 
        {
            Model = model;
            Id = id;
        }
    }
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDTO>
    {
        private readonly IUnitOfWork _repo;
        private readonly IValidator<CreateOrUpdateBookDTO> _validator;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IUnitOfWork repo, IValidator<CreateOrUpdateBookDTO> validator, IMapper mapper)
        {
            _repo = repo;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<BookDTO> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var id = request.Id;

            var validationResult = _validator.Validate(model);
       

            if (!validationResult.IsValid)
            {
                throw new InvalidRequestBodyException
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray()
                };
            }

            var entity = _repo.Books.Get(request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException($"No Item found for the Id {id}");
            }

            entity.ISBN = model.ISBN;
            entity.DateDue = model.DateDue;
            entity.Description = model.Description;
            entity.Name = model.Name;
            entity.DateIssued = model.DateIssued;
            entity.Genre = model.Genre;
            entity.Author = model.Author;

            _repo.Books.Update(entity);
            await _repo.CommitAsync();

            var updatedBook = _mapper.Map<BookDTO>(entity);

            return updatedBook;
        }
    }
}
