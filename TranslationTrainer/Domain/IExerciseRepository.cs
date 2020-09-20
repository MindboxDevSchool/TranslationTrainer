using System;

namespace TranslationTrainer.Domain
{
	public interface IExerciseRepository
	{
		IExercise GetExercise(Guid exerciseId);

		void SaveExercise(IExercise exercise);

		void DeleteExercise(Guid exerciseId);
	}
}