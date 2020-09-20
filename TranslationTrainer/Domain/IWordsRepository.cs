using System.Collections.Generic;

namespace TranslationTrainer.Domain
{
	public interface IWordsRepository
	{
		IEnumerable<Word> LoadAll();
	}
}