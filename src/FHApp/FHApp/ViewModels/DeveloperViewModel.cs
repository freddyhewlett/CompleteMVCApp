using FH.Business.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FH.App.ViewModels
{
    public class DeveloperViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [DisplayName("Documento")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Document { get; set; }

        [DisplayName("Tipo")]
        public int DeveloperType { get; set; }

        public AddressViewModel Address { get; set; }

        [DisplayName("Ativo?")]
        public bool Active { get; set; }


        public IEnumerable<GameViewModel> Games { get; set; }
    }
}
