
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using OSPeConTI.Tareas.Application.Helper;
using OSPeConTI.Tareas.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using OSPeConTI.Tareas.Domain.Entities;
using OSPeConTI.Tareas.Infrastructure.Repositories;
using OSPeConTI.Tareas.Application.Queries;
using System;
using Microsoft.OpenApi.Models;
using OData.Swagger.Services;
using System.Collections.Generic;
using OSPeConTI.Tareas.Application.Middlewares;
using OSPeConTI.Tareas.Application.Exceptions;
using OSPeConTI.Tareas.Domain.Exceptions;
using OSPeConTI.Tareas.BuildingBlocks.EventBus.Abstractions;
using OSPeConTI.Tareas.BuildingBlocks.EventBusRabbitMQ;
using Autofac;
using Microsoft.Extensions.Logging;
using OSPeConTI.Tareas.BuildingBlocks.EventBus;
using RabbitMQ.Client;
using Autofac.Extensions.DependencyInjection;
using OSPeConTI.Tareas.Application.IntegrationEvents;
using OSPeConTI.Tareas.BuildingBlocks.IntegrationEventLogEF;
using OSPeConTI.Tareas.BuildingBlocks.IntegrationEventLogEF.Services;
using System.Data.Common;
using OSPeConTI.Tareas.Domain.Repositories;

namespace OSPeConTI.Tareas.Application
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;

            _env = env;

            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddEnvironmentVariables(); ;
            if (_env.IsProduction())
            {
                Console.WriteLine("--> Corriendo en Produccion");
                builder.AddJsonFile("appSettings.production.json", optional: false, reloadOnChange: true);
            }
            else
            {
                Console.WriteLine("--> Corriendo en Desarrollo");
                builder.AddJsonFile("appSettings.development.json", optional: false, reloadOnChange: true);
            }
            this.Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddAuthorization();
            services.AddControllers();
            services.AddSwaggerGen(c =>
               {
                   c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tareas", Version = "v1" });
               });
            //services.AddOData();



            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(INotificationHandler<AfiliadosAgregadoRequested>), typeof(AfiliadosAgregadoHandler));


            services.AddScoped(typeof(SectorRepository), typeof(SectorRepository));
            services.AddScoped(typeof(TareaRepository), typeof(TareaRepository));
            services.AddScoped<ISectorQueries>(conns => new SectorQueries(Configuration.GetConnectionString("DefaultConnection")));

            /*      services.AddScoped<INacionalidadQueries>(conns => new NacionalidadQueries(Configuration.GetConnectionString("DefaultConnection")));
                 services.AddScoped<IEstadoCivilQueries>(conns => new EstadoCivilQueries(Configuration.GetConnectionString("DefaultConnection")));
                 services.AddScoped<ITipoDocumentoQueries>(conns => new TipoDocumentoQueries(Configuration.GetConnectionString("DefaultConnection")));
                 services.AddScoped<IParentescoQueries>(conns => new ParentescoQueries(Configuration.GetConnectionString("DefaultConnection")));
                 services.AddScoped<IPlanQueries>(conns => new PlanQueries(Configuration.GetConnectionString("DefaultConnection")));
                 services.AddScoped<IProvinciaQueries>(conns => new ProvinciaQueries(Configuration.GetConnectionString("DefaultConnection")));
                 services.AddScoped<ILocalidadQueries>(conns => new LocalidadQueries(Configuration.GetConnectionString("DefaultConnection")));
                 services.AddScoped<IParentescoQueries>(conns => new ParentescoQueries(Configuration.GetConnectionString("DefaultConnection"))); */
            services.AddDbContext<TareasContext>(opt =>
                      opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Application")));


            services.AddDbContext<IntegrationEventLogContext>(options =>
                  {
                      options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                                              sqlServerOptionsAction: sqlOptions =>
                                              {
                                                  sqlOptions.MigrationsAssembly("Infrastructure");
                                                  //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                                  sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                              });
                  });


            services.AddHttpClient();
            //services.AddOdataSwaggerSupport();
            //services.AddTransient<AfiliadoModificadoIntegrationEventHandler>();
            //services.AddTransient<IAfiliacionIntegrationEventService, AfiliacionIntegrationEventService>();
            services.AddEventBus(Configuration);

            //services.AddOptions();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TareasContext TareasContext)
        {


            Dictionary<Type, IResultError> exceptions = new Dictionary<Type, IResultError>();
            exceptions.Add(typeof(IInvalidException), new InvalidResultError());
            exceptions.Add(typeof(IForbiddenException), new ForbiddenResultError());
            exceptions.Add(typeof(InvalidOperationException), new InvalidResultError());
            exceptions.Add(typeof(INotFoundException), new NotFoundResultError());

            app.UseMiddleware<ExceptionMiddleware>(exceptions);

            if (env.IsDevelopment())
            {

                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Referencias v1"));



            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
                       {
                           endpoints.MapControllers();
                           /* endpoints.Select().Filter().OrderBy().Count().MaxTop(100);
                           endpoints.MapODataRoute("odata", "odata", GetEdmModel()); */
                       });




            ConfigureEventBus(app);

        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            //eventBus.Subscribe<AfiliadoModificadoIntegrationEvent, AfiliadoModificadoIntegrationEventHandler>();
            //eventBus.Subscribe<OrderStartedIntegrationEvent, OrderStartedIntegrationEventHandler>();
        }
        private IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            //odataBuilder.EntitySet<Usuario>("UsuarioQuery");

            return odataBuilder.GetEdmModel();
        }
    }


    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                       sp => (DbConnection c) => new IntegrationEventLogService(c));

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();


                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                {
                    factory.UserName = configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                {
                    factory.Password = configuration["EventBusPassword"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = configuration["SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                ILifetimeScope iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }
    }
}
