using System;

namespace TranslationTrainer.Domain
{
	public interface IExerciseFactory
	{
		IExercise Create(Guid userId);
	}
}