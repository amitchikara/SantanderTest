using BestStories.Core.HttpServices;
using BestStories.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hellang.Middleware.ProblemDetails.Mvc;
using Hellang.Middleware.ProblemDetails;
using System.Security.Authentication;
using System;
using BestStories.Exceptions;

namespace BestStories
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            services.AddSingleton<ITopStoriesService, TopStoriesService>();
            services.AddHttpClient<IHackerNewsHttpService, HackerNewsHttpService>();
            services.AddSingleton<ICacheProvider, CacheProvider>();
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Best Story Service API",
                    Version = "v1",
                    Description = "API will return the best stories.",
                });
            });

            services.AddProblemDetailsConventions();
            services.AddProblemDetails(options => {
                options.MapToStatusCode<AuthenticationException>(401);
                options.MapToStatusCode<UnauthorizedAccessException>(403);
                options.MapToStatusCode<BadRequestException>(400);
                options.MapToStatusCode<ConfigurationException>(500);
                options.MapToStatusCode<ConflictException>(409);
                options.MapToStatusCode<FeatureNotSupportedException>(501);
                options.MapToStatusCode<NotFoundException>(404);
                options.MapToStatusCode<ProcessingException>(500);
                options.MapToStatusCode<RepositoryException>(500);
                options.MapToStatusCode<ServiceUnavailableException>(503);
                options.IncludeExceptionDetails = (ctx, ex) => true;
                options.OnBeforeWriteDetails = (ctx, pr) =>
                {
                    throw new Exception($"Exception/Problem occurred {pr.Title} {pr.Type} {pr.Status} {pr.Detail} {pr.Instance}");
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "BestStory Service"));
        }
    }
}
