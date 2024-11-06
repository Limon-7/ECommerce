using Ordering.Application.Common.Interfaces;

namespace Ordering.Infrastructure.Services;

public class DateTimeService:IDateTime
{
    public DateTime Now=>DateTime.Now;
}