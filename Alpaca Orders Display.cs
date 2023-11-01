using System;
using System.Linq;
using System.Threading.Tasks;
using Alpaca.Markets;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Alpaca API Console Application - Retrieve and Display Orders");
        await RetrieveAndDisplayOrdersAsync();
    }

    static async Task RetrieveAndDisplayOrdersAsync()
    {
        try
        {
            var alpacaClient = Environments.Paper.GetAlpacaTradingClient(new SecretKey("PKQXY66TMDHLUTYEWVXG", "YNYnaq3s80lgN5MAb2E4QQmyePWr49kFHL6XU6aq"));
            var orders = await alpacaClient.ListOrdersAsync(new ListOrdersRequest());

            if (orders.Any())
            {
                Console.WriteLine("\nOrders:");
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}");
                    Console.WriteLine($"Symbol: {order.Symbol}");
                    Console.WriteLine($"Side: {order.OrderSide}");
                    Console.WriteLine($"Quantity: {order.Quantity}"); // Use Quantity instead of OrderQty
                    Console.WriteLine($"Type: {order.OrderType}");
                    Console.WriteLine($"Status: {order.OrderStatus}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No orders found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
