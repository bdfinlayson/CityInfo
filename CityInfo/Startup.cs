﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using CityInfo.Entities;
using Microsoft.EntityFrameworkCore;
using CityInfo.Services;

namespace CityInfo
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));
            // adds serialization to serialize keynames as they are defined in the model
            //.AddJsonOptions(option => 
            //{
            //    if (option.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = option.SerializerSettings.ContractResolver
            //            as DefaultContractResolver;
            //        castedResolver.NamingStrategy = null;
            //    }

            //});

            //var connectionString = Startup.Configuration["connectionStrings:cityInfoDBConnectionString"];
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CityInfo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            services.AddTransient<Services.IMailService, Services.MailService>();
            services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString)); // registering the db context with a scoped lifetime
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug();

            //loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider())
            loggerFactory.AddNLog();

            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            AutoMapper.Mapper.Initialize(configure =>
            {
                configure.CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>();
                configure.CreateMap<Entities.City, Models.CityDto>();
                configure.CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
            });

            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
