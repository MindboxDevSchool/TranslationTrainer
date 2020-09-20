using System;
using TranslationTrainer.Domain;

namespace TranslationTrainer.Application
{
	public interface IExerciseService
	{
		ExerciseStatus StartExercise(Guid userId);

		ExerciseResult FinishExercise(Guid exerciseId);

		ExerciseStatus CommitCurrentWord(Guid exerciseId, bool isCorrect);
	}
}