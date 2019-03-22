using Newtonsoft.Json;
using Outreach.Reporting.Business.Processors;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Outreach_Reporting_System
{
    public static class FileReader
    {
        static HttpClient client = new HttpClient();
        static async Task<bool> SendFilePathAsync(string filePath)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(
               "api/File/ReadExcel", filePath);
                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static void SendFilePath(string filePath)
        {
            RunAsync(filePath).GetAwaiter().GetResult();
        }
      
        static async Task RunAsync(string filePath)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:49552/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var url = await SendFilePathAsync(filePath);
            }
            catch (Exception ex)
            {

            }
        }
              
    }
}
