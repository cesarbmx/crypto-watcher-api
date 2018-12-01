using System;
using System.IO;
using CryptoWatcher.Service.Configuration;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure service
            ServiceConfig.Configure();
        }
    }
}
