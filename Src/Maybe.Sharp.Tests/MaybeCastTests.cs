using MaybeSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Maybe.Sharp.Tests
{
	public class MaybeCastTests
	{

		[Fact]
		public void Maybe_Cast_CastsWhenSomething()
		{
			Maybe<object> m = 6;
			var x = m.Cast<int>();
			Assert.False(x.IsEmpty);
			Assert.Equal((int)6, x.Value);
		}

		[Fact]
		public void Maybe_Cast_ReturnNothingWhenNothing()
		{
			Maybe<int> m = Maybe<int>.Nothing;
			var x = m.Cast<decimal>();
			Assert.True(x.IsEmpty);
			Assert.Equal(Maybe<decimal>.Nothing, x);
		}


		[Fact]
		public void Maybe_Cast_ReturnNothingOnInvalidCast()
		{
			Maybe<string> m = "test";
			var x = m.Cast<decimal>();
			Assert.True(x.IsEmpty);
			Assert.Equal(Maybe<decimal>.Nothing, x);
		}

	}
}