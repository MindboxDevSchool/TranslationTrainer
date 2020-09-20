using System.Collections.Generic;
using TranslationTrainer.Domain;

namespace TranslationTrainer.Infrastructure
{
	public class WordsRepository : IWordsRepository
	{
		public IEnumerable<Word> LoadAll()
		{
			throw new System.NotImplementedException();
		}
	}
}