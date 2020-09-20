using System;

namespace TranslationTrainer
{
	public class TranslationTrainerSettings
	{
		public TranslationTrainerSettings(int exerciseWordsCount, int userWordsToCompletion)
		{
			if (exerciseWordsCount <= 0) throw new ArgumentOutOfRangeException(nameof(exerciseWordsCount));
			if (userWordsToCompletion <= 0) throw new ArgumentOutOfRangeException(nameof(userWordsToCompletion));
			ExerciseWordsCount = exerciseWordsCount;
			UserWordsToCompletion = userWordsToCompletion;
		}

		public int ExerciseWordsCount { get; }

		public int UserWordsToCompletion { get; }
	}
}