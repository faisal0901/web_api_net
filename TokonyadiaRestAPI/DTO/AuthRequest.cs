using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TokonyadiaRestAPI.DTO;

public class AuthRequest
{


    [Column(name: "email"), Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Column(name: "password"), Required, StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    public string Password { get; set; } = null;
}