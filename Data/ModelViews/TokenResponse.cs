namespace Tawtheiq.Application.Cores.Identity.Dtos.Respones;
public record TokenResponse(string Token, DateTime? TokenExpiryTime, string UserId = "");
