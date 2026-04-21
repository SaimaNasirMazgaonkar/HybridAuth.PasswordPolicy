namespace HybridAuth.PasswordPolicy;

public class PasswordPolicyOptions
{
  public int MinLength { get; set; } = 8;
  public int MaxLength { get; set; } = 100;
  public bool RequireUppercase { get; set; } = true;
  public bool RequireLowercase { get; set; } = true;
  public bool RequireDigit { get; set; } = true;
  public bool RequireSpecialChar { get; set; } = true;
  public string AllowedSpecialChars { get; set; } = "@#$%^&*!_-";
}
