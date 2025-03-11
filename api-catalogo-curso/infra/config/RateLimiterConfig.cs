using System.Threading.RateLimiting;

namespace api_catalogo_curso.infra.config;

public static class RateLimiterConfig
{
    public static void AddRateLimiterGlobal(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpcontext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpcontext.User.Identity?.Name ?? httpcontext.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 2,
                        QueueLimit = 2,
                        Window = TimeSpan.FromSeconds(5),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                    }
                )
            );
        });
    }
}