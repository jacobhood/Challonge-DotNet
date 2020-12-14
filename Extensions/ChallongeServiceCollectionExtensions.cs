using Challonge.Api;
using Microsoft.Extensions.DependencyInjection;

namespace Challonge.Extensions.DependencyInjection
{
    public static class ChallongeServiceCollectionExtensions
    {
        public static IServiceCollection AddChallonge(this IServiceCollection services, string username, string apiKey)
        {
            services.AddSingleton<IChallongeCredentials>(new ChallongeCredentials(username, apiKey));
            services.AddHttpClient<IChallongeClient, ChallongeClient>();
            return services;
        }
    }
}
