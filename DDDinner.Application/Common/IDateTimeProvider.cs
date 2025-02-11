namespace DDDinner.Application.Common
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
