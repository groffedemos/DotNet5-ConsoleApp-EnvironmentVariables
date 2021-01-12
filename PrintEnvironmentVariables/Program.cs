using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace PrintEnvironmentVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .CreateLogger();
            logger.Information(
                "*** Testando a leitura de variaveis de ambiente ***");

            if (args.Length != 1 || String.IsNullOrWhiteSpace(args[0]))
            {
                logger.Error(
                    "Informe como unico parametro uma string com o nome das " +
                    "variaveis de ambiente separado por '|'!");
                return;
            }

            var envVars = args[0].Split('|', StringSplitOptions.RemoveEmptyEntries);

            logger.Information("# Utilizando Environment.GetEnvironmentVariable #");
            foreach (var envVar in envVars)
                logger.Information($"{envVar} = {Environment.GetEnvironmentVariable(envVar)}");

            logger.Information("# Utilizando um objeto IConfiguration #");
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables().Build();
            foreach (var envVar in envVars)
                logger.Information($"{envVar} = {configuration[envVar]}");

            logger.Information(
                "*** Concluida a leitura de variaveis de ambiente! ***");
        }
    }
}