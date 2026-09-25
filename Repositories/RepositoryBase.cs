using BibliotecaASPNet.Data;
using BibliotecaASPNet.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaASPNet.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly BibliotecaDbContext _context;
        private readonly DbSet<TEntity> DbSet;

        public RepositoryBase(BibliotecaDbContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        public void Alterar(TEntity objeto)
        {
            DbSet.Update(objeto);
            _context.SaveChanges();
        }

        public async Task AlterarAsync(TEntity objeto)
        {
            _context.Entry(objeto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void Excluir(TEntity objeto)
        {
            DbSet.Remove(objeto);
            _context.SaveChanges();
        }

        public async Task ExcluirAsync(TEntity objeto)
        {
            DbSet.Remove(objeto);
            await _context.SaveChangesAsync();
        }

        public void Incluir(TEntity objeto)
        {
            DbSet.Add(objeto);
            _context.SaveChanges();
        }

        public async Task IncluirAsync(TEntity objeto)
        {
            await DbSet.AddAsync(objeto);
            await _context.SaveChangesAsync();
        }

        public IReadOnlyList<TEntity> ListarTodos()
        {
            return DbSet.ToList();
        }

        public async Task<IReadOnlyList<TEntity>> ListarTodosAsync()
        {
            return await DbSet.ToListAsync();
        }

        public TEntity SelecionarPorId(int id)
        {
            return DbSet.Find([id])!;
        }

        public async Task<TEntity> SelecionarPorIdAsync(int id)
        {
            return (await DbSet.FindAsync([id]))!;
        }
    }
}
