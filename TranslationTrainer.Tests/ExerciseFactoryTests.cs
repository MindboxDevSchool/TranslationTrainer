using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using TranslationTrainer.Domain;

namespace TranslationTrainer.Tests
{
	[TestClass]
	public class ExerciseFactoryTests
	{
		[TestMethod]
		public void CreateExercise_ReturnsNotFinishedExercise()
		{
			var fixture = new Fixture();
			var words = fixture.CreateMany<Word>();
			var userMock = new Mock<IUser>();
			userMock.SetupGet(mock => mock.StudiedWords).Returns(new StudiedWord[0]);
			var userRepositoryMock = new Mock<IUserRepository>();
			userRepositoryMock
				.Setup(mock => mock.Load(It.IsAny<Guid>()))
				.Returns(userMock.Object);
			var wordRepositoryMock = new Mock<IWordsRepository>();
			wordRepositoryMock.Setup(mock => mock.LoadAll()).Returns(words);
			var settings = new TranslationTrainerSettings(5, 3);
			var factory = new ExerciseFactory(
				userRepositoryMock.Object,
				wordRepositoryMock.Object,
				settings);

			var exercise = factory.Create(Guid.NewGuid());

			Assert.IsFalse(exercise.Status.IsFinished);
		}
	}
}