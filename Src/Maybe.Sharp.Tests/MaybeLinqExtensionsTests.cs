using MaybeSharp;
using MaybeSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Maybe.Sharp.Tests
{
	public class MaybeLinqExtensionsTests
	{

		[Fact]
		public void Maybe_Where_ConditionOnEmptyIsFalse()
		{
			var maybeName = Maybe<string>.Nothing;
			var result =	from n in maybeName
										where n.StartsWith("slippery", StringComparison.OrdinalIgnoreCase)
										select n;

			Assert.True(result.IsEmpty);
		}

		[Fact]
		public void Maybe_Where_ConditionOnNonEmptyIsEvaluated()
		{
			var maybeName = (Maybe<string>)"Slippery Jim DiGriz";
			var result = from n in maybeName
									 where n.StartsWith("slippery", StringComparison.OrdinalIgnoreCase)
									 select n;

			Assert.False(result.IsEmpty);
			Assert.Equal(maybeName.Value, result.Value);

			maybeName = (Maybe<string>)"Slippery Jim DiGriz";
			result =		from n in maybeName
									where n.StartsWith("Dr", StringComparison.OrdinalIgnoreCase)
									select n;

			Assert.True(result.IsEmpty);
		}

		[Fact]
		public void Maybe_Where_ThrowsOnNullPredicate()
		{
			var maybeName = (Maybe<string>)"Slippery Jim DiGriz";

			Assert.Throws<ArgumentNullException>("predicate", () => maybeName.Where(null));
		}

		[Fact]
		public void Maybe_Select_NonEmptySelectsValue()
		{
			var maybeName = (Maybe<string>)"Sippery Jim DiGriz";

			var result = from name in maybeName
									 select name;

			Assert.False(result.IsEmpty);
			Assert.Equal(maybeName.Value, result);
		}

		[Fact]
		public void Maybe_Select_EmptyReturnsEmptyMaybe()
		{
			var maybeName = Maybe<string>.Nothing;

			var result = from name in maybeName
									 select name;

			Assert.True(result.IsEmpty);
		}

		[Fact]
		public void Maybe_Select_ThrowsOnNullMaybePredicate()
		{
			var maybeName = Maybe<string>.Nothing;
			Func<string, Maybe<string>> predicate = null;
			Assert.Throws<ArgumentNullException>("predicate", () => maybeName.Select(predicate));
		}

		[Fact]
		public void Maybe_Select_ThrowsOnNullTPredicate()
		{
			var maybeName = Maybe<string>.Nothing;
			Func<string, string> predicate = null;
			Assert.Throws<ArgumentNullException>("predicate", () => maybeName.Select(predicate));
		}

		[Fact]
		public void IEnumerableOfMaybe_Select_ConvertsToEnumerableOfMaybeTResult()
		{
			var values = new Maybe<int>[] { 1, 2, Maybe<int>.Nothing, 4, Maybe<int>.Nothing, Maybe<int>.Nothing, 7 };
			List<Maybe<int>> results = System.Linq.Enumerable.ToList(values.Select<int, int>((mi) => mi + 2));
			
			Assert.NotEqual(null, results);
			Assert.Equal(7, results.Count);
			Assert.Equal(3, results[0].Value);
			Assert.Equal(4, results[1].Value);
			Assert.Equal(true, results[2].IsEmpty);
			Assert.Equal(6, results[3].Value);
			Assert.Equal(true, results[4].IsEmpty);
			Assert.Equal(true, results[5].IsEmpty);
			Assert.Equal(9, results[6].Value);
		}

		[Fact]
		public void IEnumerableOfMaybe_Select_ReturnsEmptySetWhenSourceNull()
		{
			Maybe<int>[] values = null;
			List<Maybe<int>> results = System.Linq.Enumerable.ToList(values.Select<int, int>((mi) => mi + 2));

			Assert.NotEqual(null, results);
			Assert.Equal(0, results.Count);
		}

		[Fact]
		public void IEnumerableOfMaybe_Select_ThrowsWhenPredicateNull()
		{
			var values = new Maybe<int>[] { 1, 2, Maybe<int>.Nothing, 4, Maybe<int>.Nothing, Maybe<int>.Nothing, 7 };

			Assert.Throws<ArgumentNullException>
			(
				"predicate",
				() => { List<Maybe<int>> results = System.Linq.Enumerable.ToList(values.Select<int, int>(null)); }
			);
			
		}


		[Fact]
		public void Maybe_SelectMany_WithConcatEmptyIsEmpty()
		{
			var maybeFirstName = (Maybe<string>)"Jim";
			var maybeSurname = Maybe<string>.Nothing;

			var fullName = from firstName in maybeFirstName
										from surname in maybeSurname
										select firstName + " " + surname;

			Assert.True(fullName.IsEmpty);
		}

		[Fact]
		public void Maybe_SelectMany_WithConcatNonEmptiesProducesResult()
		{
			var maybeFirstName = (Maybe<string>)"Jim";
			var maybeSurname = (Maybe<string>)"DiGriz";

			var fullName = from firstName in maybeFirstName
										 from surname in maybeSurname
										 select firstName + " " + surname;

			Assert.Equal(maybeFirstName.Value + " " + maybeSurname.Value, fullName);
		}

		[Fact]
		public void Maybe_SelectMany_ThrowsWithNullConverterFunc()
		{
			var maybeFirstName = (Maybe<string>)"Jim";
			var maybeSurname = (Maybe<string>)"DiGriz";

			Func<string, Maybe<string>> converter = null;
			Func<string, string, string> combiner = (s1, s2) => s1 + s2;

			Assert.Throws<ArgumentNullException>("converter", () => maybeFirstName.SelectMany(converter, combiner));
		}


		[Fact]
		public void Maybe_SelectMany_ThrowsWithNullCombinerFunc()
		{
			var maybeFirstName = (Maybe<string>)"Jim";
			var maybeSurname = (Maybe<string>)"DiGriz";

			Func<string, Maybe<string>> converter = (s1) => s1;
			Func<string, string, string> combiner = null;

			Assert.Throws<ArgumentNullException>("combiner", () => maybeFirstName.SelectMany(converter, combiner));
		}


		[Fact]
		public void Maybe_SelectNonEmpty_SkipsEmptyResults()
		{
			var items = new List<Maybe<int>>();
			for (int cnt = 0; cnt < 100; cnt++)
			{
				if (cnt % 2 == 0)
					items.Add(new Maybe<int>(cnt));
				else
					items.Add(Maybe<int>.Nothing);
			}

			var results = items.SelectNonempty<int, int>((m) => m % 5 == 0 ? Maybe<int>.Nothing : m);

			Assert.Equal(40, System.Linq.Enumerable.Count(results));
			foreach (var r in results)
			{
				Assert.False(r.IsEmpty);
				Assert.False(r.Value % 2 != 0);
				Assert.False(r.Value % 5 == 0);
			}
		}

		[Fact]
		public void Maybe_SelectNonEmpty_ReturnsEmptySetWhenSourceNull()
		{
			List<Maybe<int>> items = null;
			var results = items.SelectNonempty<int, int>((m) => m % 5 == 0 ? Maybe<int>.Nothing : m);

			Assert.False(System.Linq.Enumerable.Any(results));
		}

		[Fact]
		public void Maybe_SelectNonEmpty_ThrowsWhenPredicateNull()
		{
			var items = new List<Maybe<int>>();
			for (int cnt = 0; cnt < 100; cnt++)
			{
				if (cnt % 2 == 0)
					items.Add(new Maybe<int>(cnt));
				else
					items.Add(Maybe<int>.Nothing);
			}

			Assert.Throws<ArgumentNullException>(
				"predicate",
				() =>
				{
					var results = items.SelectNonempty<int, int>(null);
				}
			);

		}

	}
}