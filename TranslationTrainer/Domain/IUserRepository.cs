using System;

namespace TranslationTrainer.Domain
{
	public interface IUserRepository
	{
		IUser Load(Guid userId);
		IUser Load(string login);
		void Save(IUser user);
	}
}