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
using Microsoft.Office.Interop.Excel;
using Microsoft.Extensions.DependencyInjection;
using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Data;

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

            host.Run();

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
            Console.WriteLine("CHANGED, NAME: " + e.Name);
            Console.WriteLine("CHANGED, FULLPATH: " + e.FullPath);
            //getExcelFile(e.FullPath);
            // Can change program state (set invalid state) in this method.
            // ... Better to use insensitive compares for file names.
        }
        //void LoadWorksheetInDataTable(string fileName, string sheetName)
        //{
        //    System.Data.DataTable sheetData = new System.Data.DataTable();
        //    using (OLEDBConnection conn = this.returnConnection(fileName))
        //    {
        //        conn.Open();
        //        // retrieve the data using data adapter
        //        OleDbDataAdapter sheetAdapter = new OleDbDataAdapter("select * from [" + sheetName + "]", conn);
        //        sheetAdapter.Fill(sheetData);
        //    }
        //    var x= sheetData;
        //}

        //private OleDbConnection returnConnection(string fileName)
        //{
        //    return new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + "; Jet OLEDB:Engine Type=5;Extended Properties=\"Excel 8.0;\"");
        //}

        //public static void getExcelFile(string filePath)
        //{
        //    FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

        //    //1. Reading from a binary Excel file ('97-2003 format; *.xls)
        //    IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
        //    //...
        //    //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
        //    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //    //...
        //    //3. DataSet - The result of each spreadsheet will be created in the result.Tables
        //    DataSet result = excelReader.AsDataSet();
        //    //...
        //    //4. DataSet - Create column names from first row
        //    excelReader.IsFirstRowAsColumnNames = true;
        //    DataSet result = excelReader.AsDataSet();

        //    //5. Data Reader methods
        //    while (excelReader.Read())
        //    {
        //        //excelReader.GetInt32(0);
        //    }

        //    //6. Free resources (IExcelDataReader is IDisposable)
        //    excelReader.Close();
        //}

        //public static void getExcelFile(string filePath)
        //{
      

        //    //Create COM Objects. Create a COM object for everything that is referenced
        //    Excel.Application xlApp = new Excel.Application();

        //    Excel.Workbooks xlWorkbookS = xlApp.Workbooks;
        //    Excel.Workbook xlWorkbook = xlWorkbookS.Open(filePath);

        //    //Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
        //    Excel._Worksheet xlWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[1];
        //    Excel.Range xlRange = xlWorksheet.UsedRange;

        //    int rowCount = xlRange.Rows.Count;
        //    int colCount = xlRange.Columns.Count;

        //    //iterate over the rows and columns and print to the console as it appears in the file
        //    //excel is not zero based!!
        //    for (int i = 1; i <= rowCount; i++)
        //    {
        //        for (int j = 1; j <= colCount; j++)
        //        {
        //            //new line
        //            if (j == 1)
        //                Console.Write("\r\n");

        //            //write the value to the console
        //            //if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
        //            //    Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
        //        }
        //    }

        //    //cleanup
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();

        //    //rule of thumb for releasing com objects:
        //    //  never use two dots, all COM objects must be referenced and released individually
        //    //  ex: [somthing].[something].[something] is bad

        //    //release com objects to fully kill excel process from running in the background
        //    Marshal.ReleaseComObject(xlRange);
        //    Marshal.ReleaseComObject(xlWorksheet);

        //    //close and release
        //    xlWorkbook.Close();
        //    Marshal.ReleaseComObject(xlWorkbook);
        //    Marshal.ReleaseComObject(xlWorkbookS);
        //    //quit and release
        //    xlApp.Quit();
        //    Marshal.ReleaseComObject(xlApp);

           
        //}

    }
}
