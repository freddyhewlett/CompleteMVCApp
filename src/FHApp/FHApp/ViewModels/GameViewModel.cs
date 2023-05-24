using FH.App.Extensions;
using FH.Business.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FH.App.ViewModels
{
    public class GameViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Desenvolvedor")]
        public Guid DeveloperId { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Description { get; set; }

        [DisplayName("Imagem do jogo")]
        public IFormFile ImageUpload { get; set; }

        public string Image { get; set; }

        [Currency]
        [DisplayName("Preço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Value { get; set; }

        [ScaffoldColumn(false)]
        public DateTime RegisterDate { get; set; }

        [DisplayName("Ativo?")]
        public bool Active { get; set; }

        [DisplayName("Desenvolvedor")]
        [NotMapped]
        public DeveloperViewModel Developer { get; set; }

        [NotMapped]
        public IEnumerable<DeveloperViewModel> Developers { get; set; }
    }
}
