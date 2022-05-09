using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace C2022.ApiManager
{
    public class HttpConfig
    {
        public async Task<string> GetJsonResponse(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string response = await httpClient.GetStringAsync(httpClient.BaseAddress);

                if (string.IsNullOrWhiteSpace(response))
                {
                    return "";
                }
                return response;
            }
        }

    }
}
