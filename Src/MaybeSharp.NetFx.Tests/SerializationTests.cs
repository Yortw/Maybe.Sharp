using System;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MaybeSharp;
using System.Linq;
using MaybeSharp.Extensions;
using Xunit;

namespace MaybeSharp.NetFx.Tests
{
	public class SerializationTests
	{

		[Fact]
		public void Maybe_Serialize_Binary_NonEmpty()
		{
			var value = new Maybe<int?>(5);

			var formatter = new BinaryFormatter();
			using (var ms = new System.IO.MemoryStream())
			{
				formatter.Serialize(ms, value);
				ms.Seek(0, System.IO.SeekOrigin.Begin);

				var deserialisedValue = (Maybe<int?>)formatter.Deserialize(ms);
				Assert.Equal(value, deserialisedValue);
				Assert.Equal(value.Value, deserialisedValue.Value);
			}
		}

		[Fact]
		public void Maybe_Serialize_Binary_Empty()
		{
			var value = Maybe<int?>.Nothing;

			var formatter = new BinaryFormatter();
			using (var ms = new System.IO.MemoryStream())
			{
				formatter.Serialize(ms, value);
				ms.Seek(0, System.IO.SeekOrigin.Begin);

				var deserialisedValue = (Maybe<int?>)formatter.Deserialize(ms);
				Assert.Equal(value, deserialisedValue);
				Assert.True(deserialisedValue.IsEmpty);
			}
		}

	}
}
