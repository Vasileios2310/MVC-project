namespace WebAppMVCDBFirst.Repositories;
/// <summary>
/// This class has an extension method, which help to insert in IoC container Unit of Work
/// </summary>
public static class RepositoriesDIExtensions
{
    // Extension methods take as first parameter, the type we want to extend
    // this --> makes method to be extentable 
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}