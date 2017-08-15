using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MaybeSharp.Extensions;

namespace Maybe.Sharp.Tests
{
	public class DictionaryExtensionTests
	{

		[Fact]
		public void Dictionary_TryGetValue_ForValidKeyReturnsValue()
		{
			var aliases = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			aliases.Add("Jim DiGriz", "The Stainless Steel Rat");
			aliases.Add("Rand Al Thor", "The Dragon Reborn");

			var result = aliases.TryGetValue("jim digriz");
			Assert.False(result.IsEmpty);
			Assert.Equal("The Stainless Steel Rat", result.Value);
		}

		[Fact]
		public void Dictionary_TryGetValue_ForInvalidKeyReturnsEmpty()
		{
			var aliases = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			aliases.Add("Jim DiGriz", "The Stainless Steel Rat");
			aliases.Add("Rand Al Thor", "The Dragon Reborn");

			var result = aliases.TryGetValue("Rodney d'Armand");
			Assert.True(result.IsEmpty);
		}

		[Fact]
		public void Dictionary_TryGetValue_ThrowsWhenDictionaryNull()
		{
			Dictionary<string, string> aliases = null;

			Assert.Throws<ArgumentNullException>
			(
				"source",
				() => { var result = aliases.TryGetValue("Rodney d'Armand"); }
			);
		}
	}
}