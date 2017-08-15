using System;
using Xunit;
using MaybeSharp;

namespace Maybe.Sharp.Tests
{
	public class MaybeTests
	{
		[Fact]
		public void Maybe_DefaultConstructor_ConstructsAsEmpty()
		{
			var a = new Maybe<int>();
			Assert.True(a.IsEmpty);
			Assert.False(a.HasValue);
		}

		[Fact]
		public void Maybe_Constructor_ConstructsWithValueIsNotEmpty()
		{
			var a = new Maybe<int>(5);
			Assert.False(a.IsEmpty);
			Assert.True(a.HasValue);
		}

		[Fact]
		public void Maybe_Constructor_ConstructsWithNonNullNullableTIsNotEmpty()
		{
			var a = new Maybe<int?>(5);
			Assert.False(a.IsEmpty);
			Assert.True(a.HasValue);
		}

		[Fact]
		public void Maybe_Constructor_ConstructsWithNullNullableTValueIsEmpty()
		{
			var a = new Maybe<int?>(null);
			Assert.True(a.IsEmpty);
			Assert.False(a.HasValue);
		}

		[Fact]
		public void Maybe_Constructor_ConstructsWithNullReferenceValueIsEmpty()
		{
			var a = new Maybe<object>(null);
			Assert.True(a.IsEmpty);
			Assert.False(a.HasValue);
		}

		[Fact]
		public void Maybe_Constructor_ConstructsWithDefaultForTIsNotEmptyWhenDefaultTIsNotNull()
		{
			var a = new Maybe<int>(default(int));
			Assert.False(a.IsEmpty);
			Assert.True(a.HasValue);
		}

		[Fact]
		public void Maybe_Constructor_ConstructsWithDefaultForTIsEmptyWhenDefaultTIsNull()
		{
			var a = new Maybe<int?>(default(int?));
			Assert.True(a.IsEmpty);
			Assert.False(a.HasValue);
		}


		[Fact]
		public void Maybe_Value_ReturnsValueWhenNotEmpty()
		{
			var a = new Maybe<int>(5);
			Assert.Equal<int>(5, a.Value);
		}

		[Fact]
		public void Maybe_Value_ThrowsWhenEmpty()
		{
			var a = new Maybe<int>();
			Assert.Throws<InvalidOperationException>(() => a.Value);
		}


		[Fact]
		public void Maybe_ToString_ReturnsFixedValueWhenEmpty()
		{
			var a = new Maybe<int>();
			Assert.Equal("<nothing>", a.ToString());
		}

		[Fact]
		public void Maybe_ToString_ReturnsInnerValueToStringWhenNotEmpty()
		{
			var a = new Maybe<int>(5);
			Assert.Equal("5", a.ToString());
		}

		[Fact]
		public void Maybe_IFormattable_ToString_ReturnsFixedValueWhenEmpty()
		{
			var a = new Maybe<int>();
			Assert.Equal("<nothing>", a.ToString(null, System.Globalization.CultureInfo.CurrentCulture));
		}

		[Fact]
		public void Maybe_IFormattable_ToString_ReturnsInnerValueToStringWhenNotEmpty()
		{
			var a = new Maybe<int>(5);
			Assert.Equal("5", a.ToString(null, System.Globalization.CultureInfo.CurrentCulture));
		}

		[Fact]
		public void Maybe_IFormattable_ToString_UsesFormatStringWhenTIsFormattable()
		{
			var a = new Maybe<int>(5);
			Assert.Equal("5.00", a.ToString("#0.00", System.Globalization.CultureInfo.InvariantCulture));
			Assert.Equal(5.ToString("#0.00", System.Globalization.CultureInfo.CurrentCulture), a.ToString("#0.00", System.Globalization.CultureInfo.CurrentCulture));
		}

	}
}