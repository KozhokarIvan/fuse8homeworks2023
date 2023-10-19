namespace Fuse8_ByteMinds.SummerSchool.Domain;

/// <summary>
/// Контейнер для значения, с отложенным получением
/// </summary>
public class Lazy<TValue>
{
    // TODO Реализовать ленивое получение значение при первом обращении к Value

    private bool _isCalculated = false;
    private TValue? _value;
    public TValue? Value
    {
        get
        {
            if (!_isCalculated)
            {
                _isCalculated = true;
                _value = _func.Invoke();
            }
            return _value;
        }
    }
    public readonly Func<TValue?> _func;
    public Lazy(Func<TValue?> value)
    {
        _func = value;
    }
}