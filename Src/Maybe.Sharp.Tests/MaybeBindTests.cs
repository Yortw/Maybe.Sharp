using MaybeSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Maybe.Sharp.Tests
{
	public class MaybeBindTests
	{

		[Fact]
		public void Maybe_BindT_CallsFunctionWhenSomething()
		{
			Maybe<int> m = 6;
			var x = m.Bind((i) => i + 2);
			Assert.False(x.IsEmpty);
			Assert.Equal(8, x.Value);
		}

		[Fact]
		public void Maybe_BindT_IgnoresFunctionWhenNothing()
		{
			Maybe<int> m = Maybe<int>.Nothing;
			var wasCalled = false;
			var x = m.Bind((i) => { wasCalled = true; return i + 2; });
			Assert.True(x.IsEmpty);
			Assert.False(wasCalled);
		}

		[Fact]
		public void Maybe_BindT_ReturnsNothingOnNullFunction()
		{
			Maybe<int> m = 6;

			var x = m.Bind(null);
			Assert.True(x.IsEmpty);
		}



		[Fact]
		public void Maybe_BindMaybeT_CallsFunctionWhenSomething()
		{
			Maybe<int> m = 6;
			Maybe<decimal> x = m.Bind((i) => (Maybe<decimal>)(i + 2.5M));
			Assert.False(x.IsEmpty);
			Assert.Equal(8.5M, x.Value);
		}

		[Fact]
		public void Maybe_BindMaybeT_IgnoresFunctionWhenNothing()
		{
			Maybe<int> m = Maybe<int>.Nothing;
			var wasCalled = false;
			Maybe<decimal> x = m.Bind((i) => { wasCalled = true; return (Maybe<decimal>)(i + 2.5M); });
			Assert.True(x.IsEmpty);
			Assert.False(wasCalled);
		}

		[Fact]
		public void Maybe_BindMaybeT_ReturnsNothingOnNullFunction()
		{
			Maybe<int> m = 6;

			Maybe<decimal> x = m.Bind(((Func<int, Maybe<decimal>>)null));
			Assert.True(x.IsEmpty);
		}


	}
}