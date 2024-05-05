using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.MVC.Interfaces;
using CarRepair.MVC.Settings;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Polly;

namespace CarRepair.MVC.Services
{
    public class DatabaseRetryService : IDatabaseRetryService
    {
        private readonly IAsyncPolicy _retryPolicy;

        private readonly IOptions<DatabaseReconnectSettings> _databaseReconnectSettings;
        private readonly string _logFilePath = @$"{Directory.GetCurrentDirectory()}\Logs\ReconnectLog.txt";

        public DatabaseRetryService(IOptions<DatabaseReconnectSettings> settings)
        {

            _databaseReconnectSettings = settings;

            var retryPolicy = Policy
                .Handle<SqliteException>()
                .WaitAndRetryAsync(
                    _databaseReconnectSettings.Value.RetryCount,
                    retryAttempt => TimeSpan.FromSeconds(_databaseReconnectSettings.Value.RetryWaitPeriodInSeconds),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {

                        File.AppendAllText(_logFilePath, $"Connection lost, retry attempt {retryCount} at {DateTime.Now} . Exception Message: {exception.Message}" + Environment.NewLine);

                    });

            var fallbackPolicy = Policy
                .Handle<SqliteException>()
                .FallbackAsync(
                    fallbackAction: cancellationToken => Task.CompletedTask,
                    onFallbackAsync: async e =>
                    {
                        await Task.Run(() => File.AppendAllText(_logFilePath, $"Failed after maximum retries. Exception Message: {e.Message}" + Environment.NewLine));

                    });

            _retryPolicy = Policy.WrapAsync(fallbackPolicy, retryPolicy);
        }

        public async Task ExecuteWithRetryAsync(Func<Task> action)
        {
            var context = new Context();

            int attempt = 0;
            await _retryPolicy.ExecuteAsync(async (ctx) =>
            {
                attempt++;
                await action();
            }, context);
            File.AppendAllText(_logFilePath, $"Connection successfully reconnected at attempt {attempt} at {DateTime.Now}" + Environment.NewLine);

        }
    }
}