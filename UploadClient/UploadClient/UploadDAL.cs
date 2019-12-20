using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace UploadClient
{
    public class UploadDAL : IUploadDAL
    {
        private readonly HttpClient _httpClient;

        public string ResponseBody { get; private set; }
        public UploadDAL(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async void GetResponse(string requestEndpoint, string token)
        {
            ResponseBody = string.Empty;
            try
            {
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = _httpClient.GetAsync(requestEndpoint).Result;
                response.EnsureSuccessStatusCode();
                ResponseBody = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + _httpClient.BaseAddress + requestEndpoint);
            }
        }
        public async void GetPostResponse(string requestEndpoint, string jsonInString)
        {
            ResponseBody = string.Empty;
            try
            {
                HttpResponseMessage response = _httpClient.PostAsync(requestEndpoint, new StringContent(jsonInString, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();
                ResponseBody = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + _httpClient.BaseAddress + requestEndpoint);
            }
        }
    }
}
