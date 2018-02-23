using MAXIMAGO.KickIt.Games;
using MAXIMAGO.KickIt.InMemoryStorage.Games;
using MAXIMAGO.KickIt.InMemoryStorage.Players;
using MAXIMAGO.KickIt.Players;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace MAXIMAGO.KickIT
{
    public class Startup
    {
        public const string ALLOW_ALL_POLICY_NAME = "AllowAll";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // add CORS configuration
            services.AddCors(builder =>
                builder.AddPolicy(ALLOW_ALL_POLICY_NAME,
                policyBuilder => policyBuilder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()));

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MAXIMAGO KickIT API", Version = "v1" });
            });

            services.AddTransient<PlayerRepository, InMemoryPlayerRepository>();
            services.AddTransient<GamesRepository, InMemoryGamesRepository>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(ALLOW_ALL_POLICY_NAME);
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MAXIMAGO KickIT API V1");
            });
        }
    }
}
