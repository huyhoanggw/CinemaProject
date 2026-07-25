using System.ComponentModel.DataAnnotations;

namespace Cenima.IdentityApi.Pages.Create;

public class InputModel
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]

    public string? FirstName { get; set; }

    [Required]

    public string? LastName { get; set; }

    [Required]

    public string? Email { get; set; }

    public string? ReturnUrl { get; set; }

    public string? Button { get; set; }
}
