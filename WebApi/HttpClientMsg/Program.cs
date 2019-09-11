using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientMsg
{
    class Program
    {
        public class Message
        {
            public int Id { get; set; }
            public string Sender { get; set; }
            public string Body { get; set; }
        }

        private const string URL = "http://localhost:50014/api/messages";
        //private const string URL = "http://webapichat110919.azurewebsites.net/api/messages";

        static async Task<Uri> CreateFoodAsync(Message msg, HttpClient client)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/messages", msg);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static void Main(string[] args)
        {
            // POST REQUEST
            HttpClient client_post = new HttpClient();

            client_post.BaseAddress = new Uri(URL);
            client_post.DefaultRequestHeaders.Accept.Clear();
            client_post.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Message food = new Message
            {
                Id = 10,
                Sender = "me",
                Body = "String body"
            };

            var response_post = client_post.PostAsJsonAsync(
                 "", food).Result;

            Console.WriteLine(response_post);


            // GET REQUEST
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<Message>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                foreach (var d in dataObjects)
                {
                    Console.Write("{0} ", d.Id);
                    Console.Write("{0} ", d.Sender);
                    Console.Write("{0} ", d.Body);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
    }
}