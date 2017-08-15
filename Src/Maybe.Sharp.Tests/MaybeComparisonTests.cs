using System;
using Xunit;
using MaybeSharp;

namespace Maybe.Sharp.Tests
{
	public class MaybeComparisonTests
	{

		[Fact]
		public void Maybe_CompareToTyped_LowerValueReturnsMinusOne()
		{
			var a = new Maybe<int>(4);
			var b = new Maybe<int>(5);

			Assert.Equal(-1, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareToTyped_HigherValueReturnsOne()
		{
			var a = new Maybe<int>(6);
			var b = new Maybe<int>(5);

			Assert.Equal(1, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareToTyped_EqualValuesReturn0()
		{
			var a = new Maybe<int>(5);
			var b = new Maybe<int>(5);

			Assert.Equal(0, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareToTyped_NothingToNothingReturns0()
		{
			var a = new Maybe<int>();
			var b = new Maybe<int>();

			Assert.Equal(0, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareToTyped_NothingIsLessThanValue()
		{
			var a = new Maybe<int>();
			var b = new Maybe<int>(0);

			Assert.Equal(-1, a.CompareTo(b));
			Assert.Equal(1, b.CompareTo(a));
		}


		[Fact]
		public void Maybe_CompareTo_OtherMaybe_LowerValueReturnsMinusOne()
		{
			var a = new Maybe<int>(4);
			object b = new Maybe<int>(5);

			Assert.Equal(-1, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareTo_OtherMaybe_HigherValueReturnsOne()
		{
			var a = new Maybe<int>(6);
			object b = new Maybe<int>(5);

			Assert.Equal(1, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareTo_OtherMaybe_EqualValuesReturn0()
		{
			var a = new Maybe<int>(5);
			object b = new Maybe<int>(5);

			Assert.Equal(0, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareTo_OtherMaybe_NothingToNothingReturns0()
		{
			var a = new Maybe<int>();
			object b = new Maybe<int>();

			Assert.Equal(0, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareTo_OtherMaybe_NothingIsLessThanValue()
		{
			var a = new Maybe<int>();
			object b = new Maybe<int>(0);

			Assert.Equal(-1, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareTo_OtherMaybe_NullIsLessThanValue()
		{
			var a = new Maybe<int>(5);
			object b = null;

			Assert.Equal(1, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareTo_OtherMaybe_NullIsLessThanNothing()
		{
			var a = Maybe<int>.Nothing;
			object b = null;

			Assert.Equal(1, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareToUntypedT_ThowsOnInvalidType()
		{
			var a = Maybe<int>.Nothing;
			object b = "Test";

			Assert.Throws<ArgumentException>("obj", () => a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareToUntypedT_LowerValueReturnsMinusOne()
		{
			var a = new Maybe<int>(4);
			object b = (int)5;

			Assert.Equal(-1, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareToUntypedT_HigherValueReturnsOne()
		{
			var a = new Maybe<int>(6);
			object b = (int)5;

			Assert.Equal(1, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareToUntypedT_EqualValuesReturn0()
		{
			var a = new Maybe<int>(5);
			object b = (int)5;

			Assert.Equal(0, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareToUntypedT_NothingIsLessThanValue()
		{
			var a = new Maybe<int>();
			object b = (int)0;

			Assert.Equal(-1, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareToUntypedT_NullIsLessThanValue()
		{
			var a = new Maybe<int>(5);
			object b = null;

			Assert.Equal(1, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareToUntypedT_NullIsLessThanNothing()
		{
			var a = Maybe<int>.Nothing;
			object b = null;

			Assert.Equal(1, a.CompareTo(b));
		}


		[Fact]
		public void Maybe_GreaterThanOperatorTyped_ReturnsTrueOnGreaterValue()
		{
			var a = new Maybe<int>(6);
			var b = new Maybe<int>(5);

			Assert.True(a > b);
		}

		[Fact]
		public void Maybe_GreaterThanOperatorTyped_ReturnsFalseOnEqualValues()
		{
			var a = new Maybe<int>(5);
			var b = new Maybe<int>(5);

			Assert.False(a > b);
		}

		[Fact]
		public void Maybe_GreaterThanOperatorTyped_ReturnsFalseOnLowerValue()
		{
			var a = new Maybe<int>(4);
			var b = new Maybe<int>(5);

			Assert.False(a > b);
		}

		[Fact]
		public void Maybe_GreaterThanOperatorTyped_LowestValueIsGreaterThanNothing()
		{
			var a = new Maybe<int>(Int32.MinValue);
			var b = Maybe<int>.Nothing;

			Assert.True(a > b);
		}

		[Fact]
		public void Maybe_GreaterThanOperatorTyped_NothingIsLessThanLowestValue()
		{
			var a = Maybe<int>.Nothing;
			var b = new Maybe<int>(Int32.MinValue);

			Assert.False(a > b);
		}

		[Fact]
		public void Maybe_GreaterThanOperatorT_ReturnsTrueOnGreaterValue()
		{
			var a = new Maybe<int>(6);
			var b = 5;

			Assert.True(a > b);
		}

		[Fact]
		public void Maybe_GreaterThanOperatorT_ReturnsFalseOnLowerValue()
		{
			var a = new Maybe<int>(5);
			var b = 6;

			Assert.False(a > b);
		}

		[Fact]
		public void Maybe_GreaterThanOperatorT_ReturnsFalseOnEqualValues()
		{
			var a = new Maybe<int>(6);
			var b = 6;

			Assert.False(a > b);
		}

		[Fact]
		public void Maybe_GreaterThanOrEqualToOperatorTyped_ReturnsTrueOnGreaterValue()
		{
			var a = new Maybe<int>(6);
			var b = new Maybe<int>(5);

			Assert.True(a >= b);
		}

		[Fact]
		public void Maybe_GreaterThanOrEqualToOperatorTyped_ReturnsTrueOnEqualValue()
		{
			var a = new Maybe<int>(5);
			var b = new Maybe<int>(5);

			Assert.True(a >= b);
		}

		[Fact]
		public void Maybe_GreaterThanOrEqualToOperatorTyped_NothingIsLessThanMinValue()
		{
			var a = new Maybe<int>();
			var b = new Maybe<int>(Int32.MinValue);

			Assert.False(a >= b);
		}

		[Fact]
		public void Maybe_GreaterThanOrEqualToOperatorTyped_MinValueIsGreaterThanNothing()
		{
			var a = new Maybe<int>(Int32.MinValue);
			var b = new Maybe<int>();

			Assert.True(a >= b);
		}

		[Fact]
		public void Maybe_GreaterThanOrEqualToOperatorT_ReturnsTrueOnGreaterValue()
		{
			var a = new Maybe<int>(6);
			var b = 5;

			Assert.True(a >= b);
		}

		[Fact]
		public void Maybe_GreaterThanOrEqualToOperatorT_ReturnsTrueOnEqualValues()
		{
			var a = new Maybe<int>(6);
			var b = 6;

			Assert.True(a >= b);
		}

		[Fact]
		public void Maybe_GreaterThanOrEqualToOperatorT_ReturnsFalseOnLowerValue()
		{
			var a = new Maybe<int>(5);
			var b = 6;

			Assert.False(a >= b);
		}



		[Fact]
		public void Maybe_LessThanOperatorTyped_ReturnsTrueOnLowerValue()
		{
			var a = new Maybe<int>(5);
			var b = new Maybe<int>(6);

			Assert.True(a < b);
		}

		[Fact]
		public void Maybe_LessThanOperatorTyped_ReturnsFalseOnEqualValues()
		{
			var a = new Maybe<int>(5);
			var b = new Maybe<int>(5);

			Assert.False(a < b);
		}

		[Fact]
		public void Maybe_LessThanOperatorTyped_ReturnsFalseOnGreaterValue()
		{
			var a = new Maybe<int>(6);
			var b = new Maybe<int>(5);

			Assert.False(a < b);
		}

		[Fact]
		public void Maybe_LessThanOperatorTyped_NothingIsLessThanLowestValue()
		{
			var a = Maybe<int>.Nothing;
			var b = new Maybe<int>(Int32.MinValue);

			Assert.True(a < b);
		}

		[Fact]
		public void Maybe_LessThanOperatorTyped_LowestValueIsGreaterThanNothing()
		{
			var a = new Maybe<int>(Int32.MinValue);
			var b = Maybe<int>.Nothing;

			Assert.True(a < b);
		}

		[Fact]
		public void Maybe_LessThanOperatorT_ReturnsTrueOnLowerValue()
		{
			var a = new Maybe<int>(5);
			var b = 6;

			Assert.True(a < b);
		}

		[Fact]
		public void Maybe_LessThanOperatorT_ReturnsFalseOnHigherValue()
		{
			var a = new Maybe<int>(6);
			var b = 5;

			Assert.False(a < b);
		}

		[Fact]
		public void Maybe_LessThanOperatorT_ReturnsFalseOnEqualValues()
		{
			var a = new Maybe<int>(6);
			var b = 6;

			Assert.False(a < b);
		}

		[Fact]
		public void Maybe_LessThanOrEqualToOperatorTyped_ReturnsTrueOnLowerValue()
		{
			var a = new Maybe<int>(5);
			var b = new Maybe<int>(6);

			Assert.True(a <= b);
		}

		[Fact]
		public void Maybe_LessThanOrEqualToOperatorTyped_ReturnsTrueOnEqualValue()
		{
			var a = new Maybe<int>(5);
			var b = new Maybe<int>(5);

			Assert.True(a <= b);
		}

		[Fact]
		public void Maybe_LessThanOrEqualToOperatorTyped_NothingIsLessThanLowestValue()
		{
			var a = new Maybe<int>();
			var b = new Maybe<int>(Int32.MinValue);

			Assert.True(a <= b);
		}

		[Fact]
		public void Maybe_LessThanOrEqualToOperatorTyped_LowestValueIsGreaterThanNothing()
		{
			var a = new Maybe<int>(Int32.MinValue);
			var b = new Maybe<int>();

			Assert.True(a <= b);
		}

		[Fact]
		public void Maybe_LessThanOrEqualToOperatorT_ReturnsTrueOnLowerValue()
		{
			var a = new Maybe<int>(5);
			var b = 6;

			Assert.True(a <= b);
		}

		[Fact]
		public void Maybe_LessThanOrEqualToOperatorT_ReturnsTrueOnEqualValues()
		{
			var a = new Maybe<int>(6);
			var b = 6;

			Assert.True(a <= b);
		}

		[Fact]
		public void Maybe_LessThanOrEqualToOperatorT_ReturnsFalseOnGreaterValue()
		{
			var a = new Maybe<int>(6);
			var b = 5;

			Assert.False(a <= b);
		}


		[Fact]
		public void Maybe_CompareToT_LowerValueReturnsMinusOne()
		{
			var a = new Maybe<int>(4);
			var b = 5;

			Assert.Equal(-1, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareToT_HigherValueReturnsOne()
		{
			var a = new Maybe<int>(6);
			var b = 5;

			Assert.Equal(1, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareToT_EqualValuesReturn0()
		{
			var a = new Maybe<int>(5);
			var b = 5;

			Assert.Equal(0, a.CompareTo(b));
		}

		[Fact]
		public void Maybe_CompareToT_NothingIsLessThanValue()
		{
			var a = new Maybe<int>();
			var b = 0;

			Assert.Equal(-1, a.CompareTo(b));
		}


		[Fact]
		public void Maybe_NotEqualToOperator_ReturnsTrueWhenUnequal()
		{
			var a = new Maybe<int>(4);
			var b = new Maybe<int>(5);

			Assert.True(a != b);
		}

		[Fact]
		public void Maybe_NotEqualToOperator_ReturnsFalseWhenEqual()
		{
			var a = new Maybe<int>(5);
			var b = new Maybe<int>(5);

			Assert.False(a != b);
		}

		[Fact]
		public void Maybe_NotEqualToOperator_ReturnsFalseWhenNothingAndNothing()
		{
			var a = new Maybe<int>();
			var b = new Maybe<int>();

			Assert.False(a != b);
		}

		[Fact]
		public void Maybe_NotEqualToOperator_ReturnsTrueWhenNothingAndSomething()
		{
			var a = new Maybe<int>();
			var b = new Maybe<int>(4);

			Assert.True(a != b);
		}

	}
}