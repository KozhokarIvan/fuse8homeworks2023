using System.Text;

namespace Fuse8_ByteMinds.SummerSchool.Domain;

public static class DomainExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => enumerable is null || enumerable.Count() == 0;
    public static string JoinToString<T>(this IEnumerable<T> enumerable, string separator)
    {
        StringBuilder sb = new StringBuilder(enumerable.Count() * separator.Length);
        foreach (var item in enumerable) 
            sb.Append(item + separator);
        return sb.ToString();
    }
    public static int DaysCountBetween(this DateTimeOffset firstDateTimeOffset, DateTimeOffset secondDateTimeOffset)
    {
        TimeSpan timeSpan = new TimeSpan(Math.Abs(firstDateTimeOffset.Ticks - secondDateTimeOffset.Ticks));
        int daysBetweenDates = (int)(timeSpan.Ticks / TimeSpan.TicksPerDay);
        return daysBetweenDates;
    }
}