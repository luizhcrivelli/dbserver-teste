using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SD.Domain.Interfaces.Repository;
using SD.Domain.Interfaces.Service;
using SD.Domain.Services;
using SD.Infra;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

namespace SD.Api
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

            services.AddTransient<IOperacaoService, OperacaoService>();
            services.AddTransient<IContaService, ContaService>();
            services.AddTransient<ILancamentoRepository, LancamentoRepository>();
            services.AddTransient<ILancamentoService, LancamentoService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region swagger

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info
                {
                    Title = "Teste p/ vaga superdigital",
                    Version = "v1",
                    Description = "Api do teste para efetuar uma transação",
                    Contact = new Contact
                    {
                        Email = "luizhcrivelli@gmail.com",
                        Name = "Luiz Henrique B. Crivelli"
                    }
                });

                var caminhoAplicacao = System.AppDomain.CurrentDomain.BaseDirectory;
                var nomeAplicacao = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                var caminhoXmlDoc = System.IO.Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");
                
                if (nomeAplicacao == "testhost")
                    caminhoXmlDoc = System.IO.Path.Combine(caminhoAplicacao, "SD.Api.xml");

                x.IncludeXmlComments(caminhoXmlDoc);

            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region swager
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste p/ vaga superdigital V1");
            });
            
            #endregion
            
            app.UseMvc();

        }
    }
}
