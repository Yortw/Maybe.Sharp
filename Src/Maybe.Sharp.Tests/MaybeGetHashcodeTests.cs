using System;
using Xunit;
using MaybeSharp;

namespace Maybe.Sharp.Tests
{
	public class MaybeGetHashCodeTests
	{
		[Fact]
		public void Maybe_GetHashCode_NothingInstancesHaveSameHashCode()
		{
			var a = new Maybe<int>();
			var b = new Maybe<int>();

			Assert.Equal<int>(a.GetHashCode(), b.GetHashCode());
		}

		[Fact]
		public void Maybe_GetHashCode_InstancesWithSameValueHaveSameHashCode()
		{
			var a = new Maybe<int>(5);
			var b = new Maybe<int>(5);

			Assert.Equal<int>(a.GetHashCode(), b.GetHashCode());
		}

		[Fact]
		public void Maybe_GetHashCode_InstancesWithDifferentValueHaveDifferentHashCode()
		{
			var a = new Maybe<int>(5);
			var b = new Maybe<int>(2);

			Assert.NotEqual<int>(a.GetHashCode(), b.GetHashCode());
		}

		[Fact]
		public void Maybe_GetHashCode_NothingInstanceHasDifferentHashCodeToValueInstance()
		{
			var a = new Maybe<int>();
			var b = new Maybe<int>(5);

			Assert.NotEqual<int>(a.GetHashCode(), b.GetHashCode());
		}

	}
}