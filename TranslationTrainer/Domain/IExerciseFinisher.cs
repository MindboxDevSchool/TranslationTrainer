using System;

namespace TranslationTrainer.Domain
{
	public interface IExerciseFinisher
	{
		ExerciseResult FinishExercise(Guid exerciseId);
	}
}