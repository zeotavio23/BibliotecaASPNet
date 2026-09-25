namespace BibliotecaASPNet.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        // Métodos assíncronos
        Task<IReadOnlyList<TEntity>> ListarTodosAsync();
        Task<TEntity> SelecionarPorIdAsync(int id);
        Task IncluirAsync(TEntity objeto);
        Task AlterarAsync(TEntity objeto);
        Task ExcluirAsync(TEntity objeto);

        // Métodos síncronos
        IReadOnlyList<TEntity> ListarTodos();
        TEntity SelecionarPorId(int id);
        void Incluir(TEntity objeto);
        void Alterar(TEntity objeto);
        void Excluir(TEntity objeto);
    }
}
