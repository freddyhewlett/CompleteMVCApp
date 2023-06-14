using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.Business.Models.Validations.Docs
{
    public class GameValidation : AbstractValidator<Game>
    {
        public GameValidation() 
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser informado")
                .Length(2, 200).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser informado")
                .Length(2, 1000).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Value)
                .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que {ComparisonValue}");
        }
    }
}
