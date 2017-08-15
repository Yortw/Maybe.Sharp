using System;
using System.Collections.Generic;
using System.Text;
using MaybeSharp;
using Xunit;
using MaybeSharp.Extensions;

namespace Maybe.Sharp.Tests
{

  public class MaybeCoalescingExtensionsTests
  {

		[Fact]
		public void Maybe_Extension_Or_ReturnsSecondWhenFirstEmpty()
		{
			var a = Maybe<int>.Nothing;
			var b = new Maybe<int>(5);

			Assert.Equal(b, a.Or(b));
		}

		[Fact]
		public void Maybe_Extension_Or_ReturnsFirstWhenNotEmpty()
		{
			var a = new Maybe<int>(1);
			var b = new Maybe<int>(5);

			Assert.Equal(a, a.Or(b));
		}


		[Fact]
		public void Maybe_Extension_Coalesce_Params_ReturnsSourceWhenNotEmpty()
		{
			var a = new Maybe<int>(1);
			var b = new Maybe<int>(5);
			var c = new Maybe<int>(3);
			var d = new Maybe<int>(6);

			Assert.Equal(a, a.Coalesce(b, c, d));
		}

		[Fact]
		public void Maybe_Extension_Coalesce_Params_ReturnsFirstNonEmptyValue()
		{
			var a = Maybe<int>.Nothing;
			var b = Maybe<int>.Nothing;
			var c = new Maybe<int>(3);
			var d = new Maybe<int>(6);

			Assert.Equal(c, a.Coalesce(b, c, d));
		}


		[Fact]
		public void Maybe_ExtensionCoalesce_IEnumerable_ReturnsSourceWhenNotEmpty()
		{
			var a = new Maybe<int>(1);
			var b = new Maybe<int>(5);
			var c = new Maybe<int>(3);
			var d = new Maybe<int>(6);

			IEnumerable<Maybe<int>> enumerable = new Maybe<int>[] { b, c, d };
			Assert.Equal(a, a.Coalesce(enumerable));
		}

		[Fact]
		public void Maybe_Extension_Coalesce_IEnumerable_ReturnsFirstNonEmptyValue()
		{
			var a = Maybe<int>.Nothing;
			var b = Maybe<int>.Nothing;
			var c = new Maybe<int>(3);
			var d = new Maybe<int>(6);

			IEnumerable<Maybe<int>> enumerable = new Maybe<int>[] { b, c, d };
			Assert.Equal(c, a.Coalesce(enumerable));
		}

		[Fact]
		public void Maybe_Extension_Coalesce_IEnumerable_ReturnsEmptyWhenSourceAndValuesAreEmptyAndNull()
		{
			var a = Maybe<int>.Nothing;

			Assert.Equal(Maybe<int>.Nothing, a.Coalesce(null));
		}

		[Fact]
		public void Maybe_Extension_Coalesce_IEnumerable_ReturnsEmptyWhenAllValueEmpty()
		{
			var a = Maybe<int>.Nothing;
			var b = Maybe<int>.Nothing;
			var c = new Maybe<int>(3);
			var d = new Maybe<int>(6);

			IEnumerable<Maybe<int>> enumerable = new Maybe<int>[] { Maybe<int>.Nothing, Maybe<int>.Nothing, Maybe<int>.Nothing };
			Assert.Equal(Maybe<int>.Nothing, a.Coalesce(enumerable));
		}


		[Fact]
		public void Maybe_Extension_ValueOr_ReturnsValueWhenNotEmpty()
		{
			var a = new Maybe<int>(1);

			Assert.Equal(1, a.ValueOr(5));
		}

		[Fact]
		public void Maybe_Extension_ValueOr_ReturnsOrWhenEmpty()
		{
			var a = Maybe<int>.Nothing;

			Assert.Equal(5, a.ValueOr(5));
		}


		[Fact]
		public void Maybe_Extension_ValueOrFactory_ReturnsValueWhenNotEmpty()
		{
			var a = new Maybe<int>(1);

			Assert.Equal(1, a.ValueOr(() => 5));
		}

		[Fact]
		public void Maybe_Extension_ValueOrFactory_ReturnsOrWhenEmpty()
		{
			var a = Maybe<int>.Nothing;

			Assert.Equal(5, a.ValueOr(() => 5));
		}

		[Fact]
		public void Maybe_Extension_ValueOrFactory_ReturnsDefaultTWhenFunctionNull()
		{
			var a = Maybe<int>.Nothing;

			Assert.Equal(default(int), a.ValueOr(null));
		}


		[Fact]
		public void Maybe_Extension_ValueOrException_ReturnsValueWhenNotEmpty()
		{
			var a = new Maybe<int>(1);

			Assert.Equal(1, a.ValueOrException(new ArgumentException("Maybe was nothing", "value")));
		}

		[Fact]
		public void Maybe_Extension_ValueOrException_ThrowsWhenEmpty()
		{
			var a = Maybe<int>.Nothing;

			Assert.Throws<ArgumentException>("value", () => a.ValueOrException(new ArgumentException("Maybe was nothing", "value")));
		}


		[Fact]
		public void Maybe_Extension_ValueOrExceptionFactory_ReturnsValueWhenNotEmpty()
		{
			var a = new Maybe<int>(1);

			Assert.Equal(1, a.ValueOrException(new ArgumentException("Maybe was nothing", "value")));
		}

		[Fact]
		public void Maybe_Extension_ValueOrExceptionFactory_ThrowsWhenEmpty()
		{
			var a = Maybe<int>.Nothing;

			Assert.Throws<ArgumentException>("value", () => a.ValueOrException(() => new ArgumentException("Maybe was nothing", "value")));
		}

		[Fact]
		public void Maybe_Extension_ValueOrExceptionFactory_ThrowsStandardExceptionWhenFunctionNull()
		{
			var a = Maybe<int>.Nothing;

			Assert.Throws<System.InvalidOperationException>(() => a.ValueOrException((Func<Exception>)null));
		}

		[Fact]
		public void Maybe_Extension_ValueOrExceptionFactory_ThrowsStandardExceptionWhenFunctionReturnsNull()
		{
			var a = Maybe<int>.Nothing;

			Assert.Throws<System.InvalidOperationException>(() => a.ValueOrException(() => (Exception)null));
		}

	}
}