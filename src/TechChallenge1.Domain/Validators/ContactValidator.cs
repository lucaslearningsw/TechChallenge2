using FluentValidation;
using TechChallenge1.Domain.Models;

namespace TechChallenge1.Domain.Validators
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator() 
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage("Campo de preenchimento obrigatorio.")
                .EmailAddress().WithMessage("Informe um endereco de e-mail valido. Ex.: nome@dominio.com.br");

            RuleFor(c => c.Phone).NotEmpty().WithMessage("Campo de preenchimento obrigatorio.")
                .Must(IsOnlyNumberString).WithMessage("O telefone deve conter apenas numeros.")
                .Length(10, 11).WithMessage("O telefone deve conter entre 10 e 11 digitos.");

            RuleFor(c => c.Name).NotEmpty().WithMessage("Campo de preenchimento obrigatorio.")
                .Length(4, 64).WithMessage("Campo com limite entre 4 e 64 caracteres.");

            RuleFor(c => c.StateId).Must(BeAValidGuid).WithMessage("Campo de preenchimento obrigatorio.")
                .NotEmpty().WithMessage("Campo de preenchimento obrigatorio.");
        }

        // Check if the string passed contains only numeric characters
        private bool IsOnlyNumberString(string numericString)
        {
            return long.TryParse(numericString, out _);
        }

        private bool BeAValidGuid(Guid id)
        {
            return id != Guid.Empty;
        }
    }
}
