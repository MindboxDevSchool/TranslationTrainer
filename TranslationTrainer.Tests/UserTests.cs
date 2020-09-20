using System;
using System.Linq;
using TranslationTrainer.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace TranslationTrainer.Tests
{
	[TestClass]
	public class UserTests
	{
		[TestInitialize]
		public void Initialize()
		{
			_fixture = new Fixture();
		}

		[TestMethod]
		public void MarkWordStudied_WhenUserHadNotThisWord_AddsWordToStudied()
		{
			var user = new User(Guid.NewGuid(), "nickname", new StudiedWord[0]);
			var word = _fixture.Create<Word>();

			user.MarkStudied(word, false);
			var addedWord = user.StudiedWords.First().Word;

			Assert.AreEqual(word.Original, addedWord.Original);
		}

		[TestMethod]
		public void MarkWordStudied_WhenUserHadThisWord_DoesNotChangeStudiedWordsList()
		{
			var user = _fixture.Create<User>();
			var userWords = user.StudiedWords;
			var existingWord = userWords.First().Word;

			user.MarkStudied(existingWord, false);

			CollectionAssert.AreEqual(userWords.ToArray(), user.StudiedWords.ToArray());
		}

		[TestMethod]
		public void MarkWordStudiedWithCorrectTranslation_AddsTimesLearnedToStudiedWord()
		{
			var user = _fixture.Create<User>();
			var userWords = user.StudiedWords;
			var existingWord = userWords.First();

			user.MarkStudied(existingWord.Word, true);
			var updatedWord = user.StudiedWords.First(word => word.Word.Original == existingWord.Word.Original);

			Assert.AreEqual(existingWord.TimesLearned + 1, updatedWord.TimesLearned);
		}

		private Fixture _fixture;
	}
}
