using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoreStartApp
{
    public class Program
    {
        /// <summary>
        ///  Точка входа - метод Main
        /// </summary>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Статический метод, создающий и настраивающий IHostBuilder - 
        /// объект, объекь, который в свою очередь создает хост для развертывания Core-приложения
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    // Переопределяем путь до статических файлов по умолчанию
                    webBuilder.UseWebRoot("Views");
                });
    }
}