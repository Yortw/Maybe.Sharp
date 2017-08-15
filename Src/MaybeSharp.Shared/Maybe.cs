using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace MaybeSharp
{
	/// <summary>
	/// Represents a value that might be 'empty' or a valid value.
	/// </summary>
	/// <typeparam name="T"></typeparam>
#if SUPPORTS_SERIALIZABLE
	[Serializable]
#endif

	public struct Maybe<T> : IEquatable<Maybe<T>>, IFormattable, IComparable, IComparable<Maybe<T>>, IComparable<T>
	{

		#region Fields & Constants

		private readonly T _Value;
		private readonly bool _HasValue;

		private static bool? TIsFormattable;
		private static bool? TIsNullable;

		#endregion

		#region Public Contract
		
		/// <summary>
		/// Represents an empty version of this maybe.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public readonly static Maybe<T> Nothing = new Maybe<T>();

		/// <summary>
		///	Creates an instance that represents the specified value.
		/// </summary>
		/// <remarks>
		/// <para>If {T} is a 'nullable' type (reference type or Nullable{T}) and <paramref name="value"/> is null, then an 'empty' instance is returned. Otherwise a non-empty instance containing <paramref name="value"/> is returned. 
		/// If you want an empty instance of a non-nullable type use Maybe{T}.Nothing (preferred), or new Maybe{T}. New Maybe{T}(default(T)) will return a non-empty instance with the default value of that type.</para>
		/// </remarks>
		/// <param name="value">The value represented by this instance.</param>
		public Maybe(T value)
		{
			_Value = value;
			_HasValue = !IsNull(value);
		}

		/// <summary>
		/// Returns true if this instance is empty.
		/// </summary>
		/// <seealso cref="IsEmpty"/>
		public bool IsEmpty
		{
			get { return !_HasValue; }
		}

		/// <summary>
		/// Returns true if this instance contains a valid, non-empty value.
		/// </summary>
		/// <see cref="IsEmpty"/>
		/// <seealso cref="Value"/>
		public bool HasValue
		{
			get { return !IsEmpty; }
		}

		/// <summary>
		/// If <see cref="HasValue"/> is true, returns the value contained by this instance, otherwise throws a <see cref="InvalidOperationException"/>.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">Thrown if this instance is 'empty' and has no value to return.</exception>
		/// <seealso cref="IsEmpty"/>
		/// <seealso cref="HasValue"/>
		public T Value
		{
			get
			{
				if (IsEmpty) throw CreateEmptyException();
				return _Value;
			}
		}

		/// <summary>
		/// If this maybe is not empty, calls the specified function providing the value of this maybe and returns the result. If this maybe is nothing, returns an empty maybe.
		/// </summary>
		/// <param name="func">The function to call if this maybe is not empty.</param>
		/// <returns>A Maybe{T}. Will be an empty maybe if this maybe is empty.</returns>
		public Maybe<T> Bind(Func<T, Maybe<T>> func)
		{
			if (_HasValue) return func?.Invoke(_Value) ?? Maybe<T>.Nothing;

			return Maybe<T>.Nothing;
		}

		/// <summary>
		/// If this maybe is not empty, calls the specified function providing the value of this maybe and returns the result. If this maybe is nothing, returns an empty maybe.
		/// </summary>
		/// <typeparam name="TResult">The type of value returned from the provided function.</typeparam>
		/// <param name="func">The function to call if this maybe is not empty.</param>
		/// <returns>A Maybe{TResult}. Will be an empty maybe if this maybe is empty.</returns>
		public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> func)
		{
			if (_HasValue) return func?.Invoke(_Value) ?? Maybe<TResult>.Nothing;

			return Maybe<TResult>.Nothing;
		}

		/// <summary>
		/// If this maybe is empty, returns an empty Maybe{TResult}. If this maybe is not empty, casts it's value to TResult and returns a new maybe containing the result. If the cast fails, an empty Maybe{TResult} is returned.
		/// </summary>
		/// <typeparam name="TResult">The type to cast to.</typeparam>
		/// <returns>A Maybe{TResult} instance.</returns>
		public Maybe<TResult> Cast<TResult>()
		{
			if (IsEmpty) return Maybe<TResult>.Nothing;

			try
			{
				return (Maybe<TResult>)(TResult)(object)_Value;
			}
			catch (InvalidCastException)
			{
				return Maybe<TResult>.Nothing;
			}
		}

		/// <summary>
		/// If this maybe is empty, returns an empty Maybe{TResult}. If this maybe is not empty, use the as keyword to convert it's value to TResult and returns a new maybe containing the result.
		/// </summary>
		/// <typeparam name="TResult">The type to cast to.</typeparam>
		/// <returns>A Maybe{TResult} instance.</returns>
		public Maybe<TResult> As<TResult>() where TResult : class
		{
			if (IsEmpty) return Maybe<TResult>.Nothing;

			return (Maybe<TResult>)(_Value as TResult);
		}

		#endregion

		#region Overides

		/// <summary>
		/// Returns the string &lt;nothing&gt; if <see cref="IsEmpty"/> is true, otherwise returns the result of the ToString method of the inner <see cref="Value"/>.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (IsEmpty) return "<nothing>";

			return Value.ToString();
		}

		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A integer that is the hashcode for this instance.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				return (_HasValue.GetHashCode() * 397) ^ EqualityComparer<T>.Default.GetHashCode(_Value);
			}
		}

		/// <summary>
		/// Performs an equality check.
		/// </summary>
		/// <param name="obj">The value to compare to.</param>
		/// <remarks><para>If <paramref name="obj"/> is not an instance of this Maybe{T} type then the result is false, otherwise the result of <see cref="Equals(Maybe{T})"/> is returned.</para></remarks>
		/// <returns>True if <paramref name="obj"/> is considered equal to this instance.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is Maybe<T>)) return false;

			return base.Equals((Maybe<T>)obj);
		}

		#endregion

		#region IEquatable<Maybe<T>> Implementation

		/// <summary>
		/// Performs an equality check.
		/// </summary>
		/// <param name="other">The instance to compare to.</param>
		/// <returns>True if the values are considered equal, otherwise false.</returns>
		public bool Equals(Maybe<T> other)
		{
			return this == other;
		}

		#endregion

		#region IFormattable Implementation
		
		/// <summary>
		/// Formats the value of the current instance using the specified format.
		/// </summary>
		/// <remarks>
		/// <para>If the instance is empty or the inner value does not support <see cref="IFormattable"/> then returns the same result as <see cref="ToString()"/>, otherwise returns the formatted string of the inner value.</para>
		/// </remarks>
		/// <param name="format">The format string to use.</param>
		/// <param name="formatProvider">A format provider instance to use.</param>
		/// <returns>A string containing the formatted value.</returns>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (HasValue && IsFormattable())
				return ((IFormattable)_Value).ToString(format, formatProvider);
			else
				return ToString();
		}

		#endregion

		#region IComparable Implementation

		/// <summary>
		/// Compares this instance to the value specified.
		/// </summary>
		/// <param name="obj">The value to compare to.</param>
		/// <exception cref="ArgumentException">Thrown if obj is not null, or of type Maybe{T} or {T}.</exception>
		/// <returns></returns>
		public int CompareTo(object obj)
		{
			if (obj == null) return 1;
			if (obj is Maybe<T>) return CompareTo((Maybe<T>)obj);
			if (obj is T) return CompareTo((T)obj);

			throw new ArgumentException(String.Format(System.Globalization.CultureInfo.CurrentCulture, "Argument must be a Maybe<{0}>", typeof(T).FullName), nameof(obj));
		}

		#endregion

		#region IComparable<T> Implementation

		/// <summary>
		/// Compares this instance to a value of {T}.
		/// </summary>
		/// <param name="other">The value to compare to.</param>
		/// <remarks>
		/// <para>If this instance is empty, then it is considered 'less than' <paramref name="other"/>. Otherwise Comparer{T}.Default is used to compare this instance's inner value to <paramref name="other"/>.</para>
		/// </remarks>
		/// <returns>-1 if this is less than other, 0 if this is equal to other, or 1 if this is greater than other.</returns>
		public int CompareTo(T other)
		{
			if (IsEmpty) return -1;
			return Comparer<T>.Default.Compare(_Value, other);
		}

		#endregion

		#region IComparable<Maybe<T>> Implementation

		/// <summary>
		/// Compares this instance to another instance of Maybe{T}.
		/// </summary>
		/// <param name="other">Another <see cref="Maybe{T}"/> to compare this instance with.</param>
		/// <returns>-1 if this is less than other, 0 if this is equal to other, or 1 if this is greater than other.</returns>
		public int CompareTo(Maybe<T> other)
		{
			if (IsEmpty && other.IsEmpty) return 0;
			if (IsEmpty && other.HasValue) return -1;
			if (other.IsEmpty) return 1;	

			return Comparer<T>.Default.Compare(_Value, other.Value);
		}

		#endregion

		internal static Exception CreateEmptyException()
		{
			return new InvalidOperationException(String.Format(System.Globalization.CultureInfo.CurrentCulture, "Value requested from empty Maybe<{0}>.", typeof(T).FullName));
		}

		private static bool IsFormattable()
		{
			if (TIsFormattable == null)
			{
				TIsFormattable = TypeImplementsInterface<T>(typeof(IFormattable));
			}

			return TIsFormattable.Value;
		}

		private static bool TypeImplementsInterface<T1>(Type interfaceType)
		{
#if SUPPORTS_GETTYPEINFO
			return interfaceType.GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo());
#else
			return interfaceType.IsAssignableFrom(typeof(T));
#endif
		}

		private static bool IsNull(T value)
		{
			if (TIsNullable == null)
			{
#if SUPPORTS_GETTYPEINFO
				var typeInfo = typeof(T).GetTypeInfo();
				TIsNullable = !typeInfo.IsValueType ||
					(typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>));
#else
				var type = typeof(T);
				TIsNullable = !type.IsValueType ||
					(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
#endif
			}

			return TIsNullable.Value && Object.ReferenceEquals(value, null);
		}

		#region Operators

		#region (In)Equality Operators

		/// <summary>
		/// Performs equality checking on Maybe{T} instances.
		/// </summary>
		/// <param name="m1">A Maybe{T} instance.</param>
		/// <param name="m2">A Maybe{T} instance.</param>
		/// <remarks>Returns true if both instances are 'empty'. Returns false is one instance is empty and the other is not. Otherwise returns the result of EqualityComparer{T}.Default.Equals for the inner values of the maybe instances.</remarks>
		/// <returns>True if the instances are equal, otherwise false.</returns>
		public static bool operator ==(Maybe<T> m1, Maybe<T> m2)
		{
			if (m1.IsEmpty && m2.IsEmpty) return true;
			if (m1.IsEmpty != m2.IsEmpty) return false;

			return EqualityComparer<T>.Default.Equals(m1.Value, m2.Value);
		}

		/// <summary>
		/// Performs inequality checking on Maybe{T} instances.
		/// </summary>
		/// <param name="m1">A Maybe{T} instance.</param>
		/// <param name="m2">A Maybe{T} instance.</param>
		/// <remarks>Inverts the result of an == comparison.</remarks>
		/// <returns>True if the instances are not equal, otherwise false.</returns>
		public static bool operator !=(Maybe<T> m1, Maybe<T> m2)
		{
			return !(m1 == m2);
		}

		#endregion

		#region Greater/Less Than (Or Equal To) Maybe<T> Operators

		/// <summary>
		/// Returns true if <paramref name="m1"/> is greater than <paramref name="m2"/>.
		/// </summary>
		/// <param name="m1">A Maybe{T} instance.</param>
		/// <param name="m2">A Maybe{T} instance.</param>
		/// <remarks>An empty instance is considered the 'lowest possible value' and will never be greater than anything else.</remarks>
		/// <returns>True if <paramref name="m1"/> is greater/larger than <paramref name="m2"/>.</returns>
		public static bool operator >(Maybe<T> m1, Maybe<T> m2)
		{
			if (m1.IsEmpty && m2.HasValue) return false;

			return m1.CompareTo(m2) > 0;
		}

		/// <summary>
		/// Returns true if <paramref name="m1"/> is less than <paramref name="m2"/>.
		/// </summary>
		/// <param name="m1">A Maybe{T} instance.</param>
		/// <param name="m2">A Maybe{T} instance.</param>
		/// <remarks>An empty instance is considered the 'lowest possible value' and will always be lower than anything else.</remarks>
		/// <returns>True if <paramref name="m1"/> is lower/lesser than <paramref name="m2"/>.</returns>
		public static bool operator <(Maybe<T> m1, Maybe<T> m2)
		{
			if (m1.HasValue && m2.IsEmpty) return true;

			return m1.CompareTo(m2) < 0;
		}

		/// <summary>
		/// Returns true if <paramref name="m1"/> is greater than <paramref name="m2"/>.
		/// </summary>
		/// <param name="m1">A Maybe{T} instance.</param>
		/// <param name="m2">A Maybe{T} instance.</param>
		/// <remarks>An empty instance is considered the 'lowest possible value' and will never be greater than anything else.</remarks>
		/// <returns>True if <paramref name="m1"/> is greater/larger than <paramref name="m2"/>.</returns>
		public static bool operator >=(Maybe<T> m1, Maybe<T> m2)
		{
			if (m1.IsEmpty && m2.HasValue) return false;

			return m1.CompareTo(m2) >= 0;
		}

		/// <summary>
		/// Returns true if <paramref name="m1"/> is less than <paramref name="m2"/>.
		/// </summary>
		/// <param name="m1">A Maybe{T} instance.</param>
		/// <param name="m2">A Maybe{T} instance.</param>
		/// <remarks>An empty instance is considered the 'lowest possible value' and will always be lower than anything else.</remarks>
		/// <returns>True if <paramref name="m1"/> is lower/lesser than <paramref name="m2"/>.</returns>
		public static bool operator <=(Maybe<T> m1, Maybe<T> m2)
		{
			if (m1.HasValue && m2.IsEmpty) return true;

			return m1.CompareTo(m2) <= 0;
		}

		#endregion

		#region Greater/Less Than (Or Equal To) T Operators

		/// <summary>
		/// Returns true if <paramref name="m1"/> is less than <paramref name="m2"/>.
		/// </summary>
		/// <param name="m1">A Maybe{T} instance.</param>
		/// <param name="m2">A T instance.</param>
		/// <remarks>An empty instance is considered the 'lowest possible value' and will always be lower than anything else.</remarks>
		/// <returns>True if <paramref name="m1"/> is lower/lesser than <paramref name="m2"/>.</returns>
		public static bool operator <(Maybe<T> m1, T m2)
		{
			return m1.CompareTo(m2) < 0;
		}

		/// <summary>
		/// Returns true if <paramref name="m1"/> is greater than <paramref name="m2"/>.
		/// </summary>
		/// <param name="m1">A Maybe{T} instance.</param>
		/// <param name="m2">A T instance.</param>
		/// <remarks>An empty instance is considered the 'lowest possible value' and will always be lower than anything else.</remarks>
		/// <returns>True if <paramref name="m1"/> is lower/lesser than <paramref name="m2"/>.</returns>
		public static bool operator >(Maybe<T> m1, T m2)
		{
			return m1.CompareTo(m2) > 0;
		}

		/// <summary>
		/// Returns true if <paramref name="m1"/> is less than <paramref name="m2"/>.
		/// </summary>
		/// <param name="m1">A Maybe{T} instance.</param>
		/// <param name="m2">A T instance.</param>
		/// <remarks>An empty instance is considered the 'lowest possible value' and will always be lower than anything else.</remarks>
		/// <returns>True if <paramref name="m1"/> is lower/lesser than <paramref name="m2"/>.</returns>
		public static bool operator <=(Maybe<T> m1, T m2)
		{
			return m1.CompareTo(m2) <= 0;
		}

		/// <summary>
		/// Returns true if <paramref name="m1"/> is greater than <paramref name="m2"/>.
		/// </summary>
		/// <param name="m1">A Maybe{T} instance.</param>
		/// <param name="m2">A T instance.</param>
		/// <remarks>An empty instance is considered the 'lowest possible value' and will always be lower than anything else.</remarks>
		/// <returns>True if <paramref name="m1"/> is lower/lesser than <paramref name="m2"/>.</returns>
		public static bool operator >=(Maybe<T> m1, T m2)
		{
			return m1.CompareTo(m2) >= 0;
		}

		#endregion

		#region Cast Operators

		/// <summary>
		/// Allows implicit conversion from {T} to a Maybe{T} instance.
		/// </summary>
		/// <param name="value">The {T} to cast to.</param>
		public static implicit operator Maybe<T>(T value)
		{
			if (IsNull(value)) return Maybe<T>.Nothing; 

			return new Maybe<T>(value);
		}

		/// <summary>
		/// Explicitly converts a Maybe{T} to it's {T} value if it is non-empty, otherwise throws an <see cref="InvalidOperationException"/>.
		/// </summary>
		/// <param name="maybeValue">The Maybe{T} to cast to.</param>
		/// <exception cref="System.InvalidOperationException">Thrown if <see cref="IsEmpty"/> is true.</exception>
		public static explicit operator T(Maybe<T> maybeValue)
		{
			return maybeValue.Value;
		}

		#endregion

		#endregion

	}
}