using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using TranslationTrainer.Domain;

namespace TranslationTrainer.Tests
{
	[TestClass]
	public class ExerciseTests
	{
		[TestInitialize]
		public void Initialize()
		{
			_fixture = new Fixture();	
		}

		[TestMethod]
		public void AfterCommitingAllWords_ExerciseShouldBeFinished()
		{
			var words = _fixture.CreateMany<ExercisedWord>();
			var exercise = new Exercise(Guid.NewGuid(), Guid.NewGuid(), words);

			foreach (var word in words)
			{
				exercise.CommitCurrentTranslation(false);
			}

			Assert.IsTrue(exercise.Status.IsFinished);
		}

		[TestMethod]
		public void CommitWordToFinishedExercise_ThrowsInvalidOperationException()
		{
			var words = _fixture.CreateMany<ExercisedWord>();
			var exercise = new Exercise(Guid.NewGuid(), Guid.NewGuid(), words);

			foreach (var word in words)
			{
				exercise.CommitCurrentTranslation(false);
			}

			Assert.ThrowsException<InvalidOperationException>(
				() => exercise.CommitCurrentTranslation(false));
		}

		[TestMethod]
		public void CommitWordWithIsCorrectTrue_AddsWordToConsideredCorrect()
		{
			var exercise = _fixture.Create<Exercise>();
			var currentWord = exercise.Status.CurrentWord;

			exercise.CommitCurrentTranslation(true);
			var status = exercise.Status;
			var consideredCorrect = status.WordsConsideredCorrect.Contains(currentWord);
			var consideredIncorrect = status.WordsConsideredIncorrect.Contains(currentWord);
			
			Assert.IsTrue(consideredCorrect);
			Assert.IsFalse(consideredIncorrect);
		}

		[TestMethod]
		public void CommitWordWithIsIncorrectTrue_AddsWordToConsideredIncorrect()
		{
			var exercise = _fixture.Create<Exercise>();
			var currentWord = exercise.Status.CurrentWord;

			exercise.CommitCurrentTranslation(false);
			var status = exercise.Status;
			var consideredCorrect = status.WordsConsideredCorrect.Contains(currentWord);
			var consideredIncorrect = status.WordsConsideredIncorrect.Contains(currentWord);

			Assert.IsFalse(consideredCorrect);
			Assert.IsTrue(consideredIncorrect);
		}

		private Fixture _fixture;
	}
}