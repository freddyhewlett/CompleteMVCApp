using FH.Business.Models.Validations.Docs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.Business.Models.Validations
{
    public class DeveloperValidation : AbstractValidator<Developer>
    {
        public DeveloperValidation() 
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser informado")
                .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            When(d => d.DeveloperType == DeveloperType.GameDeveloper, () => 
            {
                RuleFor(d => d.Document.Length).Equal(GameDevValidation.GameDevDocumentLength)
                    .WithMessage("O campo Documento deve ter {ComparisonValue} caracteres, mas foi informado {PropertyValue}");

                RuleFor(d => GameDevValidation.Validate(d.Document)).Equal(true)
                    .WithMessage("Documento informado é inválido");
            });

            When(d => d.DeveloperType == DeveloperType.SoftwareDeveloper, () => 
            {
                RuleFor(d => d.Document.Length).Equal(SwDevValidation.SwDevDocLength)
                    .WithMessage("O campo Documento deve ter {ComparisonValue} caracteres, mas foi informado {PropertyValue}");

                RuleFor(d => SwDevValidation.Validate(d.Document)).Equal(true)
                    .WithMessage("Documento informado é inválido");
            });
        }
    }
}
