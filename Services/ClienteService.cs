using ProjetoFinalPos.Models.Dtos;
using ProjetoFinalPos.Models.Entities;
using ProjetoFinalPos.Repositories;

namespace ProjetoFinalPos.Services;

public class ClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClienteResponseDto?> GetByIdAsync(int id)
    {
        var cliente = await _repository.GetByIdAsync(id);
        if (cliente == null)
            return null;

        return MapToResponseDto(cliente);
    }

    public async Task<IEnumerable<ClienteResponseDto>> GetAllAsync()
    {
        var clientes = await _repository.GetAllAsync();
        return clientes.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<ClienteResponseDto>> GetByNameAsync(string name)
    {
        var clientes = await _repository.GetByNameAsync(name);
        return clientes.Select(MapToResponseDto);
    }

    public async Task<int> GetCountAsync()
    {
        return await _repository.GetCountAsync();
    }

    public async Task<ClienteResponseDto> CreateAsync(ClienteDto dto)
    {
        var cliente = new Cliente
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone
        };

        var createdCliente = await _repository.CreateAsync(cliente);
        return MapToResponseDto(createdCliente);
    }

    public async Task<ClienteResponseDto?> UpdateAsync(int id, ClienteDto dto)
    {
        var cliente = await _repository.GetByIdAsync(id);
        if (cliente == null)
            return null;

        cliente.Name = dto.Name;
        cliente.Email = dto.Email;
        cliente.Phone = dto.Phone;

        var updatedCliente = await _repository.UpdateAsync(cliente);
        return MapToResponseDto(updatedCliente);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static ClienteResponseDto MapToResponseDto(Cliente cliente)
    {
        return new ClienteResponseDto
        {
            Id = cliente.Id,
            Name = cliente.Name,
            Email = cliente.Email,
            Phone = cliente.Phone,
            CreatedAt = cliente.CreatedAt,
            UpdatedAt = cliente.UpdatedAt
        };
    }
}
