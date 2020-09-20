using System;

namespace TranslationTrainer.Domain
{
	public interface IExercise
	{
		Guid ExerciseId { get; }

		Guid UserId { get; }

		void CommitCurrentTranslation(bool isCorrect);

		ExerciseStatus Status { get; }
	}
}