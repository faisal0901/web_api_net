using System.ComponentModel.DataAnnotations;

namespace TokonyadiaRestAPI.DTO;

public class RegisterRequest
{
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required] public string PhoneNumber { get; set; } = string.Empty;

    [Required, StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    public string password { get; set; } = string.Empty;
}