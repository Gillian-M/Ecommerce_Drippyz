using Drippyz.Models;
using System.Net.Http.Headers;

namespace Drippyz.Services;

public class Programe
{
    static void Main()
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://drippyzapi20220507192916.azurewebsites.net/");


        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        ListAllProductEntries(client).Wait();
        ListAllOrderEntries(client).Wait();
        DeleteOrder(client).Wait();

    }
    // List all products from client
    private static async Task ListAllProductEntries(HttpClient client)
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync("api/Products");
            if (response.IsSuccessStatusCode)
            {

                var product = await response.Content.ReadAsAsync<IEnumerable<Product>>();


                Console.WriteLine("List All Products");

                foreach (var entries in product)
                {
                    Console.WriteLine(entries);
                }
            }
            else
            {
                Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }

    // List all Orders from client
    private static async Task ListAllOrderEntries(HttpClient client)
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync("api/OrderItems");
            if (response.IsSuccessStatusCode)
            {
                var orders = await response.Content.ReadAsAsync<IEnumerable<OrderItem>>();

                Console.WriteLine("");
                Console.WriteLine("List All Orders");

                foreach (var entries in orders)
                {
                    Console.WriteLine(entries);
                }
            }
            else
            {
                Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }
    // Delete order method from client 
    private static async Task DeleteOrder(HttpClient client)
    {
        try
        {
            HttpResponseMessage response = await client.DeleteAsync("api/OrderItems/10");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("");
                Console.WriteLine("Order deleted!");
            }
            else
            {
                Console.WriteLine(response.StatusCode.ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }

}

