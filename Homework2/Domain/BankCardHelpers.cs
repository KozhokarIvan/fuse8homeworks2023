using System.Reflection;

namespace Fuse8_ByteMinds.SummerSchool.Domain;

public static class BankCardHelpers
{
	/// <summary>
	/// Получает номер карты без маски
	/// </summary>
	/// <param name="card">Банковская карта</param>
	/// <returns>Номер карты без маски</returns>
	public static string GetUnmaskedCardNumber(BankCard card)
	{
		string nameOfField = "_number";
        FieldInfo? unmaskedCardNumberField = typeof(BankCard).GetField(nameOfField, BindingFlags.Instance | BindingFlags.NonPublic);
		string? unmaskedCardNumber = unmaskedCardNumberField?.GetValue(card) as string;
		if (unmaskedCardNumber is null) throw new NullReferenceException($"No property named {nameOfField} in class {nameof(BankCard)}");
        return unmaskedCardNumber;
	}
}