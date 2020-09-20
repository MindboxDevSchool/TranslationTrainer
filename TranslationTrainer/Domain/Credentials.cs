using System;
using System.Security.Cryptography;
using System.Text;

namespace TranslationTrainer.Domain
{
	public class Credentials
	{
		public Credentials(string login, string passwordHash)
		{
			Login = login;
			PasswordHash = passwordHash;
		}

		public static Credentials FromLoginAndPassword(string login, string password)
		{
			using (var sha256 = SHA256.Create())
			{
				var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
				var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
				
				return new Credentials(login, hash);
			}
		}

		public string Login { get; }
		public string PasswordHash { get; }
	}
}