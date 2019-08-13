using Api.Model;
using Api.Security;
using Api.Util;
using DinkToPdf;
using DinkToPdf.Contracts;
using Domain.AlunoConselhoContent.Handlers;
using Domain.AlunoConselhoContent.Repositories;
using Domain.AlunoContent.Handlers;
using Domain.AlunoContent.Repositories;
using Domain.AlunoTurmaContent.Repositories;
using Domain.AnoContent.Handlers;
using Domain.AnoContent.Repositories;
using Domain.BimestreContent.Handlers;
using Domain.BimestreContent.Repositories;
using Domain.ConselhoContent.Handlers;
using Domain.ConselhoContent.Repositories;
using Domain.DadoPessoalContent.Handlers;
using Domain.DadoPessoalContent.Repositories;
using Domain.DepartamentoContent.Handlers;
using Domain.DepartamentoContent.Repositories;
using Domain.DiaSemanaContent.Handlers;
using Domain.DiaSemanaContent.Repositories;
using Domain.DisciplinaContent.Handlers;
using Domain.DisciplinaTurmaContent.Repositories;
using Domain.EscolaContent.Handlers;
using Domain.EscolaContent.Repositories;
using Domain.FuncaoContent.Handlers;
using Domain.FuncaoContent.Repositories;
using Domain.FuncionarioContent.Handlers;
using Domain.FuncionarioContent.Repositories;
using Domain.HorarioRotinaContent.Handlers;
using Domain.HorarioRotinaContent.Repositories;
using Domain.ItemAlunoTurmaContent.Handlers;
using Domain.ItemDepartamentoEscolaContent.Handlers;
using Domain.ItemDepartamentoEscolaContent.Repositories;
using Domain.ItemDisciplinaTurmaContent.Handlers;
using Domain.OcorrenciaContent.Handlers;
using Domain.OcorrenciaContent.Repositories;
using Domain.ProvidenciaContent.Handlers;
using Domain.ProvidenciaContent.Repositories;
using Domain.RotinaContent.Handlers;
using Domain.RotinaContent.Repositories;
using Domain.SerieContent.Handlers;
using Domain.SerieContent.Repositories;
using Domain.Services;
using Domain.TipoOcorrenciaContent.Handlers;
using Domain.TipoOcorrenciaContent.Repositories;
using Domain.TurmaContent.Handlers;
using Domain.TurmaContent.Repositories;
using Domain.UserContent.Handlers;
using Domain.UserContent.Repositories;
using Infra.DataContexts;
using Infra.PopularBD;
using Infra.Repositories;
using Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Interfaces;
using System;
using System.IO;

namespace Api
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(),"libwkhtmltox.dll"));

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());

                auth.AddPolicy("Administrador", policy => policy.RequireClaim("Administrador", "Administrador"));
            });


            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();


            


            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();

            services.AddResponseCompression();

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<DataContext, DataContext>();
            services.AddTransient<IUnitOfWorkRepositorio, UnitOfWorkRepositorio>();

            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<IDadoPessoalRepositorio, DadoPessoalRepositorio>();
            services.AddTransient<ISerieRepositorio, SerieRepositorio>();
            services.AddTransient<IDepartamentoRepositorio, DepartamentoRepositorio>();
            services.AddTransient<IDisciplinaRepositorio, DisciplinaRepositorio>();
            services.AddTransient<IEscolaRepositorio, EscolaRepositorio>();
            services.AddTransient<IFuncaoRepositorio, FuncaoRepositorio>();
            services.AddTransient<IAlunoRepositorio, AlunoRepositorio>();
            services.AddTransient<IFuncionarioRepositorio, FuncionarioRepositorio>();
            services.AddTransient<IOcorrenciaRepositorio, OcorrenciaRepositorio>();
            services.AddTransient<ITipoOcorrenciaRepositorio, TipoOcorrenciaRepositorio>();
            services.AddTransient<IProvidenciaRepositorio, ProvidenciaRepositorio>();
            services.AddTransient<IProvidenciaRepositorio, ProvidenciaRepositorio>();
            services.AddTransient<IRotinaRepositorio, RotinaRepositorio>();
            services.AddTransient<IDiaSemanaRepositorio, DiaSemanaRepositorio>();
            services.AddTransient<IHorarioRotinaRepositorio, HorarioRotinaRepositorio>();
            services.AddTransient<IBimestreRepositorio, BimestreRepositorio>();
            services.AddTransient<IConselhoRepositorio, ConselhoRepositorio>();
            services.AddTransient<IAlunoConselhoRepositorio, AlunoConselhoRepositorio>();
            services.AddTransient<IAnoRepositorio, AnoRepositorio>();
            services.AddTransient<ITurmaRepositorio, TurmaRepositorio>();
            services.AddTransient<IItemAlunoTurmaRepositorio, ItemAlunoTurmaRepositorio>();
            services.AddTransient<IItemDepartamentoEscolaRepositorio, ItemDepartamentoEscolaRepositorio>();
            services.AddTransient<IItemDisciplinaTurmaRepositorio, ItemDisciplinaTurmaRepositorio>();

            services.AddTransient<IEnviarEmailServices, EnviarEmailServices>();
            services.AddTransient<UsuarioHandler, UsuarioHandler>();
            services.AddTransient<DadoPessoalHandler, DadoPessoalHandler>();
            services.AddTransient<SerieHandler, SerieHandler>();
            services.AddTransient<DepartamentoHandler, DepartamentoHandler>();
            services.AddTransient<DisciplinaHandler, DisciplinaHandler>();
            services.AddTransient<EscolaHandler, EscolaHandler>();
            services.AddTransient<AlunoHandler, AlunoHandler>();
            services.AddTransient<FuncaoHandler, FuncaoHandler>();
            services.AddTransient<FuncionarioHandler, FuncionarioHandler>();
            services.AddTransient<OcorrenciaHandler, OcorrenciaHandler>();
            services.AddTransient<TipoOcorrenciaHandler, TipoOcorrenciaHandler>();
            services.AddTransient<ProvidenciaHandler, ProvidenciaHandler>();
            services.AddTransient<ItemDepartamentoEscolaHandler, ItemDepartamentoEscolaHandler>();
            services.AddTransient<RotinaHandler, RotinaHandler>();
            services.AddTransient<DiaSemanaHandler, DiaSemanaHandler>();
            services.AddTransient<HorarioRotinaHandler, HorarioRotinaHandler>();
            services.AddTransient<ConselhoHandler, ConselhoHandler>();
            services.AddTransient<AlunoConselhoHandler, AlunoConselhoHandler>();
            services.AddTransient<BimestreHandler, BimestreHandler>();
            services.AddTransient<AnoHandler, AnoHandler>();
            services.AddTransient<TurmaHandler, TurmaHandler>();
            services.AddTransient<ItemAlunoTurmaHandler, ItemAlunoTurmaHandler>();
            services.AddTransient<ItemDisciplinaTurmaHandler, ItemDisciplinaTurmaHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataContext context)
        {


            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            new PopularBanco(context)
                .Initialize();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


        }
    }
}
