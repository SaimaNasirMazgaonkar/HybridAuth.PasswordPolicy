# HybridAuth.PasswordPolicy

A configurable password policy validator for .NET 8. Returns all validation errors at once — not one at a time.

## Install

```bash
dotnet add package HybridAuth.PasswordPolicy
```

## Quick Start

### With appsettings.json (recommended)

```json
{
  "PasswordPolicy": {
    "MinLength": 8,
    "MaxLength": 100,
    "RequireUppercase": true,
    "RequireLowercase": true,
    "RequireDigit": true,
    "RequireSpecialChar": true,
    "AllowedSpecialChars": "@#$%^&*!_-"
  }
}
```

```csharp
// Program.cs
builder.Services.AddHybridPasswordPolicy(builder.Configuration);
```

### With inline config

```csharp
builder.Services.AddHybridPasswordPolicy(options =>
{
    options.MinLength = 10;
    options.RequireSpecialChar = true;
    options.AllowedSpecialChars = "@#$!";
});
```

### Usage

```csharp
public class AuthService
{
    private readonly IPasswordPolicyValidator _validator;

    public AuthService(IPasswordPolicyValidator validator)
    {
        _validator = validator;
    }

    public void Register(string password)
    {
        var result = _validator.Validate(password);

        if (!result.IsValid)
        {
            // result.Errors contains ALL failures:
            // ["Password must contain at least one uppercase letter",
            //  "Password must contain at least one digit"]
        }
    }
}
```

### Without DI

```csharp
var validator = new PasswordPolicyValidator(new PasswordPolicyOptions
{
    MinLength = 8,
    RequireUppercase = true,
    AllowedSpecialChars = "@#$%^&*!_-"
});

var result = validator.Validate("weak");
// result.IsValid = false
// result.Errors = ["Password must be at least 8 characters", ...]
```

## Validation Rules

| Rule | Default | Description |
|------|---------|-------------|
| MinLength | 8 | Minimum password length |
| MaxLength | 100 | Maximum password length |
| RequireUppercase | true | At least one A-Z |
| RequireLowercase | true | At least one a-z |
| RequireDigit | true | At least one 0-9 |
| RequireSpecialChar | true | At least one from AllowedSpecialChars |
| AllowedSpecialChars | @#$%^&*!_- | Only these special chars are allowed |

Characters not in `AllowedSpecialChars` and not alphanumeric are rejected.

## License

MIT
