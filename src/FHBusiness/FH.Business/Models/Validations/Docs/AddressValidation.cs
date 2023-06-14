using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.Business.Models.Validations.Docs
{
    public class AddressValidation: AbstractValidator<Address>
    {
        public AddressValidation() 
        {
            RuleFor(c => c.Street)
               .NotEmpty().WithMessage("O campo {PropertyName} deve ser informado")
               .Length(2, 200).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Neighborhood)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            //Validação de CEP por motivos didaticos
            RuleFor(c => c.ZipCode)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser informado")
                .Length(8).WithMessage("O campo {PropertyName} deve ter {MaxLength} caracteres");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser informado")
                .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.State)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser informado")
                .Length(2, 50).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Number)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser informado")
                .Length(1, 50).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Country)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser informado")
                .Length(1, 50).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
