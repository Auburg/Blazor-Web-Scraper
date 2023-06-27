using BlazorApp1.Services;

namespace BlazorApp1.DI;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorQuotesRetrieverService, AuthorQuotesRetrieverService>();
        services.AddSingleton<IFurnitureRetrieverService, FurnitureRetrieverService>();

        return services;
    }
}