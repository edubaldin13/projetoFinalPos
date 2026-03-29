namespace ProjetoFinalPos.Repositories;

public interface IClienteRepository
{
    Task<Cliente?> GetByIdAsync(int id);
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<IEnumerable<Cliente>> GetByNameAsync(string name);
    Task<int> GetCountAsync();
    Task<Cliente> CreateAsync(Cliente cliente);
    Task<Cliente> UpdateAsync(Cliente cliente);
    Task<bool> DeleteAsync(int id);
}
