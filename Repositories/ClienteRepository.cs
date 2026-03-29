using Microsoft.EntityFrameworkCore;
using ProjetoFinalPos.Data;
using ProjetoFinalPos.Models.Entities;

namespace ProjetoFinalPos.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ApplicationDbContext _context;

    public ClienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
        return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<IEnumerable<Cliente>> GetByNameAsync(string name)
    {
        return await _context.Clientes
            .Where(c => c.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Clientes.CountAsync();
    }

    public async Task<Cliente> CreateAsync(Cliente cliente)
    {
        cliente.CreatedAt = DateTime.UtcNow;
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> UpdateAsync(Cliente cliente)
    {
        cliente.UpdatedAt = DateTime.UtcNow;
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cliente = await GetByIdAsync(id);
        if (cliente == null)
            return false;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }
}
