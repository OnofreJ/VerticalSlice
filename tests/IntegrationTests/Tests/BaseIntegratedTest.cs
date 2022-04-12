using System;
using System.Threading;
using System.Threading.Tasks;
using Polly;

namespace VerticalSlice.IntegratedTests.Tests
{
    public abstract class BaseIntegratedTest
    {
        protected virtual async Task<TResult> ExecutePolicyAsync<TResult>(Func<TResult, bool> predicate,
            int numberOfRetries,
            double sleepDurantionProviderInSeconds,
            Func<Task<TResult>> action)
        {
            return await Policy.HandleResult(predicate)
                .WaitAndRetryAsync(numberOfRetries, sleep => TimeSpan.FromSeconds(sleepDurantionProviderInSeconds))
                .ExecuteAsync(action);
        }

        protected CancellationToken GetCancellationToken
        {
            get
            {
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(600_000);
                return cancellationTokenSource.Token;
            }
        }
    }
}