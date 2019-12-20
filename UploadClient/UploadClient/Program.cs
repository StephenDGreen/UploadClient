using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;

namespace UploadClient
{
    class Program
    {
#if DEBUG
        private const string AppSettingsFileName = "appsettings.json";
#else
        private const string AppSettingsFileName = "appsettings.Release.json";
#endif
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<ConsoleApplication>().Run(args);
        }		
		private static IServiceCollection ConfigureServices()
        {
            IConfigurationBuilder cbuilder = new ConfigurationBuilder()
                .AddJsonFile($"{AppSettingsFileName}", optional: false, reloadOnChange: true);
            IConfigurationRoot configuration = cbuilder.Build();

            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient<IUploadDAL, UploadDAL>(client =>
            {
                client.BaseAddress = new Uri(configuration["MainEndpoint"]);
                client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }).ConfigureHttpMessageHandlerBuilder(c =>
            {
                c.PrimaryHandler =
                  new HttpClientHandler()
                  {
                      Proxy = new WebProxy(),
                      UseProxy = true
                  };
                c.Build();
            });
            services.AddTransient<IUploadHandler, UploadHandler>();
            services.AddTransient<ILogin, Login>(l => new Login(configuration["Email"].ToString(), configuration["Pwd"].ToString()));
            services.AddTransient<IGetToken, GetToken>(t => new GetToken(configuration, t.GetRequiredService<IUploadDAL>(), t.GetRequiredService<IUploadHandler>(), t.GetRequiredService<ILogin>()));
            services.AddTransient<IAppFiles, AppFiles>(t => new AppFiles(configuration, t.GetRequiredService<IUploadDAL>(), t.GetRequiredService<IUploadHandler>()));
            services.AddSingleton<ConsoleApplication>();
            return services;
        }    
    }
}
