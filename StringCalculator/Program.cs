using Microsoft.Extensions.DependencyInjection;
using StringCalculator.Entities;
using StringCalculator.Service;
using StringCalculator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            // Get the Calculator service
            var calculatorService = serviceProvider.GetService<ICalculatorService>();

            Console.WriteLine("Welcome to the Calculator. Press Ctrl+C to exit.");


            while (true)
            {
                try
                {
                    Console.WriteLine("Enter input:");
                    string input = Console.ReadLine();

                    var result = calculatorService.Evaluate(input, OperationType.Add);
                    Console.WriteLine($"Result: {result.result}");
                    Console.WriteLine($"Formula: {result.formula}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<IParser, Parser>();
            services.AddTransient<IValidator, Validator>();
            services.AddTransient<ICalculatorService, CalculatorService>();

            return services.BuildServiceProvider();
        }
    }
}   
 
