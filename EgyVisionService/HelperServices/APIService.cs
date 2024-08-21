using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace EgyVisionService.HelperServices
{
    public static class APIService
    {
        public static string getToken([FromServices] IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath).AddJsonFile("appsettings.json");
            var Configuration = builder.Build();
            string url = Configuration.GetSection("ApplicationSettings:ApiUrl").Value.ToString() + "api/Tisr?audience=" + Configuration.GetSection("ApplicationSettings:audience").Value.ToString();

            string secretKey = Configuration.GetSection("ApplicationSettings:ApiUserName").Value.ToString();
            string AccessKey = Configuration.GetSection("ApplicationSettings:ApiPass").Value.ToString();

            // Testing Basic Authentication
            using (HttpClient client = new HttpClient())
            {
                string creds = String.Format("{0}:{1}", secretKey, AccessKey);
                byte[] bytes = Encoding.ASCII.GetBytes(creds);
                var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
                client.DefaultRequestHeaders.Authorization = header;

                string response = String.Empty;
                var responseMessage = client.GetAsync(url)
                .Result;
                if (responseMessage.IsSuccessStatusCode)
                    response = responseMessage.Content.ReadAsStringAsync().Result;

                return response.ToString();
            }
        }
    }
}
