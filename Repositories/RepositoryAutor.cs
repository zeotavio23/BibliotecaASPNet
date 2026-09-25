using BibliotecaASPNet.Data;
using BibliotecaASPNet.Models;

namespace BibliotecaASPNet.Repositories
{
    public class RepositoryAutor : RepositoryBase<Autor>
    {
        public RepositoryAutor(BibliotecaDbContext context) : base(context)
        {
        }

        public bool EmailJaCadastrado(string email, int? ignorarId = null)
        {
            IEnumerable<Autor> autores = this.ListarTodos();
            return autores.Any(a => a.Email == email && a.Id != ignorarId);
        }

        public IEnumerable<Autor> Listar(string? nome = null)
        {
            IEnumerable<Autor> consulta = this.ListarTodos();
            if (!string.IsNullOrWhiteSpace(nome))
            {
                consulta = consulta.Where(a => a.Nome.Contains(nome,
                    StringComparison.OrdinalIgnoreCase));
            }
            return consulta.OrderBy(a => a.Nome);
        }
    }
}
