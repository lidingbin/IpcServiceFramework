using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using JKang.IpcServiceFramework.Hosting;

namespace IpcServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddScoped<IInterProcessService, InterProcessService>();
            })
            .ConfigureIpcHost(builder =>
            {
                // configure IPC endpoints
                builder.AddNamedPipeEndpoint<IInterProcessService>(pipeName: "pipeinternal");
            })
            .ConfigureLogging(builder =>
            {
                // optionally configure logging
                //builder.SetMinimumLevel(LogLevel.Information);
            });
    }

    public interface IInterProcessService
    {
        string ReverseString(string input);
    }

    class InterProcessService : IInterProcessService
    {
        public string ReverseString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
