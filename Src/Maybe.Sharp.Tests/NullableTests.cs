using System;
using Xunit;
using MaybeSharp;
using MaybeSharp.Extensions;

namespace Maybe.Sharp.Tests
{
	public class NullableTests
	{
		[Fact]
		public void Nullable_ToMaybe_ReturnsNothingWhenNull()
		{
			int? a = null;
			var m = a.ToMaybe();
			Assert.True(m.IsEmpty);
			Assert.False(m.HasValue);
		}

		[Fact]
		public void Nullable_ToMaybe_ReturnsSomethingWhenNotNull()
		{
			int? a = 6;
			var m = a.ToMaybe();
			Assert.False(m.IsEmpty);
			Assert.Equal(6, m.Value);
		}

	}
}