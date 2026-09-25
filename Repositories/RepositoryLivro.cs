using BibliotecaASPNet.Data;
using BibliotecaASPNet.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaASPNet.Repositories
{
    public class RepositoryLivro : RepositoryBase<Livro>
    {
        private readonly BibliotecaDbContext _context;

        public RepositoryLivro(BibliotecaDbContext context) : base(context)
        {
            _context = context;
        }

        public bool IsbnJaCadastrado(string isbn, int? ignorarId = null)
        {
            IEnumerable<Livro> livros = this.ListarTodos();
            return livros.Any(l => l.Isbn == isbn && l.Id != ignorarId);
        }

        public IEnumerable<Livro> Listar(string? titulo = null)
        {
            IQueryable<Livro> consulta = _context.Livros.Include(l => l.Autor);
            if (!string.IsNullOrWhiteSpace(titulo))
            {
                consulta = consulta.Where(l => l.Titulo.Contains(titulo));
            }
            return consulta.OrderBy(l => l.Titulo).ToList();
        }

        public Livro? SelecionarComAutor(int id)
        {
            return _context.Livros.Include(l => l.Autor)
                .FirstOrDefault(l => l.Id == id);
        }
    }
}
