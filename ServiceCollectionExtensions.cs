using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HybridAuth.PasswordPolicy;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddHybridPasswordPolicy(this IServiceCollection services, IConfiguration configuration, string sectionName = "PasswordPolicy")
  {
    services.Configure<PasswordPolicyOptions>(configuration.GetSection(sectionName));
    services.AddSingleton<IPasswordPolicyValidator, PasswordPolicyValidator>();
    return services;
  }

  public static IServiceCollection AddHybridPasswordPolicy(this IServiceCollection services, Action<PasswordPolicyOptions> configure)
  {
    services.Configure(configure);
    services.AddSingleton<IPasswordPolicyValidator, PasswordPolicyValidator>();
    return services;
  }
}
