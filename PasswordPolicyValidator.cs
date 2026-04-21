using Microsoft.Extensions.Options;

namespace HybridAuth.PasswordPolicy;

public class PasswordPolicyValidator : IPasswordPolicyValidator
{
  private readonly PasswordPolicyOptions _options;

  public PasswordPolicyValidator(IOptions<PasswordPolicyOptions> options)
  {
    _options = options.Value;
  }

  public PasswordPolicyValidator(PasswordPolicyOptions options)
  {
    _options = options;
  }

  public PasswordValidationResult Validate(string password)
  {
    var errors = new List<string>();

    if (string.IsNullOrEmpty(password))
    {
      errors.Add("Password is required");
      return PasswordValidationResult.Failure(errors);
    }

    if (password.Length < _options.MinLength)
      errors.Add($"Password must be at least {_options.MinLength} characters");

    if (password.Length > _options.MaxLength)
      errors.Add($"Password must not exceed {_options.MaxLength} characters");

    if (_options.RequireUppercase && !password.Any(char.IsUpper))
      errors.Add("Password must contain at least one uppercase letter");

    if (_options.RequireLowercase && !password.Any(char.IsLower))
      errors.Add("Password must contain at least one lowercase letter");

    if (_options.RequireDigit && !password.Any(char.IsDigit))
      errors.Add("Password must contain at least one digit");

    if (_options.RequireSpecialChar && !password.Any(c => _options.AllowedSpecialChars.Contains(c)))
      errors.Add($"Password must contain at least one special character ({_options.AllowedSpecialChars})");

    foreach (var c in password)
    {
      if (!char.IsLetterOrDigit(c) && !_options.AllowedSpecialChars.Contains(c))
      {
        errors.Add($"Character '{c}' is not allowed. Allowed special characters: {_options.AllowedSpecialChars}");
        break;
      }
    }

    return errors.Count == 0
        ? PasswordValidationResult.Success()
        : PasswordValidationResult.Failure(errors);
  }
}
