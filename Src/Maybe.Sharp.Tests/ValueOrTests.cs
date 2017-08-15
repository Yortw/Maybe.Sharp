using MaybeSharp;
using MaybeSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Maybe.Sharp.Tests
{
	public class ValueOrTests
	{

		[Fact]
		public void Maybe_ValueOr_ReturnsMaybeValueWhenNotEmpty()
		{
			var m = new Maybe<int>(5);
			Assert.Equal(5, m.ValueOr(2));
		}

		[Fact]
		public void Maybe_ValueOr_ReturnsDefaultWhenEmpty()
		{
			var m = Maybe<int>.Nothing;
			Assert.Equal(2, m.ValueOr(2));
		}

		[Fact]
		public void Maybe_ValueOrFunc_ReturnsMaybeValueWhenNotEmpty()
		{
			var m = new Maybe<int>(5);
			Assert.Equal(5, m.ValueOr(() => 2));
		}

		[Fact]
		public void Maybe_ValueOrFunc_ReturnsDefaultWhenEmpty()
		{
			var m = Maybe<int>.Nothing;
			Assert.Equal(2, m.ValueOr(() => 2));
		}

		[Fact]
		public void Maybe_ValueOrFunc_ReturnsDefaultTWhenFuncNullAndEmpty()
		{
			var m = Maybe<int>.Nothing;
			Assert.Equal(0, m.ValueOr((Func<int>)null));
		}

		[Fact]
		public void Maybe_ValueOrException_ReturnsValueWhenNotEmpty()
		{
			var m = new Maybe<int>(5);
			Assert.Equal(5, m.ValueOrException(new InvalidProgramException()));
		}

		[Fact]
		public void Maybe_ValueOrException_ThrowsExceptionWhenEmpty()
		{
			var m = Maybe<int>.Nothing;
			Assert.Throws<InvalidProgramException>(() => m.ValueOrException(new InvalidProgramException()));
		}

		[Fact]
		public void Maybe_ValueOrException_ThrowsInvalidOperationExceptionWhenEmptyAndExceptionIsNull()
		{
			var m = Maybe<int>.Nothing;
			Assert.Throws<InvalidOperationException>(() => m.ValueOrException((Exception)null));
		}

		[Fact]
		public void Maybe_ValueOrExceptionFunc_ReturnsValueWhenNotEmpty()
		{
			var m = new Maybe<int>(5);
			Assert.Equal(5, m.ValueOrException(() => new InvalidProgramException()));
		}

		[Fact]
		public void Maybe_ValueOrExceptionFunc_ThrowsWhenEmpty()
		{
			var m = Maybe<int>.Nothing;
			Assert.Throws<InvalidProgramException>(() => m.ValueOrException(() => new InvalidProgramException()));
		}

		[Fact]
		public void Maybe_ValueOrExceptionFunc_ThrowsInvalidOperationExceptionWithNullFuncWhenEmpty()
		{
			var m = Maybe<int>.Nothing;
			Assert.Throws<InvalidOperationException>(() => m.ValueOrException((Func<Exception>)null));
		}

	}
}