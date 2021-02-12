using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OpenIddictPractice.Client.ServerWorker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var client = new HttpClient();

            try
            {
                var token = await GetTokenAsync(client);
                Console.WriteLine($"Access token: {token}");

                var resource = await GetResourceAsync(client, token);
                Console.WriteLine($"API response: {resource}");

                Console.ReadKey();
            }
            catch (System.Exception)
            {
                Console.WriteLine("Auth fail");
            }
        }

        private static async Task<string> GetTokenAsync(HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/connect/token")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "client_credentials",
                    ["client_id"] = "console",
                    ["client_secret"] = "388D45FA-B36B-4988-BA59-B187D329C207"
                })
            };

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            var payload = await response.Content.ReadFromJsonAsync<OpenIddictResponse>();

            return payload.AccessToken;
        }

        private static async Task<string> GetResourceAsync(HttpClient client, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/api/message");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
