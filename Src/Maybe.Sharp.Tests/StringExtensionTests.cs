using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MaybeSharp.Extensions;

namespace Maybe.Sharp.Tests
{
	public class StringExtensionTests
	{

		[Fact]
		public void String_TryParseBoolean_ParsesValidValue()
		{
			var s = "True";
			var m = s.TryParseBoolean();
			Assert.False(m.IsEmpty);
			Assert.Equal(true, m.Value);

			s = "False";
			m = s.TryParseBoolean();
			Assert.False(m.IsEmpty);
			Assert.Equal(false, m.Value);
		}

		[Fact]
		public void String_TryParseBoolean_ReturnsEmptyForNull()
		{
			string s = null;
			var m = s.TryParseBoolean();
			Assert.True(m.IsEmpty);
		}

		[Fact]
		public void String_TryParseBoolean_ReturnsEmptyForInvalidValue()
		{
			string s = "Hey there!";
			var m = s.TryParseBoolean();
			Assert.True(m.IsEmpty);
		}


		[Fact]
		public void String_TryParseByte_ParsesValidValue()
		{
			var s = "8";
			var m = s.TryParseByte();
			Assert.False(m.IsEmpty);
			Assert.Equal(8, m.Value);
		}

		[Fact]
		public void String_TryParseByte_ReturnsEmptyForNull()
		{
			string s = null;
			var m = s.TryParseByte();
			Assert.True(m.IsEmpty);
		}

		[Fact]
		public void String_TryParseByte_ReturnsEmptyForOutOfRange()
		{
			string s = "-1";
			var m = s.TryParseByte();
			Assert.True(m.IsEmpty);

			s = "500";
			m = s.TryParseByte();
			Assert.True(m.IsEmpty);
		}


		[Fact]
		public void String_TryParseChar_ParsesValidValue()
		{
			var s = "C";
			var m = s.TryParseChar();
			Assert.False(m.IsEmpty);
			Assert.Equal('C', m.Value);
		}

		[Fact]
		public void String_TryParseChar_ReturnsEmptyForNull()
		{
			string s = null;
			var m = s.TryParseChar();
			Assert.True(m.IsEmpty);
		}

		[Fact]
		public void String_TryParseChar_ReturnsEmptyForInvalidValue()
		{
			string s = "Hey there!";
			var m = s.TryParseChar();
			Assert.True(m.IsEmpty);
		}


		[Fact]
		public void String_TryParseDateTime_ParsesValidValue()
		{
			var s = "2017-08-14T02:17:22.4081245Z";
			var m = s.TryParseDateTime();
			Assert.False(m.IsEmpty);
			Assert.Equal(DateTime.Parse(s), m.Value);
		}

		[Fact]
		public void String_TryParseDateTime_ReturnsEmptyForNull()
		{
			string s = null;
			var m = s.TryParseDateTime();
			Assert.True(m.IsEmpty);
		}

		[Fact]
		public void String_TryParseDateTime_ReturnsEmptyForInvalidValue()
		{
			string s = "Hey there!";
			var m = s.TryParseDateTime();
			Assert.True(m.IsEmpty);
		}


		[Fact]
		public void String_TryParseDateTimeOffset_ParsesValidValue()
		{
			var s = "2017-08-14T02:19:28.9138274+00:00";
			var m = s.TryParseDateTimeOffset();
			Assert.False(m.IsEmpty);
			Assert.Equal(DateTime.Parse(s), m.Value);
		}

		[Fact]
		public void String_TryParseDateTimeOffset_ReturnsEmptyForNull()
		{
			string s = null;
			var m = s.TryParseDateTimeOffset();
			Assert.True(m.IsEmpty);
		}

		[Fact]
		public void String_TryParseDateTimeOffset_ReturnsEmptyForInvalidValue()
		{
			string s = "Hey there!";
			var m = s.TryParseDateTimeOffset();
			Assert.True(m.IsEmpty);
		}


		[Fact]
		public void String_TryParseDecimal_ParsesValidValue()
		{
			var s = "10.5234";
			var m = s.TryParseDecimal();
			Assert.False(m.IsEmpty);
			Assert.Equal(10.5234M, m.Value);
		}

		[Fact]
		public void String_TryParseDecimal_ReturnsEmptyForNull()
		{
			string s = null;
			var m = s.TryParseDecimal();
			Assert.True(m.IsEmpty);
		}

		[Fact]
		public void String_TryParseDecimal_ReturnsEmptyForInvalidValue()
		{
			string s = "Hey there!";
			var m = s.TryParseDecimal();
			Assert.True(m.IsEmpty);
		}


		[Fact]
		public void String_TryParseDouble_ParsesValidValue()
		{
			var s = "10.5234";
			var m = s.TryParseDouble();
			Assert.False(m.IsEmpty);
			Assert.Equal(10.5234D, m.Value);
		}

		[Fact]
		public void String_TryParseDouble_ReturnsEmptyForNull()
		{
			string s = null;
			var m = s.TryParseDouble();
			Assert.True(m.IsEmpty);
		}

		[Fact]
		public void String_TryParseDouble_ReturnsEmptyForInvalidValue()
		{
			string s = "Hey there!";
			var m = s.TryParseDouble();
			Assert.True(m.IsEmpty);
		}


		[Fact]
		public void String_TryParseInt_ParsesValidValue()
		{
			var s = "10";
			var m = s.TryParseInt();
			Assert.False(m.IsEmpty);
			Assert.Equal(10, m.Value);
		}

		[Fact]
		public void String_TryParseInt_ReturnsEmptyForNull()
		{
			string s = null;
			var m = s.TryParseInt();
			Assert.True(m.IsEmpty);
		}

		[Fact]
		public void String_TryParseInt_ReturnsEmptyForInvalidValue()
		{
			string s = "Hey there!";
			var m = s.TryParseInt();
			Assert.True(m.IsEmpty);
		}


		[Fact]
		public void String_TryParseInt16_ParsesValidValue()
		{
			var s = "10";
			var m = s.TryParseInt16();
			Assert.False(m.IsEmpty);
			Assert.Equal((Int16)10, m.Value);
		}

		[Fact]
		public void String_TryParseInt16_ReturnsEmptyForNull()
		{
			string s = null;
			var m = s.TryParseInt16();
			Assert.True(m.IsEmpty);
		}

		[Fact]
		public void String_TryParseInt16_ReturnsEmptyForInvalidValue()
		{
			string s = "Hey there!";
			var m = s.TryParseInt16();
			Assert.True(m.IsEmpty);
		}


		[Fact]
		public void String_TryParseInt64_ParsesValidValue()
		{
			var s = "10";
			var m = s.TryParseInt64();
			Assert.False(m.IsEmpty);
			Assert.Equal((Int64)10, m.Value);
		}

		[Fact]
		public void String_TryParseInt64_ReturnsEmptyForNull()
		{
			string s = null;
			var m = s.TryParseInt64();
			Assert.True(m.IsEmpty);
		}

		[Fact]
		public void String_TryParseInt64_ReturnsEmptyForInvalidValue()
		{
			string s = "Hey there!";
			var m = s.TryParseInt64();
			Assert.True(m.IsEmpty);
		}


		[Fact]
		public void String_TryParseSingle_ParsesValidValue()
		{
			var s = "10.123";
			var m = s.TryParseSingle();
			Assert.False(m.IsEmpty);
			Assert.Equal((float)10.123, m.Value);
		}

		[Fact]
		public void String_TryParseSingle_ReturnsEmptyForNull()
		{
			string s = null;
			var m = s.TryParseSingle();
			Assert.True(m.IsEmpty);
		}

		[Fact]
		public void String_TryParseSingle_ReturnsEmptyForInvalidValue()
		{
			string s = "Hey there!";
			var m = s.TryParseSingle();
			Assert.True(m.IsEmpty);
		}


	}
}