using System;
using System.Collections.Generic;
using System.Text;

namespace MaybeSharp.Extensions
{
	/// <summary>
	/// Provides extensions to <see cref="IDictionary{TKey, TValue}"/> for working with <see cref="Maybe{T}"/>.
	/// </summary>
	public static class MaybeDictionaryExtensions
	{

		/// <summary>
		/// Attempts to retrieve a value with the sepcified key from the given collection and returns the result as a <see cref="Maybe{T}"/>. If the key is not found, an empty maybe is returned.
		/// </summary>
		/// <typeparam name="TKey">The type of key stored in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of value stored in the dictionary.</typeparam>
		/// <param name="source">The dictionary to retrieve a value from.</param>
		/// <param name="key">The key of the value to retrieve.</param>
		/// <returns>A <see cref="Maybe{T}"/> containing the retrieved value, or an empty <see cref="Maybe{T}"/> if the key was not found in the dictionary.</returns>
		/// <exception cref="System.ArgumentNullException">Thrhwon if <paramref name="source"/> is null.</exception>
		public static Maybe<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));

			TValue value;
			if (source.TryGetValue(key, out value))
				return value;

			return Maybe<TValue>.Nothing;
		}

	}
}