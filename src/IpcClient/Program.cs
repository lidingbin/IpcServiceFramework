using JKang.IpcServiceFramework.Client;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IpcClient
{
    class Program
    {
        static void Main(string[] args)
        {

            // register IPC clients
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddNamedPipeIpcClient<IInterProcessService>("client1", pipeName: "pipeinternal")
                .BuildServiceProvider();

            // resolve IPC client factory
            IIpcClientFactory<IInterProcessService> clientFactory = serviceProvider
                .GetRequiredService<IIpcClientFactory<IInterProcessService>>();

            // create client
            IIpcClient<IInterProcessService> client = clientFactory.CreateClient("client1");

            string output = client.InvokeAsync(x => x.ReverseString("lidingbin")).Result;

            Console.WriteLine(output);

            Console.WriteLine("Hello World!");

            Console.ReadLine();
        }
    }

    public interface IInterProcessService
    {
        string ReverseString(string input);
    }
}
