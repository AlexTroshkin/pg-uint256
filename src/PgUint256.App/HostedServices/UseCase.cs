using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PgUint256.Data;
using System.Numerics;
using static System.Console;

namespace PgUint256.App.HostedServices;

public static partial class LOG
{
    [LoggerMessage(
        EventId   = 1945853840,
        EventName = nameof(UNHANDLED_EXCEPTION),
        Level     = LogLevel.Error,
        Message   = "Unhandled exception")]
    public static partial void UNHANDLED_EXCEPTION(ILogger logger, Exception Exception);

    [LoggerMessage(
        EventId   = 361353657,
        EventName = nameof(DISPLAY_EXIT_CODE),
        Level     = LogLevel.Information,
        Message   = "Exiting with return code: {ExitCode}")]
    public static partial void DISPLAY_EXIT_CODE(ILogger logger, Int32? ExitCode);
}

public class UseCase : IHostedService
{
    private Int32? exitCode;

    private readonly IServiceProvider         serviceProvider;
    private readonly ILogger<UseCase>         logger;
    private readonly IHostApplicationLifetime appLifetime;

    public UseCase(
        IServiceProvider         serviceProvider,
        ILogger<UseCase>         logger,
        IHostApplicationLifetime appLifetime)
    {
        this.serviceProvider = serviceProvider;
        this.logger          = logger;
        this.appLifetime     = appLifetime;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                try
                {
                    using var scope = serviceProvider.CreateScope();

                    var db = scope.ServiceProvider.GetRequiredService<Db>();

                    //---------------------------------------------------------------
                    // ACCOUNTS SORTED BY DESCENDING BALANCE
                    //---------------------------------------------------------------

                    {
                        var accounts = await db.Accounts
                            .OrderByDescending(x => x.Amount)
                            .Where(x => x.Amount >= UInt64.MaxValue)
                            .ToListAsync(cancellationToken);

                        WriteLine("----------------------------------------------");
                        WriteLine("- ACCOUNTS SORTED BY DESCENDING BALANCE       ");
                        WriteLine("----------------------------------------------");

                        foreach (var account in accounts)
                        {
                            WriteLine("");

                            WriteLine($"Id      : {account.Id}");
                            WriteLine($"Address : {account.Address}");
                            WriteLine($"Amount  : {account.Amount}");
                        }
                    }

                    //---------------------------------------------------------------
                    // QUERY WITH ARITHMETIC OPERATIONS
                    //---------------------------------------------------------------

                    {
                        var add = new BigInteger(92372987429387216L);
                        var sub = new BigInteger(3274812371029820L);
                        var mul = new BigInteger(10);
                        var div = new BigInteger(2);

                        var accounts = await db.Accounts
                            .Select(acc => new
                            {
                                Id      = acc.Id,
                                Address = acc.Address,
                                Amount  = (acc.Amount + add - sub) * mul / div //Attention: the presence of parentheses affects the query
                            })
                            .ToListAsync(cancellationToken);

                        WriteLine("----------------------------------------------");
                        WriteLine("- QUERY WITH ARITHMETIC OPERATIONS            ");
                        WriteLine("----------------------------------------------");

                        foreach (var account in accounts)
                        {
                            WriteLine("");

                            WriteLine($"Id      : {account.Id}");
                            WriteLine($"Address : {account.Address}");
                            WriteLine($"Amount  : {account.Amount}");
                        }
                    }

                    exitCode = 0;
                }
                catch (Exception e)
                {
                    LOG.UNHANDLED_EXCEPTION(logger, e);
                    exitCode = 1;
                }
                finally
                {
                    appLifetime.StopApplication();
                }
            });
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        LOG.DISPLAY_EXIT_CODE(logger, exitCode);
        
        Environment.ExitCode = exitCode.GetValueOrDefault(-1);
        return Task.CompletedTask;
    }
}
