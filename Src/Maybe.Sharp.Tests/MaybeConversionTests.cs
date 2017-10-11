using System;
using Xunit;
using MaybeSharp;
using MaybeSharp.Extensions;

namespace Maybe.Sharp.Tests
{
	public class MaybeConversionTests
	{

		[Fact]
		public void Maybe_ImplicitCastFrom_NullToMaybeIsEmpty()
		{
			Maybe<int?> a = null;
			Assert.True(a.IsEmpty);
			Assert.False(a.HasValue);
		}

		[Fact]
		public void Maybe_ImplicitCastFrom_DefaultTToMaybeWhenDefaultTIsNotNullIsNonEmpty()
		{
			Maybe<int> a = default(int);
			Assert.False(a.IsEmpty);
			Assert.True(a.HasValue);
			Assert.Equal(default(int), a.Value);
		}

		[Fact]
		public void Maybe_ImplicitCastFrom_NonNullValueToMaybeT()
		{
			Maybe<int> a = 5;
			Assert.False(a.IsEmpty);
			Assert.True(a.HasValue);
			Assert.Equal(5, a.Value);
		}


		[Fact]
		public void Maybe_ExplicitCastTo_MaybeToTReturnsValue()
		{
			var a = new Maybe<int>(5);
			int b = (int)a;

			Assert.Equal<int>(5, b);
		}

		[Fact]
		public void Maybe_ExplicitCastTo_EmptyMaybeToTThrows()
		{
			var a = Maybe<int>.Nothing;

			Assert.Throws<InvalidOperationException>(() => (int)a.Value);
		}


		[Fact]
		public void Maybe_As_WithValueReturnsCastValue()
		{
			Maybe<Customer1> a = new Customer1();
			var b = a.As<CustomerBase>();
			Assert.Equal<CustomerBase>(a.Value, b.Value);
		}

		[Fact]
		public void Maybe_As_WhenEmptyReturnsNothing()
		{
			Maybe<Customer1> a = (Customer1)null;
			var b = a.As<CustomerBase>();
			Assert.True(b.IsEmpty);
		}


		[Fact]
		public void Maybe_ToNullable_ReturnsNullNullableWhenEmpty()
		{
			var maybe = Maybe<int>.Nothing;
			Assert.Null(maybe.ToNullable());
		}

		[Fact]
		public void Maybe_ToNullable_ReturnsNonNullNullableWhenSomething()
		{
			var maybe = (Maybe<int>)5;
			Assert.Equal((int?)5, maybe.ToNullable());
		}

	}

	public class CustomerBase { }
	public class Customer1 : CustomerBase { }
	public class Customer2 : CustomerBase { }

}