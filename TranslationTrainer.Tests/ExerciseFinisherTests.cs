using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using TranslationTrainer.Domain;

namespace TranslationTrainer.Tests
{
	[TestClass]
	public class ExerciseFinisherTests
	{
		[TestInitialize]
		public void Initialize()
		{
			_fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

			_words = _fixture.CreateMany<Word>(10);
			var wordsRepoMock = _fixture.Freeze<Mock<IWordsRepository>>();
			wordsRepoMock.Setup(mock => mock.LoadAll()).Returns(_words);

			_user = _fixture.Freeze<Mock<IUser>>();
			var userRepoMock = _fixture.Freeze<Mock<IUserRepository>>();
			userRepoMock.Setup(mock => mock.Load(It.IsAny<Guid>())).Returns(_user.Object);

			_fixture.Register(() => new TranslationTrainerSettings(10, 2));
			_finisher = _fixture.Create<ExerciseFinisher>();

			_exercise = _fixture.Freeze<Mock<IExercise>>();

			_exerciseRepoMock = _fixture.Freeze<Mock<IExerciseRepository>>();
			_exerciseRepoMock.Setup(mock => mock.GetExercise(It.IsAny<Guid>())).Returns(_exercise.Object);
		}

		[TestMethod]
		public void ApplyResultOfExercise_ResultContainsOnlyExercisedWords()
		{
			var status = SetupExercise();
			var wordsShouldBeApplied = status.WordsConsideredCorrect
				.Concat(status.WordsConsideredIncorrect)
				.Select(word => word.Original)
				.ToArray();

			var result = _finisher.FinishExercise(_exercise.Object.ExerciseId);
			var resultWords = result.ExercisedWordWithResults
				.Select(word => word.ActualWord.Original)
				.ToArray();

			CollectionAssert.AreEquivalent(wordsShouldBeApplied, resultWords);
		}

		[TestMethod]
		public void ApplyResultsOfExercise_MarkAllWordsStudiedInUser()
		{
			var status = SetupExercise();
			var wordsShouldBeApplied = status.WordsConsideredCorrect.Concat(status.WordsConsideredIncorrect);

			_finisher.FinishExercise(_exercise.Object.ExerciseId);

			foreach (var wordToApply in wordsShouldBeApplied)
			{
				_user.Verify(user => user.MarkStudied(
					It.Is<Word>(word => word.Original == wordToApply.Original), 
					It.IsAny<bool>()));
			}
		}

		private ExerciseStatus SetupExercise()
		{
			var exerciseWords = GetWordsWithCorrectTranslation()
				.Concat(GetWordsWithIncorrectTranslation())
				.OrderBy(_ => Guid.NewGuid())
				.ToArray();
			var exerciseStatus = new ExerciseStatus(
				_exercise.Object.ExerciseId,
				10,
				0,
				true,
				null,
				exerciseWords.Take(5),
				exerciseWords.Skip(5).Take(5));
			_exercise.SetupGet(ex => ex.Status).Returns(exerciseStatus);
			return exerciseStatus;
		}

		private IEnumerable<ExercisedWord> GetWordsWithCorrectTranslation()
		{
			return _words
				.OrderBy(_ => Guid.NewGuid())
				.Take(5)
				.Select(word => new ExercisedWord(word.Original, word.Translation))
				.ToArray();
		}

		private IEnumerable<ExercisedWord> GetWordsWithIncorrectTranslation()
		{
			return _words
				.OrderBy(_ => Guid.NewGuid())
				.Take(5)
				.Select(word => new ExercisedWord(word.Original, _fixture.Create<string>()))
				.ToArray();
		}

		private IFixture _fixture;
		private ExerciseFinisher _finisher;
		private Mock<IUser> _user;
		private Mock<IExercise> _exercise;
		private IEnumerable<Word> _words;
		private Mock<IExerciseRepository> _exerciseRepoMock;
	}
}