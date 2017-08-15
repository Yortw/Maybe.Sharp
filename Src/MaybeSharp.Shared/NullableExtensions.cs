using System;
using System.Collections.Generic;
using System.Text;

namespace MaybeSharp.Extensions
{
	/// <summary>
	/// Provides extensions to <see cref="Nullable{T}"/> values for working with <see cref="Maybe{T}"/> types.
	/// </summary>
	public static class NullableExtensions
	{
		/// <summary>
		/// Converts a <see cref="Nullable{T}"/> to a <see cref="Maybe{T}"/> value.
		/// </summary>
		/// <typeparam name="T">The type of value used in both the nullable and maybe instances.</typeparam>
		/// <param name="source">A <see cref="Nullable{T}"/> to be turned into a <see cref="Maybe{T}"/> instance.</param>
		/// <returns>A <see cref="Maybe{T}"/> instance, either an empty instance if <paramref name="source"/> is null, otherwise a non-empty instance containing the value from <paramref name="source"/>.</returns>
		public static Maybe<T> ToMaybe<T>(this T? source) where T : struct
		{
			if (source == null) return Maybe<T>.Nothing;
			
			return (Maybe<T>)source.Value;
		}
	}
}