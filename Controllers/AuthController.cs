using Microsoft.AspNetCore.Mvc;
using ProjetoFinalPos.Authentication;
using ProjetoFinalPos.Models.Requests;

namespace ProjetoFinalPos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IJwtService jwtService, ILogger<AuthController> logger)
    {
        _jwtService = jwtService;
        _logger = logger;
    }

    /// <summary>
    /// Faz login e retorna um JWT token
    /// </summary>
    /// <param name="request">Credenciais (username e password)</param>
    /// <returns>Token JWT válido por 1 hora</returns>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { message = "Username e Password são obrigatórios" });

        // Validação simplificada para demo
        // Em produção, validar contra um banco de usuários com senha hasheada
        if (request.Username == "admin" && request.Password == "admin123")
        {
            var token = _jwtService.GenerateToken(request.Username);
            var expiresAt = DateTime.UtcNow.AddHours(1);
            
            _logger.LogInformation($"Login bem-sucedido para usuário: {request.Username}");
            
            return Ok(new LoginResponse
            {
                Token = token,
                Username = request.Username,
                ExpiresAt = expiresAt
            });
        }

        _logger.LogWarning($"Tentativa de login falhada para usuário: {request.Username}");
        return Unauthorized(new { message = "Credenciais inválidas" });
    }
}
