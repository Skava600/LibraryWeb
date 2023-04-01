using FluentValidation;
using LibraryWeb.Contracts.DTO;

namespace LibraryWeb.Core.Validators
{
    internal class CreateOrUpdateBookDTOValidator : AbstractValidator<CreateOrUpdateBookDTO>
    {
        public CreateOrUpdateBookDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Provide a brief description about the book");
            RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required");
            RuleFor(x => x.DateIssued).LessThanOrEqualTo(x => x.DateDue).NotNull();
            RuleFor(x => x.DateDue).NotNull();
            RuleFor(x => x.ISBN).Matches(@"/^\d{13}$");
        }
    }
}
