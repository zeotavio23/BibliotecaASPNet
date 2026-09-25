using System.ComponentModel.DataAnnotations;

namespace BibliotecaASPNet.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }

        // Chave estrangeira para Livro
        [Required(ErrorMessage = "Selecione um livro")]
        [Display(Name = "Livro")]
        public int LivroId { get; set; }
        public Livro? Livro { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "O Nome do leitor deve possuir entre 3 e 100 caracteres")]
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
        public bool Devolvido { get; set; } = false;
    }
}
