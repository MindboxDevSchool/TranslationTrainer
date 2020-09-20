using System;

namespace TranslationTrainer.Domain
{
	public class ExercisedWordWithResult
	{
		public ExercisedWordWithResult(
			ExercisedWord exercisedWord,
			Word actualWord,
			bool isMatchedCorrectly)
		{
			ExercisedWord = exercisedWord ?? throw new ArgumentNullException(nameof(exercisedWord));
			ActualWord = actualWord ?? throw new ArgumentNullException(nameof(actualWord));
			IsMatchedCorrectly = isMatchedCorrectly;
		}

		public ExercisedWord ExercisedWord { get; }

		public Word ActualWord { get; }

		public bool IsMatchedCorrectly { get; }
	}
}