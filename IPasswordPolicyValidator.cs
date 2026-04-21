namespace HybridAuth.PasswordPolicy;

public interface IPasswordPolicyValidator
{
  PasswordValidationResult Validate(string password);
}
