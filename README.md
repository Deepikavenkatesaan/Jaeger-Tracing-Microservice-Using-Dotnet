### Alpaca API Console Application with OpenTelemetry for Jaeger-traced Stock Market Microservices

## Introduction:

This console application leverages the Alpaca API to retrieve and display orders. It is enhanced with OpenTelemetry for distributed tracing, enabling detailed monitoring of order operations. 

The application utilizes Jaeger for tracing, providing comprehensive insights into the flow of execution.

## Instructions:

### API Key Setup:

Replace the placeholder API keys in the new SecretKey() constructor with your own Alpaca API credentials.

### Running the Application:

Execute the program to retrieve and display orders.

The application will display detailed information about each order.

### OpenTelemetry Integration:

The application incorporates OpenTelemetry for distributed tracing.

Traces are captured for each order operation to provide insights into the flow of execution.

### Error Handling:

In case of any errors during the process, the application will display an error message with details.

## Function Signature:

  ```csharp
public class AlpacaApplication
{
    private readonly Tracer tracer;

    public AlpacaApplication(TracerProvider tracerProvider)
    {
        // Constructor logic...
    }

    public async Task RunAsync()
    {
        // Function logic...
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        // Main method logic...
    }
}

## Description:
This asynchronous function initiates the retrieval of orders from the Alpaca API. For each order, it captures distributed traces using OpenTelemetry.



### Example Usage:

```csharp
using System;

class Program
{
    static async Task Main(string[] args)
    {
        // Create an instance of the AlpacaApplication
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

        // Run the application to retrieve and display orders
        await app.RunAsync();
    }
}
