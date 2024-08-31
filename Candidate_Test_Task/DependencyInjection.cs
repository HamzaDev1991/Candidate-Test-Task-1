using Candidate_Test_Task.Services;
using Microsoft.Extensions.Options;

namespace Candidate_Test_Task
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(Options => Options.AddPolicy("AllowAll", builder=>builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()) 
            );

            services.AddEndpointsApiExplorer()
                    .AddDistributedMemoryCache()
                    .AddSwaggerGen()
                    .AddEndpointsApiExplorer();
                   
           
       
            services.AddScoped<ICandidateServices, CandidateServicesForCSV>();
            services.AddScoped<ICashService, CashService>();

            return services;
        }

     
    }
}
