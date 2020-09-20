using System;
using System.Collections.Generic;

namespace TranslationTrainer.Domain
{
	public class ExerciseStatus
	{
		public ExerciseStatus(
			Guid exerciseId,
			int wordsDone,
			int wordsLeft,
			bool isFinished,
			ExercisedWord currentWord,
			IEnumerable<ExercisedWord> wordsConsideredCorrect,
			IEnumerable<ExercisedWord> wordsConsideredIncorrect)
		{
			WordsDone = wordsDone;
			WordsLeft = wordsLeft;
			IsFinished = isFinished;
			CurrentWord = currentWord;
			WordsConsideredCorrect = wordsConsideredCorrect;
			WordsConsideredIncorrect = wordsConsideredIncorrect;
			ExerciseId = exerciseId;
		}

		public Guid ExerciseId { get; }
		public int WordsDone { get; }
		public int WordsLeft { get; }
		public bool IsFinished { get; }
		public ExercisedWord CurrentWord { get; }
		public IEnumerable<ExercisedWord> WordsConsideredCorrect { get; }
		public IEnumerable<ExercisedWord> WordsConsideredIncorrect { get; }
	}
}