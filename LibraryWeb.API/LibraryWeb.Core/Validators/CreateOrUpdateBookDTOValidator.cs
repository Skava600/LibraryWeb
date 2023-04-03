using FluentValidation;
using LibraryWeb.Contracts.DTO;
using System.Security.Cryptography.X509Certificates;

namespace LibraryWeb.Core.Validators
{
    public class CreateOrUpdateBookDTOValidator : AbstractValidator<CreateOrUpdateBookDTO>
    {
        public CreateOrUpdateBookDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Provide a brief description about the book");
            RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required");

            RuleFor(x => x.DateDue)
                .GreaterThanOrEqualTo(x => x.DateIssued ?? DateOnly.MinValue)
                .WithMessage("Date of returning must be greater than or equal to day of taking");
            RuleFor(x => x)
                .Must(x => (x.DateIssued == null && x.DateDue == null) || (x.DateIssued != null && x.DateDue != null))
                .WithMessage("Both dates must be provided or none of them");
            RuleFor(x => x.DateIssued).LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Date of taking must be earlier or equal to today");

            RuleFor(x => x.ISBN).Matches(@"^\d{13}$");
        }
    }
}
