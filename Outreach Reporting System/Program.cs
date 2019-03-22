using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Runtime.InteropServices;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Data;
using System.Data.OleDb;
using System.Text;
using Outreach.Reporting.Business.Processors;

namespace Outreach_Reporting_System
{
    public class Program
    {
        /// <summary>
        /// Watcher.
        /// </summary>
        static FileSystemWatcher _watcher;
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
           // var host = new WebHostBuilder()
           // .UseKestrel()
           // .UseContentRoot(Directory.GetCurrentDirectory())
           // .UseIISIntegration()
           // .UseStartup<Startup>()
           //// .UseUrls("http://localhost.backend.com:80/")
           // .Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ReportDBContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            //host.Run();

            string directory = @"C:\Project\";
            Program._watcher = new FileSystemWatcher(directory);
            Program._watcher.Created +=
                new FileSystemEventHandler(Program._watcher_Changed);
            Program._watcher.EnableRaisingEvents = true;
            Program._watcher.IncludeSubdirectories = true;
            //host.Run();
            CreateWebHostBuilder(args).Build().Run();           
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        /// <summary>
        /// Handler.
        /// </summary>
        static void _watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string ext = Path.GetExtension(e.FullPath);
            if (ext == ".xlsx")
                FileReader.SendFilePath(e.FullPath);
            // Can change program state (set invalid state) in this method.
            // ... Better to use insensitive compares for file names.
        }

    }
}
