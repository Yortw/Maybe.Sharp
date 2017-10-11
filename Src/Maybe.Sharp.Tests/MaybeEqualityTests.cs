using System;
using Xunit;
using MaybeSharp;

namespace Maybe.Sharp.Tests
{
	public class MaybeEqualityTests
	{
		[Fact]
		public void Maybe_EqualsOperator_InstancesWithSameInnerValueAreEqual()
		{
			var a = new Maybe<int>(5);
			var b = new Maybe<int>(5);

			Assert.True(a == b);
			Assert.True(b == a);
		}

		[Fact]
		public void Maybe_Equals_InstancesWithSameInnerValueAreEqual()
		{
			var a = new Maybe<int>(5);
			var b = new Maybe<int>(5);

			Assert.True(a.Equals(b));
			Assert.True(b.Equals(a));
		}

		[Fact]
		public void Maybe_EqualsOperator_InstancesWithDifferentInnerValueAreNotEqual()
		{
			var a = new Maybe<int>(5);
			var b = new Maybe<int>(2);

			Assert.False(a == b);
			Assert.False(b == a);
		}

		[Fact]
		public void Maybe_Equals_InstancesWithDifferentInnerValueAreNotEqual()
		{
			var a = new Maybe<int>(5);
			var b = new Maybe<int>(2);

			Assert.False(a.Equals(b));
			Assert.False(b.Equals(a));
		}

		[Fact]
		public void Maybe_EqualsOperator_NothingEqualsNothing()
		{
			var a = new Maybe<int>();
			Assert.True(a == Maybe<int>.Nothing);
		}

		[Fact]
		public void Maybe_Equals_NothingEqualsItself()
		{
			var a = new Maybe<int>();
			Assert.True(Maybe<int>.Nothing.Equals(a));
		}

		[Fact]
		public void Maybe_EqualsOperator_NothingDoesNotEqualSomething()
		{
			var a = new Maybe<int>(5);
			Assert.False(a == Maybe<int>.Nothing);
			Assert.False(Maybe<int>.Nothing == a);
		}

		[Fact]
		public void Maybe_Equals_NothingDoesNotEqualSomething()
		{
			var a = new Maybe<int>(5);
			Assert.False(Maybe<int>.Nothing.Equals(a));
			Assert.False(a.Equals(Maybe<int>.Nothing));
		}

		[Fact]
		public void Maybe_EqualsObj_InequalOnOtherType()
		{
			var a = new Maybe<int>(5);

			Assert.False(a.Equals(new object()));
		}

		[Fact]
		public void Maybe_EqualsObj_InequalOnSameTypeOtherValue()
		{
			var a = new Maybe<int>(5);
			object b = new Maybe<int>(2);

			Assert.False(a.Equals(b));
		}

		[Fact]
		public void Maybe_EqualsObj_InequalOnNull()
		{
			var a = new Maybe<int>(5);
			object b = null;

			Assert.False(a.Equals(b));
		}

		[Fact]
		public void Maybe_EqualsObj_EqualOnSameTypeSameValue()
		{
			var a = new Maybe<int>(5);
			object b = new Maybe<int>(5);
			Assert.True(a.Equals(b));
		}

	}
}