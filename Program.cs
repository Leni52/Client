using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client2
{
    class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();
      

        static async Task Main(string[] args)
        {
            var products = await ProcessRepositories();
            foreach (var p in products)
                Console.WriteLine(p.Name);
        }
        private static async Task<List<ProductList>> ProcessRepositories()
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
           
            /*
            var stringTask = httpClient.GetStringAsync("https://localhost:44330/api/");
            var msg = await stringTask;
            Console.Write(msg);
            */

            var streamTask = httpClient.GetStreamAsync("https://localhost:44330/api/products");
            var products = await JsonSerializer.DeserializeAsync<List<ProductList>>(await streamTask);
            return products;


        }


    }
}
