using System;
using System.Collections.Generic;
using TranslationTrainer.Domain;

namespace TranslationTrainer.Application
{
	public class UserService : IUserService
	{
		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		}

		public Guid RegisterUser(string userName, string password)
		{
			if (userName == null) throw new ArgumentNullException(nameof(userName));
			if (password == null) throw new ArgumentNullException(nameof(password));

			var userId = Guid.NewGuid();
			var credentials = Credentials.FromLoginAndPassword(userName, password);
			var user = new User(userId, credentials, new StudiedWord[0]);
			_userRepository.Save(user);

			return userId;
		}

		public IEnumerable<StudiedWord> GetStudiedWords(Guid userId)
		{
			var user = _userRepository.Load(userId);
			if (user != null)
			{
				return user.StudiedWords;
			}

			return new StudiedWord[0];
		}

		private readonly IUserRepository _userRepository;
	}
}