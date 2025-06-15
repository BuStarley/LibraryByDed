using Application.Users.Command.Register;
using Application.Users.Query.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Services.Users;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers.api;

[ApiController]
[Route("api/auth")]
[Produces("application/json")]
public class AuthController(IMediator mediator, ILogger logger) : Controller
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request)
    {
        try
        {
            var command = new RegisterUserCommand(
                request.UserName,
                request.PhoneNumber,
                request.Password);

            var result = await mediator.Send(command);

            return Ok(new AuthResponse
            {
                Success = true,
                UserId = result.UserId,
                Message = "Регистрация успешна. Подтвердите номер телефона."
            });
        }
        catch (ValidationException ex)
        {
            logger.LogWarning(ex, "Ошибка валидации при регистрации");
            return BadRequest(new ErrorResponse(ex.Message));
        }
        catch (UserAlreadyExistsException ex)
        {
            logger.LogWarning(ex, "Попытка повторной регистрации");
            return Conflict(new ErrorResponse("Пользователь с этим номером уже зарегистрирован"));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при регистрации");
            return StatusCode(500, new ErrorResponse("Внутренняя ошибка сервера"));
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        var command = new LoginUserQuery(request.PhoneNumber, request.Password);
        var result = await mediator.Send(command);

        return Ok(result);
    }
    public record RegisterRequest(
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя должно быть от 3 до 50 символов")]
        string UserName,

        [Required(ErrorMessage = "Номер телефона обязателен")]
        [Phone(ErrorMessage = "Неверный формат номера телефона")]
        string PhoneNumber,

        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(8, ErrorMessage = "Пароль должен содержать минимум 8 символов")]
        [DataType(DataType.Password)]
        string Password);

    public record LoginRequest(
        [Required(ErrorMessage = "Номер телефона обязателен")]
        [Phone(ErrorMessage = "Неверный формат номера телефона")]
        string PhoneNumber,

        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        string Password);

    public record AuthResponse
    {
        public bool Success { get; init; }
        public string? Message { get; init; }
        public Guid? UserId { get; init; }
        public string? Token { get; init; }
        public string? RefreshToken { get; init; }
    }

    public record ErrorResponse(string Error);
}
