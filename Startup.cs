using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Data;
using ProductCatalog.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace ProductCatalog
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddResponseCompression();
            services.AddScoped<StoreDataContext, StoreDataContext>();
            services.AddTransient<ProductRepository, ProductRepository>();

            services.AddSwaggerGen(x => 
            {
                x.SwaggerDoc("v1", new Info { Title = "Catálogo de Produtos", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
            app.UseResponseCompression();
            app.UseSwagger();
            app.UseSwaggerUI(x => 
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Catálogo de Produtos");
            });
        }
    }
}
