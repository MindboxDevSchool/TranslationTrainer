using System;

namespace TranslationTrainer.Domain
{
	public class StudiedWord
	{
		public StudiedWord(Guid userId, Word word, int timesLearned)
		{
			UserId = userId;
			Word = word ?? throw new ArgumentNullException(nameof(word));
			if (timesLearned < 0) throw new ArgumentOutOfRangeException(nameof(timesLearned));
			TimesLearned = timesLearned;
		}

		public Guid UserId { get; }
		public Word Word { get; }
		public int TimesLearned { get; }
	}
}