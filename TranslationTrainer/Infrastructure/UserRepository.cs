using System;
using TranslationTrainer.Domain;

namespace TranslationTrainer.Infrastructure
{
	public class UserRepository : IUserRepository
	{
		public IUser Load(Guid userId)
		{
			throw new NotImplementedException();
		}

		public IUser Load(string login)
		{
			throw new NotImplementedException();
		}

		public void Save(IUser user)
		{
			throw new NotImplementedException();
		}
	}
}