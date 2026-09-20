using Kofoten.NativeCli;
using Kofoten.NativeCli.Generator.Tests;
using System;

public class Program
{
    public static int Main(string[] args)
    {
        try
        {
            // Let's test the happy path with the greedy collection and both boolean flags
            string[] simulatedArgs = new string[] { "-s", "text", "-i", "5", "--datetime-test", "2026-09-20 23:30:00" };

            Console.WriteLine($"Simulating single command app args: {string.Join(" ", simulatedArgs)}\n");

            var command = OptionsTestCommandParser.Parse(simulatedArgs);

            // Execute your handcrafted logic!
            command.Execute();
        }
        catch (AggregateException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Command failed with the following errors:");
            foreach (var inner in ex.InnerExceptions)
            {
                Console.WriteLine($"- {inner.Message}");
            }
            Console.ResetColor();
        }

        try
        {
            // Let's test the happy path with the greedy collection and both boolean flags
            string[] simulatedArgs = new string[] { "test", "options", "-s", "text", "-i", "5", "--datetime-test", "2026-09-20 23:30:00" };

            Console.WriteLine($"Simulating multi command app args: {string.Join(" ", simulatedArgs)}\n");

            var builder = CliCommandBuilder.Configure(router =>
            {
                router.Map("test", sr =>
                {
                    sr.MapOptionsTestCommand("options");
                });
            }, ExceptionHandler);

            var command = builder.ToCommand(simulatedArgs);

            // Execute your handcrafted logic!
            command.Execute();
        }
        catch (AggregateException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Command failed with the following errors:");
            foreach (var inner in ex.InnerExceptions)
            {
                Console.WriteLine($"- {inner.Message}");
            }
            Console.ResetColor();
        }

        return 0;
    }

    static int ExceptionHandler(Exception exception, IServiceProvider _)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        if (exception is CliParseException parseException)
        {
            Console.WriteLine("Command failed with the following errors:");
            foreach (var error in parseException.Errors)
            {
                Console.WriteLine($"- {error}");
            }
            Console.ResetColor();
            Console.WriteLine(parseException.HelpText);
            return 1;
        }

        Console.WriteLine(exception.Message);
        Console.ResetColor();
        Console.WriteLine(exception.StackTrace);
        return 42;
    }
}
