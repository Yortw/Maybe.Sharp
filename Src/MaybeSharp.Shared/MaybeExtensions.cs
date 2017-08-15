using System;
using System.Collections.Generic;
using System.Text;
using MaybeSharp;
using System.Linq;

namespace MaybeSharp.Extensions
{
	/// <summary>
	/// Provides extensions to the <see cref="Maybe{T}"/> type.
	/// </summary>
	public static class MaybeExtensions
	{

		#region Coalescing/Or Extensions

		/// <summary>
		/// Coalesces this value with another. If this value is empty, <paramref name="other"/> is returned, otherwise this is returned.
		/// </summary>
		/// <typeparam name="T">The sub-type of the Maybe{T} instances.</typeparam>
		/// <remarks>
		/// <para>Similar to the <see cref="Coalesce{T}(Maybe{T}, IEnumerable{Maybe{T}})"/> method is lower allocation when using individual instances as no array or enumerale type needs to be created to pass the individual elements.</para>
		/// </remarks>
		/// <param name="source">A Maybe{T} instance to coalesce.</param>
		/// <param name="other">A second Maybe{T} instance to coalesce.</param>
		/// <returns>Either <paramref name="source"/> if it is not empty, otherwise <paramref name="other"/>.</returns>
		public static Maybe<T> Or<T>(this Maybe<T> source, Maybe<T> other)
		{
			if (source.IsEmpty) return other;

			return source;
		}

		/// <summary>
		/// Coalesces this value with one or more others. If <paramref name="source"/> is non-empty it is returned, otherwise the first non-empty value from <paramref name="values"/> is returned. If all values are empty, the last empty value is returned.
		/// </summary>
		/// <typeparam name="T">The sub-type of the Maybe{T} instances.</typeparam>
		/// <param name="source">A Maybe{T} instance to coalesce.</param>
		/// <param name="values">An array of Maybe{T} values to coalesce.</param>
		/// <returns>The first non-empty value (using <paramref name="source"/> then <paramref name="values"/>), otherwise the last value.</returns>
		/// <seealso cref="Or{T}(Maybe{T}, Maybe{T})"/>
		public static Maybe<T> Coalesce<T>(this Maybe<T> source, params Maybe<T>[] values)
		{
			return Coalesce(source, (IEnumerable<Maybe<T>>)values);
		}

		/// <summary>
		/// Coalesces this value with one or more others. If <paramref name="source"/> is non-empty it is returned, otherwise the first non-empty value from <paramref name="values"/> is returned. If all values are empty, the last empty value is returned.
		/// </summary>
		/// <typeparam name="T">The sub-type of the Maybe{T} instances.</typeparam>
		/// <param name="source">A Maybe{T} instance to coalesce.</param>
		/// <param name="values">An enumerable of Maybe{T} values to coalesce.</param>
		/// <returns>The first non-empty value (using <paramref name="source"/> then <paramref name="values"/>), otherwise the last value.</returns>
		/// <seealso cref="Or{T}(Maybe{T}, Maybe{T})"/>
		public static Maybe<T> Coalesce<T>(this Maybe<T> source, IEnumerable<Maybe<T>> values)
		{
			if (!source.IsEmpty) return source;
			if (values == null) return Maybe<T>.Nothing;

			foreach (var value in values)
			{
				if (value.HasValue) return value;
			}
			return Maybe<T>.Nothing;
		}

		#endregion

		#region ValueOr Extensions

		/// <summary>
		/// Returns the value of the Maybe{T} instance if it is non-empty, otherwise returns <paramref name="value"/>.
		/// </summary>
		/// <typeparam name="T">The sub-type of the Maybe{T} instances.</typeparam>
		/// <param name="source">The Mabye{T} instance whose value is returned if it is not empty.</param>
		/// <param name="value">The value to return if the Maybe{T} is empty.</param>
		/// <returns>Either the value of Maybe{T} or <paramref name="value"/>.</returns>
		public static T ValueOr<T>(this Maybe<T> source, T value)
		{
			if (source.IsEmpty) return value;

			return source.Value;
		}

		/// <summary>
		/// Returns the value of the Maybe{T} instance if it is non-empty, otherwise returns the result of the <paramref name="valueFactory"/> function.
		/// </summary>
		/// <typeparam name="T">The sub-type of the Maybe{T} instances.</typeparam>
		/// <param name="source">The Mabye{T} instance whose value is returned if it is not empty.</param>
		/// <param name="valueFactory">A function the is used to calculate the return value if <paramref name="source"/> is empty.</param>
		/// <returns>Either the value of <paramref name="source"/> or the result of calling <paramref name="valueFactory"/> if the former is empty.</returns>
		public static T ValueOr<T>(this Maybe<T> source, Func<T> valueFactory)
		{
			if (source.IsEmpty)
			{
				if (valueFactory == null) return default(T);
				return valueFactory.Invoke();
			}

			return source.Value;
		}

		/// <summary>
		/// Returns the value of <paramref name="source"/> if it is not empty, otherwise throws the provided exception.
		/// </summary>
		/// <typeparam name="T">The sub-type of the Maybe{T} instances.</typeparam>
		/// <param name="source">The Mabye{T} instance whose value is returned if it is not empty.</param>
		/// <param name="exception">The exception to throw if <paramref name="source"/> is empty. If null, an <see cref="System.InvalidOperationException"/> is thrown.</param>
		/// <returns>The value of <paramref name="source"/>.</returns>
		public static T ValueOrException<T>(this Maybe<T> source, Exception exception)
		{
			if (source.IsEmpty) throw exception ?? Maybe<T>.CreateEmptyException();

			return source.Value;
		}

		/// <summary>
		/// Returns the value of <paramref name="source"/> if it is not empty, otherwise throws an exception created from the function provided.
		/// </summary>
		/// <typeparam name="T">The sub-type of the Maybe{T} instances.</typeparam>
		/// <param name="source">The Mabye{T} instance whose value is returned if it is not empty.</param>
		/// <param name="exceptionFactory">A function that builds the exception to be thrown if <paramref name="source"/> is empty. If the function is null or returns null, a <see cref="System.InvalidOperationException"/> is thrown.</param>
		/// <returns>The value of <paramref name="source"/>.</returns>
		public static T ValueOrException<T>(this Maybe<T> source, Func<Exception> exceptionFactory)
		{
			if (source.IsEmpty)
				throw exceptionFactory?.Invoke() ?? Maybe<T>.CreateEmptyException();

			return source.Value;
		}

		#endregion

		#region WhenExtensions

		/// <summary>
		/// Executes the <paramref name="action"/> is this instance is not nothing.
		/// </summary>
		/// <typeparam name="T">The type of value contained within the maybe.</typeparam>
		/// <param name="source">A <see cref="Maybe{T}"/> to check for emptiness.</param>
		/// <param name="action">The action to execute if <paramref name="source"/> is not nothing.</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="action"/> is null.</exception>
		/// <returns>Returns <paramref name="source"/> allowing for chanining of method calls.</returns>
		public static Maybe<T> WhenSomething<T>(this Maybe<T> source, Action<T> action)
		{
			if (action == null) throw new ArgumentNullException(nameof(action));

			if (source.HasValue) action(source.Value);

			return source;
		}

		/// <summary>
		/// Exectues the <paramref name="action"/> only if <paramref name="source"/> is empty.
		/// </summary>
		/// <typeparam name="T">The type of value contained within the maybe.</typeparam>
		/// <param name="source">A <see cref="Maybe{T}"/> to check for emptiness.</param>
		/// <param name="action">The action to execute if <paramref name="source"/> is nothing.</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="action"/> is null.</exception>
		/// <returns>Returns <paramref name="source"/> allowing for chanining of method calls.</returns>
		public static Maybe<T> WhenNothing<T>(this Maybe<T> source, Action action)
		{
			if (action == null) throw new ArgumentNullException(nameof(action));

			if (source.IsEmpty) action();

			return source;
		}

		#endregion

		#region Linq Related Extensions

		/// <summary>
		/// Extension method allowing Maybe{T} instances to be used in LINQ where clauses.
		/// </summary>
		/// <typeparam name="T">The type of value contained by the maybe.</typeparam>
		/// <param name="maybe">A Maybe{T} instance to operate on.</param>
		/// <param name="predicate">The predicate to apply to the Maybe{T} instance.</param>
		/// <returns>Either <paramref name="maybe"/> if it is empty or <paramref name="predicate"/> returns true, otherwise <see cref="Maybe{T}.Nothing"/>.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="predicate"/> is null.</exception>
		public static Maybe<T> Where<T>(this Maybe<T> maybe, Func<T, bool> predicate)
		{
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			if (!maybe.HasValue)
				return maybe;

			if (predicate(maybe.Value))
				return maybe;

			return Maybe<T>.Nothing;
		}

		/// <summary>
		/// Converts a <see cref="Maybe{T}"/> to a Maybe{TResult} using the function provided.
		/// </summary>
		/// <typeparam name="T">The type of value contained in the <paramref name="maybe"/> instance.</typeparam>
		/// <typeparam name="TResult">The type of value in the returned <see cref="Maybe{T}"/> instance.</typeparam>
		/// <param name="maybe">The maybe to convert.</param>
		/// <param name="predicate">A function that converts the value of <paramref name="maybe"/>.</param>
		/// <remarks>
		/// <para>If <paramref name="maybe"/> is nothing then an empty instance of Maybe{TResult} is returned, otherwise the <paramref name="predicate"/> function is used to calculate the return value.</para>
		/// </remarks>
		/// <returns>A Maybe{T} instance that is either nothing, or the result of the <paramref name="predicate"/> function.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="predicate"/> is null.</exception>
		public static Maybe<TResult> Select<T, TResult>(this Maybe<T> maybe, Func<T, Maybe<TResult>> predicate)
		{
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return maybe.HasValue ? predicate(maybe.Value) : Maybe<TResult>.Nothing; 
		}

		/// <summary>
		/// Converts a <see cref="Maybe{T}"/> to a Maybe{TResult} using the function provided.
		/// </summary>
		/// <typeparam name="T">The type of value contained in the <paramref name="maybe"/> instance.</typeparam>
		/// <typeparam name="TResult">The type of value in the returned <see cref="Maybe{T}"/> instance.</typeparam>
		/// <param name="maybe">The maybe to convert.</param>
		/// <param name="predicate">A function that converts the value of <paramref name="maybe"/> to a value of TResult.</param>
		/// <returns>A Maybe{T} instance that is either nothing, or the result of the <paramref name="predicate"/> function.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="predicate"/> is null.</exception>
		public static Maybe<TResult> Select<T, TResult>(this Maybe<T> maybe, Func<T, TResult> predicate)
		{
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return maybe.Bind(v => (Maybe<TResult>)predicate(v));
		}

		/// <summary>
		/// Converts an <see cref="IEnumerable{T}"/> of <see cref="Maybe{T}"/> values to a set of Maybe{TResult} values.
		/// </summary>
		/// <typeparam name="T">The type of value contained with the <see cref="Maybe{T}"/> instances in the <paramref name="source"/> argument.</typeparam>
		/// <typeparam name="TResult">The type of value contained in the <see cref="Maybe{T}"/> instances returned.</typeparam>
		/// <param name="source">The <see cref="IEnumerable{T}"/> to operate on.</param>
		/// <param name="predicate">A function that converts a {T} value to a Maybe{TResult}.</param>
		/// <remarks>
		/// <para>If <paramref name="source"/> is null then an empty set is returned.</para>
		/// </remarks>
		/// <returns>A set of Maybe{TResult} values.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="predicate"/> is null.</exception>
		public static IEnumerable<Maybe<TResult>> Select<T, TResult>(this IEnumerable<Maybe<T>> source, Func<T, Maybe<TResult>> predicate)
		{
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			if (source == null) return new Maybe<TResult>[0];

			return source.Select((m) => m.Select(predicate));
		}

		/// <summary>
		/// Allows values from multiple maybe instances to be combined in the select portion of a LINQ query.
		/// </summary>
		/// <typeparam name="T">The type of the first maybe instance.</typeparam>
		/// <typeparam name="TOther">The type of the second maybe instance.</typeparam>
		/// <typeparam name="TResult">The type of the result of combining the two types.</typeparam>
		/// <param name="maybe">A maybe instance to combine.</param>
		/// <param name="converter">A function that takes the value of <paramref name="maybe"/> (when non-empty) and returns a Maybe{TOther} result.</param>
		/// <param name="combiner">Function that combines values of T and TOther to return a TResult.</param>
		/// <returns>A Maybe{TResult} containing the result of the <paramref name="combiner"/> function. Will be Mabye{T}.Not</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="converter"/> or <paramref name="combiner"/> is null.</exception>
		public static Maybe<TResult> SelectMany<T, TOther, TResult>(this Maybe<T> maybe, Func<T, Maybe<TOther>> converter, Func<T, TOther, TResult> combiner)
		{
			if (converter == null) throw new ArgumentNullException(nameof(converter));
			if (combiner == null) throw new ArgumentNullException(nameof(combiner));

			return maybe.Bind<TResult>
			(
				(a) => converter(a).Bind<TResult>
				(
					(b) => (Maybe<TResult>)combiner(a, b)
				)
			);
		}

		/// <summary>
		/// Converts an <see cref="IEnumerable{T}"/> of <see cref="Maybe{T}"/> values to a set of Maybe{TResult} values, skipping any empty values.
		/// </summary>
		/// <typeparam name="T">The type of value contained with the <see cref="Maybe{T}"/> instances in the <paramref name="source"/> argument.</typeparam>
		/// <typeparam name="TResult">The type of value contained in the <see cref="Maybe{T}"/> instances returned.</typeparam>
		/// <param name="source">The <see cref="IEnumerable{T}"/> to operate on.</param>
		/// <param name="predicate">A function that converts a {T} value to a Maybe{TResult}.</param>
		/// <remarks>
		/// <para>If <paramref name="source"/> is null then an empty set is returned.</para>
		/// </remarks>
		/// <returns>A set of Maybe{TResult} values that excludes any empty values in the source.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="predicate"/> is null.</exception>
		public static IEnumerable<Maybe<TResult>> SelectNonempty<T, TResult>(this IEnumerable<Maybe<T>> source, Func<T, Maybe<TResult>> predicate)
		{
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			if (source == null) return new Maybe<TResult>[0];

			return source.Select((m) => m.Select(predicate)).Where((m) => !m.IsEmpty);
		}

		#endregion

		/// <summary>
		/// Returns a <see cref="Nullable{T}"/> from a <see cref="Maybe{T}"/> where null is used if the maybe instance is empty.
		/// </summary>
		/// <typeparam name="T">The subtype of maybe and nullable to return.</typeparam>
		/// <param name="source">The maybe instance whose value should be returned as a nullable.</param>
		/// <returns>A <see cref="Nullable{T}"/> which is null if the maybe provided is empty.</returns>
		public static T? ToNullable<T>(this Maybe<T> source) where T : struct
		{
			if (source.IsEmpty) return null;

			return (T?)source.Value;
		}

	}
}