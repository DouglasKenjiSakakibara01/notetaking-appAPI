namespace NoteTakingAPI.Core.DTOs;
public record UsuarioLoginRetornoDTO
{
    public string Email { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;

    public UsuarioLoginRetornoDTO(string email, string token)
    {
        this.Email = email;
        this.Token = token;
    }
}