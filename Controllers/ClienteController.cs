using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinalPos.Models.Dtos;
using ProjetoFinalPos.Services;

namespace ProjetoFinalPos.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]  // Requer autenticação JWT
public class ClienteController : ControllerBase
{
    private readonly ClienteService _service;

    public ClienteController(ClienteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> GetAll()
    {
        var clientes = await _service.GetAllAsync();
        return Ok(clientes);
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount()
    {
        var count = await _service.GetCountAsync();
        return Ok(count);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteResponseDto>> GetById(int id)
    {
        var cliente = await _service.GetByIdAsync(id);
        if (cliente == null)
            return NotFound(new { message = "Cliente não encontrado" });

        return Ok(cliente);
    }

    [HttpGet("search/{name}")]
    public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> GetByName(string name)
    {
        var clientes = await _service.GetByNameAsync(name);
        return Ok(clientes);
    }

    [HttpPost]
    public async Task<ActionResult<ClienteResponseDto>> Create(ClienteDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Email))
            return BadRequest(new { message = "Name e Email são obrigatórios" });

        var cliente = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ClienteResponseDto>> Update(int id, ClienteDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Email))
            return BadRequest(new { message = "Name e Email são obrigatórios" });

        var cliente = await _service.UpdateAsync(id, dto);
        if (cliente == null)
            return NotFound(new { message = "Cliente não encontrado" });

        return Ok(cliente);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success)
            return NotFound(new { message = "Cliente não encontrado" });

        return NoContent();
    }
}
