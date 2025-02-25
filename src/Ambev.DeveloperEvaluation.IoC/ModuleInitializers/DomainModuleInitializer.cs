using Ambev.DeveloperEvaluation.Domain.Services.DiscountRules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class DomainModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IDiscountStrategyFactory, DiscountStrategyFactory>();
        builder.Services.AddTransient<IDiscountStrategy, NoDiscountStrategy>();
        builder.Services.AddTransient<IDiscountStrategy, TenPercentDiscountStrategy>();
        builder.Services.AddTransient<IDiscountStrategy, TwentyPercentDiscountStrategy>();
    }
}