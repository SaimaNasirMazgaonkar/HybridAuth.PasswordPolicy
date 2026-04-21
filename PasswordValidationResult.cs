namespace HybridAuth.PasswordPolicy;

public class PasswordValidationResult
{
  public bool IsValid { get; set; }
  public List<string> Errors { get; set; } = new();

  public static PasswordValidationResult Success() => new() { IsValid = true };

  public static PasswordValidationResult Failure(List<string> errors) => new() { IsValid = false, Errors = errors };
}
