using System.ComponentModel.DataAnnotations;

namespace BibliotecaASPNet.ViewModels
{
    public class LivroViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(150, MinimumLength = 1)]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^[0-9\-]{10,17}$", ErrorMessage = "Informe um ISBN válido")]
        public string Isbn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1400, 2100, ErrorMessage = "Informe um ano de publicação válido")]
        [Display(Name = "Ano de Publicação")]
        public int AnoPublicacao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50)]
        public string Genero { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, 10000)]
        [Display(Name = "Número de Páginas")]
        public int NumeroPaginas { get; set; }

        [Required(ErrorMessage = "Selecione um autor")]
        [Display(Name = "Autor")]
        public int AutorId { get; set; }

        // Apenas para exibição (não é preenchido pelo formulário)
        public string? AutorNome { get; set; }
    }
}
