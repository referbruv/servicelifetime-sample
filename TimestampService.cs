using System;
using Microsoft.Extensions.DependencyInjection;

namespace ScopedServices
{
    public interface ITimestampService
    {
        long GetCurrentTimestamp();
    }

    public abstract class TimestampServiceBase 
    {
        public abstract long GetCurrentTimestamp();
    }

    public class TimestampService : TimestampServiceBase, ITimestampService
    {
        public override long GetCurrentTimestamp()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }

    public interface IDateTimeService
    {
        string PrintCurrentDateTime();
    }

    public class DateTimeService : IDateTimeService
    {
        private readonly IServiceProvider sp;
        public DateTimeService(IServiceProvider sp)
        {
            this.sp = sp;
        }
        public string PrintCurrentDateTime()
        {
            using (var scope = sp.CreateScope())
            {
                var tsService = scope.ServiceProvider.GetRequiredService<ITimestampService>();

                return $"CurrentDate: {DateTime.Now}, Timestamp: {tsService.GetCurrentTimestamp()}";
            }
        }
    }
}