using System.ComponentModel.DataAnnotations;

namespace BibliotecaASPNet.ViewModels
{
    public class EmprestimoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecione um livro")]
        [Display(Name = "Livro")]
        public int LivroId { get; set; }

        // Apenas para exibição
        public string? LivroTitulo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Nome do Leitor")]
        public string NomeLeitor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date)]
        [Display(Name = "Data do Empréstimo")]
        public DateTime DataEmprestimo { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Devolução Prevista")]
        public DateTime DataDevolucaoPrevista { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Devolução Real")]
        public DateTime? DataDevolucaoReal { get; set; }

        [Display(Name = "Devolvido")]
        public bool Devolvido { get; set; }
    }
}
