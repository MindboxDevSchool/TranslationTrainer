using System;
using System.Collections.Generic;
using TranslationTrainer.Domain;

namespace TranslationTrainer.Application
{
	public interface IUserService
	{
		Guid RegisterUser(string userName, string password);

		IEnumerable<StudiedWord> GetStudiedWords(Guid userId);
	}
}