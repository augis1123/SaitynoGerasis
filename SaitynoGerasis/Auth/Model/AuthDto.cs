using System.ComponentModel.DataAnnotations;

namespace SaitynoGerasis.Auth.Model
{
    public record RegisterUserDto([Required] string UserName, [EmailAddress][Required] string Email, [Required] string Password);

    public record LoginDto(string UserName, string Password);   
    public record UserDto (string id, string UserName, string Password );
    public record Succes(string token);

}
