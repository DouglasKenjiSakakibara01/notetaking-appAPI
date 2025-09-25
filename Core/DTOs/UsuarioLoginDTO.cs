namespace NoteTakingAPI.Core.DTOs
{
    public record UsuarioLoginDTO
    {
        public string Email { get; init; } = string.Empty;
        public string Senha { get; init; } = string.Empty;
    }
}
