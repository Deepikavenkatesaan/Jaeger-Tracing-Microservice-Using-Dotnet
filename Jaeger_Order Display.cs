using System;
using System.Linq;
using System.Threading.Tasks;
using Alpaca.Markets;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

public class AlpacaApplication
{
    private readonly Tracer tracer;
    public AlpacaApplication(TracerProvider tracerProvider)
    {
        tracer = tracerProvider.GetTracer("AlpacaApp");
    }

    public async Task RunAsync()
    {
        Console.WriteLine("Alpaca API Console Application - Retrieve and Display Orders");

        try
        {
            var alpacaClient = Environments.Paper.GetAlpacaTradingClient(new SecretKey("PKAE1F88O4AUKEYAK9VZ", "G5cPJrAkARfeD3eFQ3QoGfyLmzqfQHz9DrxrZfpX"));
            var orders = await alpacaClient.ListOrdersAsync(new ListOrdersRequest());

            if (orders.Any())
            {
                Console.WriteLine("\nOrders:");
                foreach (var order in orders)
                {
                    using var span = tracer.StartActiveSpan("Alpaca-Order-Operation");

                    try
                    {
                        Console.WriteLine($"Order ID: {order.OrderId}");
                        Console.WriteLine($"Symbol: {order.Symbol}");
                        Console.WriteLine($"Side: {order.OrderSide}");
                        Console.WriteLine($"Quantity: {order.Quantity}");
                        Console.WriteLine($"Type: {order.OrderType}");
                        Console.WriteLine($"Status: {order.OrderStatus}");
                        Console.WriteLine();
                    }
                    finally
                    {
                        span.End();
                    }
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

public class Program
{
    public static async Task Main(string[] args)
    {
        using var openTelemetry = Sdk.CreateTracerProviderBuilder()
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Order History"))
            .AddSource("AlpacaApplication")
            .AddHttpClientInstrumentation()
            .AddJaegerExporter(configure: jaegerexporter =>
            {
                jaegerexporter.AgentHost = "54.196.55.118";
                jaegerexporter.AgentPort = 6831;
            })
            .Build();

        var app = new AlpacaApplication(openTelemetry);
        await app.RunAsync();
    }
}
