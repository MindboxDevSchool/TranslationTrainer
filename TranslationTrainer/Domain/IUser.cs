using System;
using System.Collections.Generic;

namespace TranslationTrainer.Domain
{
	public interface IUser
	{
		Guid Id { get; }
		Credentials Credentials { get; }
		IEnumerable<StudiedWord> StudiedWords { get; }
		void MarkStudied(Word word, bool correctTranslationChosen);
	}
}