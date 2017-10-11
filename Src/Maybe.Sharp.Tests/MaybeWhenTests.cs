using MaybeSharp;
using MaybeSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Maybe.Sharp.Tests
{
	public class MaybeWhenTests
	{

		[Fact]
		public void Maybe_WhenSomething_IgnoresNullAction()
		{
			var a = new Maybe<int>(5);
			Assert.Throws<ArgumentNullException>("action", (Action)(() => a.WhenSomething(null)));
		}

		[Fact]
		public void Maybe_WhenSomething_CallsActionWhenSomething()
		{
			var a = new Maybe<int>(5);
			var result = 0;
			a.WhenSomething((i) => result = i + 2);
			Assert.Equal(7, result);
		}

		[Fact]
		public void Maybe_WhenSomething_IgnoresActionWhenNothing()
		{
			var a = Maybe<int>.Nothing;
			var wasCalled = false;
			a.WhenSomething((i) => wasCalled = true);
			Assert.False(wasCalled);
		}


		[Fact]
		public void Maybe_WhenNothing_ThrowsOnNullAction()
		{
			var a = new Maybe<int>(5);
			Assert.Throws<ArgumentNullException>("action", (Action)(() => { a.WhenNothing(null); }));
		}

		[Fact]
		public void Maybe_WhenNothing_CallsActionWhenNothing()
		{
			var a = Maybe<int>.Nothing;
			var wasCalled = false;
			a.WhenNothing(() => wasCalled = true);
			Assert.True(wasCalled);
		}

		[Fact]
		public void Maybe_WhenNothing_IgnoresFunctionWhenSomething()
		{
			var a = new Maybe<int>(5);
			var wasCalled = false;
			a.WhenNothing(() => wasCalled = true);
			Assert.False(wasCalled);
		}


	}
}