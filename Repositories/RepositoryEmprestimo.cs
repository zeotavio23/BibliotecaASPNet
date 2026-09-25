using BibliotecaASPNet.Data;
using BibliotecaASPNet.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaASPNet.Repositories
{
    public class RepositoryEmprestimo : RepositoryBase<Emprestimo>
    {
        private readonly BibliotecaDbContext _context;

        public RepositoryEmprestimo(BibliotecaDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Emprestimo> Listar()
        {
            return _context.Emprestimos
                .Include(e => e.Livro)
                .OrderByDescending(e => e.DataEmprestimo)
                .ToList();
        }

        public Emprestimo? SelecionarComLivro(int id)
        {
            return _context.Emprestimos.Include(e => e.Livro)
                .FirstOrDefault(e => e.Id == id);
        }
    }
}
