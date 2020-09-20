using System;
using System.Collections.Generic;
using System.Linq;

namespace TranslationTrainer.Domain
{
	public class ExerciseFinisher : IExerciseFinisher
	{
		public ExerciseFinisher(
			IWordsRepository wordsRepository, 
			IUserRepository userRepository,
			IExerciseRepository exerciseRepository,
			TranslationTrainerSettings settings)
		{
			_wordsRepository = wordsRepository ?? throw new ArgumentNullException(nameof(wordsRepository));
			_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
			_exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
			_settings = settings ?? throw new ArgumentNullException(nameof(settings));
		}
		
		public ExerciseResult FinishExercise(Guid exerciseId)
		{
			var exercise = _exerciseRepository.GetExercise(exerciseId);
			var exerciseStatus = exercise.Status;
			
			var user = _userRepository.Load(exercise.UserId);
			if (user == null)
			{
				throw new UserNotFoundException(exercise.UserId);
			}

			var translationsDictionary = _wordsRepository.LoadAll()
				.ToDictionary(word => word.Original);

			var exercisedWordsWithResults = exerciseStatus.WordsConsideredCorrect
				.Select(word => GetResultForExercisedWord(translationsDictionary, word, true))
				.Concat(exerciseStatus.WordsConsideredIncorrect.Select(
					word => GetResultForExercisedWord(translationsDictionary, word, false)))
				.ToArray();

			ApplyExerciseResult(user, exercisedWordsWithResults);

			var learnedWords = GetLearnedWords(user, exercisedWordsWithResults);

			var exerciseResult = new ExerciseResult(exercise.ExerciseId, exercise.UserId, exercisedWordsWithResults, learnedWords);

			_exerciseRepository.DeleteExercise(exerciseId);

			return exerciseResult;
		}
		
		private ExercisedWordWithResult GetResultForExercisedWord(
			Dictionary<string, Word> tranlsationsDictionary,
			ExercisedWord exercisedWord,
			bool shouldSuggestedTranslationBeCorrect)
		{
			var actualWord = tranlsationsDictionary[exercisedWord.Original];
			var matchedCorrectly = !(exercisedWord.SuggestedTranslation == actualWord.Translation ^
			                       shouldSuggestedTranslationBeCorrect);
			return new ExercisedWordWithResult(exercisedWord, actualWord, matchedCorrectly);
		}

		private void ApplyExerciseResult(
			IUser user,
			IEnumerable<ExercisedWordWithResult> exercisedWordWithResults)
		{
			foreach (var exercisedWordWithResult in exercisedWordWithResults)
			{
				user.MarkStudied(exercisedWordWithResult.ActualWord, exercisedWordWithResult.IsMatchedCorrectly);
			}

			_userRepository.Save(user);
		}

		private IEnumerable<Word> GetLearnedWords(
			IUser user, 
			IEnumerable<ExercisedWordWithResult> exercisedWordWithResults)
		{
			foreach (var userStudiedWord in user.StudiedWords)
			{
				if (exercisedWordWithResults.Any(word => word.ActualWord.Original == userStudiedWord.Word.Original &&
				                                         userStudiedWord.TimesLearned >= _settings.UserWordsToCompletion))
				{
					yield return userStudiedWord.Word;
				}
			}
		}

		private readonly IWordsRepository _wordsRepository;
		private readonly IUserRepository _userRepository;
		private readonly IExerciseRepository _exerciseRepository;
		private readonly TranslationTrainerSettings _settings;
	}
}