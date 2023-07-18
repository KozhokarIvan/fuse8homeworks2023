namespace Fuse8_ByteMinds.SummerSchool.Domain;

/// <summary>
/// Модель для хранения денег
/// </summary>
public class Money
{
    public Money(int rubles, int kopeks)
        : this(false, rubles, kopeks)
    {
    }

    public Money(bool isNegative, int rubles, int kopeks)
    {
        if (rubles < 0)
            throw new ArgumentException("Количество рублей не может быть отрицательным");
        if (kopeks < 0 || kopeks > 99)
            throw new ArgumentException("Количество копеек должно быть неотрицательным и меньше 100");
        if (isNegative && rubles == 0 && kopeks == 0)
            throw new ArgumentException("Значение не может быть одновременно нулевым и отрицательным");
        IsNegative = isNegative;
        Rubles = rubles;
        Kopeks = kopeks;
    }

    /// <summary>
    /// Отрицательное значение
    /// </summary>
    public bool IsNegative { get; }

    /// <summary>
    /// Число рублей
    /// </summary>
    public int Rubles { get; }

    /// <summary>
    /// Количество копеек
    /// </summary>
    public int Kopeks { get; }
    public static Money operator +(Money left, Money right)
    {
        int totalKopeks = left.TotalKopeks + right.TotalKopeks;
        return new Money(totalKopeks < 0, Math.Abs(totalKopeks / 100), Math.Abs(totalKopeks % 100));
    }

    public static Money operator -(Money left, Money right)
    {
        int totalKopeks = left.TotalKopeks - right.TotalKopeks;
        return new Money(totalKopeks < 0, Math.Abs(totalKopeks / 100), Math.Abs(totalKopeks % 100));
    }
    public static bool operator >(Money left, Money right)
    {
        if (!left.IsNegative && right.IsNegative) return true;
        if (left.IsNegative && !right.IsNegative) return false;
        return left.TotalKopeks > right.TotalKopeks;
    }
    public static bool operator <(Money left, Money right)
    {
        if (!left.IsNegative && right.IsNegative) return false;
        if (left.IsNegative && !right.IsNegative) return true;
        return left.TotalKopeks < right.TotalKopeks;
    }
    public static bool operator >=(Money left, Money right) => left > right || left.TotalKopeks == right.TotalKopeks;
    public static bool operator <=(Money left, Money right) => left < right || left.TotalKopeks == right.TotalKopeks;
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (!(obj is Money value))
            return false;

        return TotalKopeks.Equals(value.TotalKopeks);
    }
    public override int GetHashCode() => TotalKopeks.GetHashCode();
    public override string ToString() => $"{(IsNegative ? '-' : string.Empty)}{Rubles},{Kopeks} ₽";

    /// <summary>
    /// Количество копеек в сумме с  рублям конвертированными в копейки. Отрицательное значение если IsNegative true
    /// </summary>
    private int TotalKopeks { get => (Kopeks + Rubles * 100) * (IsNegative ? -1 : 1); }
}