using System;
using System.Collections.Generic;

namespace TranslationTrainer.Domain
{
	public class ExerciseResult
	{
		public ExerciseResult(Guid exerciseId, Guid userId, IEnumerable<ExercisedWordWithResult> exercisedWordWithResults, IEnumerable<Word> completelyLearnedWords)
		{
			ExerciseId = exerciseId;
			UserId = userId;
			ExercisedWordWithResults = exercisedWordWithResults 
				?? throw new ArgumentNullException(nameof(exercisedWordWithResults));
			CompletelyLearnedWords = completelyLearnedWords 
				?? throw new ArgumentNullException(nameof(completelyLearnedWords));
		}

		public Guid ExerciseId { get; }

		public Guid UserId { get; }

		public IEnumerable<ExercisedWordWithResult> ExercisedWordWithResults { get; }
		
		public IEnumerable<Word> CompletelyLearnedWords { get; }
	}
}