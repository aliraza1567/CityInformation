using CityInformation.Api.Services;
using CityInformation.Database;
using CityInformation.Database.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace CityInformation.Api
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            environment.ConfigureNLog("nlog.config");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(options => options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));
            //.AddJsonOptions(options =>
            //{
            //    if (options.SerializerSettings.ContractResolver != null)
            //    {
            //        var castResolver = options.SerializerSettings.ContractResolver as DefaultContractResolver;

            //        if (castResolver != null)
            //            castResolver.NamingStrategy = null;
            //    }
            //} );

            services.AddTransient<IMailService, MailService>();
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();

            //Create System Variable for Database Connection String with the Name of connectionString:CityInformationDbConnectionString

            var connectionString = Configuration["connectionString:CityInformationDbConnectionString"];

            services.AddDbContext<CityInformationContext>(builder => builder.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CityInformationContext cityInformationContext)
        {
            loggerFactory.AddNLog();
            app.AddNLogWeb();

            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            cityInformationContext.DataSeed();
            app.UseStatusCodePages();

            app.UseMvc();
            
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
