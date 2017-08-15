using System;
using System.Collections.Generic;
using System.Text;

namespace MaybeSharp.Extensions
{
	/// <summary>
	/// Provides extension methods to <see cref="System.String"/> for working with <see cref="Maybe{T}"/> values;
	/// </summary>
	public static class MaybeStringExtensions
	{

		/// <summary>
		/// Attempts to parse a string to a <see cref="Maybe{T}"/> of <see cref="System.Byte"/>. If the parse fails (or <paramref name="source"/> is null) an empty maybe is returned.
		/// </summary>
		/// <param name="source">The string to parse.</param>
		/// <returns>A <see cref="Maybe{T}"/> containing a valid byte value parsed from the string, or an empty maybe instance if the parse failed.</returns>
		public static Maybe<Byte> TryParseByte(this string source)
		{
			if (source == null) return Maybe<Byte>.Nothing;

			if (Byte.TryParse(source, out Byte result))
				return result;

			return Maybe<Byte>.Nothing;
		}

		/// <summary>
		/// Attempts to parse a string to a <see cref="Maybe{T}"/> of <see cref="System.Int16"/>. If the parse fails (or <paramref name="source"/> is null) an empty maybe is returned.
		/// </summary>
		/// <param name="source">The string to parse.</param>
		/// <returns>A <see cref="Maybe{T}"/> containing a valid Int16 value parsed from the string, or an empty maybe instance if the parse failed.</returns>
		public static Maybe<Int16> TryParseInt16(this string source)
		{
			if (source == null) return Maybe<Int16>.Nothing;

			if (Int16.TryParse(source, out Int16 result))
				return result;

			return Maybe<Int16>.Nothing;
		}

		/// <summary>
		/// Attempts to parse a string to a <see cref="Maybe{T}"/> of <see cref="System.Int32"/>. If the parse fails (or <paramref name="source"/> is null) an empty maybe is returned.
		/// </summary>
		/// <param name="source">The string to parse.</param>
		/// <returns>A <see cref="Maybe{T}"/> containing a valid Int32 value parsed from the string, or an empty maybe instance if the parse failed.</returns>
		public static Maybe<int> TryParseInt(this string source)
		{
			if (source == null) return Maybe<int>.Nothing;

			if (Int32.TryParse(source, out int result))
				return result;

			return Maybe<int>.Nothing;
		}

		/// <summary>
		/// Attempts to parse a string to a <see cref="Maybe{T}"/> of <see cref="System.Int64"/>. If the parse fails (or <paramref name="source"/> is null) an empty maybe is returned.
		/// </summary>
		/// <param name="source">The string to parse.</param>
		/// <returns>A <see cref="Maybe{T}"/> containing a valid Int64 value parsed from the string, or an empty maybe instance if the parse failed.</returns>
		public static Maybe<Int64> TryParseInt64(this string source)
		{
			if (source == null) return Maybe<Int64>.Nothing;

			if (Int64.TryParse(source, out Int64 result))
				return result;

			return Maybe<Int64>.Nothing;
		}

		/// <summary>
		/// Attempts to parse a string to a <see cref="Maybe{T}"/> of <see cref="System.Single"/>. If the parse fails (or <paramref name="source"/> is null) an empty maybe is returned.
		/// </summary>
		/// <param name="source">The string to parse.</param>
		/// <returns>A <see cref="Maybe{T}"/> containing a valid Single value parsed from the string, or an empty maybe instance if the parse failed.</returns>
		public static Maybe<Single> TryParseSingle(this string source)
		{
			if (source == null) return Maybe<Single>.Nothing;

			if (Single.TryParse(source, out Single result))
				return result;

			return Maybe<Single>.Nothing;
		}

		/// <summary>
		/// Attempts to parse a string to a <see cref="Maybe{T}"/> of <see cref="System.Double"/>. If the parse fails (or <paramref name="source"/> is null) an empty maybe is returned.
		/// </summary>
		/// <param name="source">The string to parse.</param>
		/// <returns>A <see cref="Maybe{T}"/> containing a valid Double value parsed from the string, or an empty maybe instance if the parse failed.</returns>
		public static Maybe<Double> TryParseDouble(this string source)
		{
			if (source == null) return Maybe<Double>.Nothing;

			if (Double.TryParse(source, out Double result))
				return result;

			return Maybe<Double>.Nothing;
		}

		/// <summary>
		/// Attempts to parse a string to a <see cref="Maybe{T}"/> of <see cref="System.Boolean"/>. If the parse fails (or <paramref name="source"/> is null) an empty maybe is returned.
		/// </summary>
		/// <param name="source">The string to parse.</param>
		/// <returns>A <see cref="Maybe{T}"/> containing a valid Boolean value parsed from the string, or an empty maybe instance if the parse failed.</returns>
		public static Maybe<Boolean> TryParseBoolean(this string source)
		{
			if (source == null) return Maybe<Boolean>.Nothing;

			if (Boolean.TryParse(source, out Boolean result))
				return result;

			return Maybe<Boolean>.Nothing;
		}

		/// <summary>
		/// Attempts to parse a string to a <see cref="Maybe{T}"/> of <see cref="System.Char"/>. If the parse fails (or <paramref name="source"/> is null) an empty maybe is returned.
		/// </summary>
		/// <param name="source">The string to parse.</param>
		/// <returns>A <see cref="Maybe{T}"/> containing a valid Char value parsed from the string, or an empty maybe instance if the parse failed.</returns>
		public static Maybe<Char> TryParseChar(this string source)
		{
			if (source == null) return Maybe<Char>.Nothing;

			if (Char.TryParse(source, out Char result))
				return result;

			return Maybe<Char>.Nothing;
		}

		/// <summary>
		/// Attempts to parse a string to a <see cref="Maybe{T}"/> of <see cref="System.Decimal"/>. If the parse fails (or <paramref name="source"/> is null) an empty maybe is returned.
		/// </summary>
		/// <param name="source">The string to parse.</param>
		/// <returns>A <see cref="Maybe{T}"/> containing a valid Decimal value parsed from the string, or an empty maybe instance if the parse failed.</returns>
		public static Maybe<Decimal> TryParseDecimal(this string source)
		{
			if (source == null) return Maybe<Decimal>.Nothing;

			if (Decimal.TryParse(source, out Decimal result))
				return result;

			return Maybe<Decimal>.Nothing;
		}

		/// <summary>
		/// Attempts to parse a string to a <see cref="Maybe{T}"/> of <see cref="System.DateTime"/>. If the parse fails (or <paramref name="source"/> is null) an empty maybe is returned.
		/// </summary>
		/// <param name="source">The string to parse.</param>
		/// <returns>A <see cref="Maybe{T}"/> containing a valid DateTime value parsed from the string, or an empty maybe instance if the parse failed.</returns>
		public static Maybe<DateTime> TryParseDateTime(this string source)
		{
			if (source == null) return Maybe<DateTime>.Nothing;

			if (DateTime.TryParse(source, out DateTime result))
				return result;

			return Maybe<DateTime>.Nothing;
		}

		/// <summary>
		/// Attempts to parse a string to a <see cref="Maybe{T}"/> of <see cref="System.DateTimeOffset"/>. If the parse fails (or <paramref name="source"/> is null) an empty maybe is returned.
		/// </summary>
		/// <param name="source">The string to parse.</param>
		/// <returns>A <see cref="Maybe{T}"/> containing a valid DateTimeOffset value parsed from the string, or an empty maybe instance if the parse failed.</returns>
		public static Maybe<DateTimeOffset> TryParseDateTimeOffset(this string source)
		{
			if (source == null) return Maybe<DateTimeOffset>.Nothing;

			if (DateTimeOffset.TryParse(source, out DateTimeOffset result))
				return result;

			return Maybe<DateTimeOffset>.Nothing;
		}

	}
}