using System.ComponentModel.DataAnnotations;

namespace BibliotecaASPNet.Models
{
    public class Livro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(150, MinimumLength = 1,
            ErrorMessage = "O Título deve possuir entre 1 e 150 caracteres")]
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
        [Range(1, 10000, ErrorMessage = "Informe um número de páginas válido")]
        [Display(Name = "Número de Páginas")]
        public int NumeroPaginas { get; set; }

        // Chave estrangeira para Autor
        [Required(ErrorMessage = "Selecione um autor")]
        [Display(Name = "Autor")]
        public int AutorId { get; set; }
        public Autor? Autor { get; set; }

        // Um livro pode ter vários empréstimos ao longo do tempo
        public ICollection<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();
    }
}
