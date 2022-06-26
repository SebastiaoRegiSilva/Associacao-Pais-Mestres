using Disparo.Plataforma.Domain.Alunos;
using Disparo.Plataforma.Domain.Armarios;
using Disparo.Plataforma.Domain.Classes;
using Disparo.Plataforma.Domain.Emails;
using Disparo.Plataforma.Domain.Responsaveis;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Alunos;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Armarios;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Classes;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Emails;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Predios;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Responsaveis;
using Disparo.Plataforma.Domain.Predios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using System;
using Disparo.Plataforma.Domain.Pagamentos;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Pagamentos;

namespace Disparo.Plataforma.Api
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
            string mongoConString = Configuration.GetValue<string>("DB:Mongo:ConString");
            string mongoDatabase = Configuration.GetValue<string>("DB:Mongo:Database");

            var alunoRep = new AlunoRepository(mongoConString, mongoDatabase);
            var alunoService = new AlunoService(alunoRep);
            services.AddSingleton<AlunoService>(alunoService);

            var classeRep = new ClasseRepository(mongoConString, mongoDatabase);
            var classeService = new ClasseService(classeRep);
            services.AddSingleton<ClasseService>(classeService);

            var emailRep = new EmailRepository(mongoConString, mongoDatabase);
            var emailService = new EmailService(emailRep, alunoService);
            services.AddSingleton<EmailService>(emailService);

            var predioRep = new PredioRepository(mongoConString, mongoDatabase);
            var predioService = new PredioService(predioRep);
            services.AddSingleton<PredioService>(predioService);

            var armarioRep = new ArmarioRepository(mongoConString, mongoDatabase);
            var armarioService = new ArmarioService(armarioRep, alunoService, predioService);
            services.AddSingleton<ArmarioService>(armarioService);

            var responsavelRep = new ResponsavelRepository(mongoConString, mongoDatabase);
            var responsavelService = new ResponsavelService(responsavelRep);
            services.AddSingleton<ResponsavelService>(responsavelService);

            var pagamentoRep = new PagamentoRepository(mongoConString, mongoDatabase);
            var pagamentoService = new PagamentoService(pagamentoRep, alunoService);
            services.AddSingleton<PagamentoService>(pagamentoService);


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "associacao_pais_mestres", Version = "v1" });
                
                // Permitir comentários no swagger. 
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Corrigir, pois está rodando como se fosse em produção.
            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "associacao_pais_mestres v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
