using System;
using System.Linq;

namespace TranslationTrainer.Domain
{
	public class ExerciseFactory : IExerciseFactory
	{
		public ExerciseFactory(
			IUserRepository userRepository,
			IWordsRepository wordsRepository,
			TranslationTrainerSettings settings)
		{
			_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
			_wordsRepository = wordsRepository ?? throw new ArgumentNullException(nameof(wordsRepository));
			_settings = settings ?? throw new ArgumentNullException(nameof(settings));
		}

		public IExercise Create(Guid userId)
		{
			var user = _userRepository.Load(userId);
			if (user == null)
			{
				throw new UserNotFoundException(userId);
			}

			var allWords = _wordsRepository.LoadAll().ToArray();

			var userCompletedWords = user.StudiedWords
				.Where(IsUncomplete)
				.Select(word => word.Word);

			var random = new Random();
			var wordsForExercise = allWords
				.Except(userCompletedWords)
				.OrderBy(_ => random.Next())
				.Take(_settings.ExerciseWordsCount)
				.Select(word => GetExercisedWord(word, allWords, random));

			return new Exercise(Guid.NewGuid(), userId, wordsForExercise);
		}

		private bool IsUncomplete(StudiedWord studiedWord)
		{
			return studiedWord.TimesLearned < _settings.UserWordsToCompletion;
		}

		private static ExercisedWord GetExercisedWord(Word word, Word[] allWords, Random random)
		{
			var shouldReplaceTranslation = random.Next() % 2 == 0;
			if (!shouldReplaceTranslation)
			{
				return new ExercisedWord(word.Original, word.Translation);
			}

			var alternaveTranslation = allWords[random.Next(allWords.Length)].Translation;
			return new ExercisedWord(word.Original, alternaveTranslation);
		} 

		private readonly IUserRepository _userRepository;
		private readonly IWordsRepository _wordsRepository;
		private readonly TranslationTrainerSettings _settings;
	}
}