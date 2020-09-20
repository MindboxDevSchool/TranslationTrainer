using System;
using System.Collections.Generic;
using System.Linq;

namespace TranslationTrainer.Domain
{
	public class Exercise : IExercise
	{
		public Exercise(Guid exerciseId, Guid userId, IEnumerable<ExercisedWord> exercisedWords)
		{
			ExerciseId = exerciseId;
			UserId = userId;
			_wordsDone = 0;
			_wordsLeft = exercisedWords.Count();
			_wordsEnumerator = exercisedWords?.GetEnumerator() 
				?? throw new ArgumentNullException(nameof(exercisedWords));
			_isFinished = !_wordsEnumerator.MoveNext();
			_currentExercisedWord = _wordsEnumerator.Current;
		}
		
		public Guid ExerciseId { get; }
		
		public Guid UserId { get; }

		public void CommitCurrentTranslation(bool isCorrect)
		{
			if (_isFinished)
			{
				throw new InvalidOperationException("Can not commit translations to finished exercise");
			}

			if (isCorrect)
			{
				_wordsConsideredCorrect.Add(_currentExercisedWord);
			}
			else
			{
				_wordsConsideredIncorrect.Add(_currentExercisedWord);
			}

			_isFinished = !_wordsEnumerator.MoveNext();
			_currentExercisedWord = _isFinished ? null : _wordsEnumerator.Current;
			_wordsDone++;
			_wordsLeft--;
		}

		public ExerciseStatus Status => new ExerciseStatus(
			ExerciseId,
			_wordsDone,
			_wordsLeft,
			_isFinished,
			_currentExercisedWord,
			_wordsConsideredCorrect,
			_wordsConsideredIncorrect);

		private int _wordsDone;
		private int _wordsLeft;
		private readonly List<ExercisedWord> _wordsConsideredCorrect = new List<ExercisedWord>();
		private readonly List<ExercisedWord> _wordsConsideredIncorrect = new List<ExercisedWord>();
		private readonly IEnumerator<ExercisedWord> _wordsEnumerator;
		private bool _isFinished;
		private ExercisedWord _currentExercisedWord;
	}
}