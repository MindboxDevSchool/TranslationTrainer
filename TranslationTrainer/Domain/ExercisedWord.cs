using System;

namespace TranslationTrainer.Domain
{
	public class ExercisedWord
	{
		public ExercisedWord(string original, string suggestedTranslation)
		{
			Original = original ?? throw new ArgumentNullException(nameof(original));
			SuggestedTranslation = suggestedTranslation ?? throw new ArgumentNullException(nameof(suggestedTranslation));
		}

		public string Original { get; }

		public string SuggestedTranslation { get; }
	}
}