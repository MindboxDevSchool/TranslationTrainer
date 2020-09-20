using System;
using System.Collections.Generic;

namespace TranslationTrainer.Domain
{
	public class User : IUser
	{
		public User(Guid id, Credentials credentials, IEnumerable<StudiedWord> studiedWords)
		{
			Id = id;
			Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
			_studiedWords = new List<StudiedWord>(studiedWords);
		}

		public Guid Id { get; }

		public Credentials Credentials { get; }

		public IEnumerable<StudiedWord> StudiedWords => _studiedWords;

		public void MarkStudied(Word word, bool correctTranslationChosen)
		{
			if (word == null) throw new ArgumentNullException(nameof(word));

			var existingWord = _studiedWords.Find(studiedWord => word.Original == studiedWord.Word.Original);
			if (existingWord != null)
			{
				var updatedWord = new StudiedWord(
					Id,
					word,
					correctTranslationChosen ? existingWord.TimesLearned + 1 : existingWord.TimesLearned);
				_studiedWords.Remove(existingWord);
				_studiedWords.Add(updatedWord);
				return;
			}

			var newWord = new StudiedWord(Id, word, correctTranslationChosen ? 1 : 0);
			_studiedWords.Add(newWord);
		}

		private readonly List<StudiedWord> _studiedWords;
	}
}