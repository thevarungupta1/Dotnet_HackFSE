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
using Excel = Microsoft.Office.Interop.Excel;       //microsoft Excel 14 object in references-> COM tab
using System.Data;
//using Microsoft.Office.Interop.Excel;
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

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ReportContext>();
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
            if(ext == ".xlsx")
            getExcelFile(e.FullPath);
            // Can change program state (set invalid state) in this method.
            // ... Better to use insensitive compares for file names.
        }

        public static void getExcelFile(string filePath)
        {
            var dtContent = GetDataTableFromExcel(filePath);
            FileProcessor.DataTableProcess(dtContent);
            //var res = from DataRow dr in dtContent.Rows
            // where (string)dr[“Name”] == “Gil”
            // select ((string)dr[“Section”]).FirstOrDefault();
            //foreach (DataRow dr in dtContent.Rows)
            //{
            //    Console.WriteLine(dr["Venue Address"]);
            //}
        }
        private static DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                if (ws.Dimension != null && ws.Dimension.End != null)
                {
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.Rows.Add();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }
                }
                return tbl;
            }
        }          

    }
}
