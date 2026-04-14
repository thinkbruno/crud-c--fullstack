using Microsoft.AspNetCore.Mvc;
using UserApi.Domain.DTOs;
using UserApi.Domain.Entities;
using UserApi.Domain.Interfaces;

namespace UserApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UsersController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
    {
        var users = await _repository.GetAllAsync();

        // Converte a lista de Entidades para DTOs de Resposta
        var response = users.Select(user => new UserResponse(
            user.Id,
            user.Name,
            user.Email,
            user.CreatedAt
        ));

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetUser(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return NotFound();

        var response = new UserResponse(user.Id, user.Name, user.Email, user.CreatedAt);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<UserResponse>> PostUser(UserRequest request)
    {
        // Mapeia o Request (DTO) para a Entidade (Domain)
        var user = new User
        {
            Name = request.Name,
            Email = request.Email
            // O Id e CreatedAt são gerados automaticamente na classe User
        };

        await _repository.AddAsync(user);

        // Retorna o Response (DTO) para não expor a entidade pura
        var response = new UserResponse(user.Id, user.Name, user.Email, user.CreatedAt);

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(Guid id, [FromBody] UserRequest request) // Adicione [FromBody] para garantir
    {
        var userInDb = await _repository.GetByIdAsync(id);
        if (userInDb == null) return NotFound();

        userInDb.Name = request.Name;
        userInDb.Email = request.Email;

        await _repository.UpdateAsync(userInDb);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return NotFound();

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}