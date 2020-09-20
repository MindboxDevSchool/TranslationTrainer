using System;

namespace TranslationTrainer.Domain
{
	public class Word
	{
		public Word(string original, string translation)
		{
			Original = original ?? throw new ArgumentNullException(nameof(original));
			Translation = translation ?? throw new ArgumentNullException(nameof(translation));
		}

		public string Original { get; }

		public string Translation { get; }

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((Word) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Original.GetHashCode() * 397) ^ Translation.GetHashCode();
			}
		}

		public static bool operator ==(Word left, Word right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Word left, Word right)
		{
			return !Equals(left, right);
		}
		
		private bool Equals(Word other)
		{
			return string.Equals(Original, other.Original) && string.Equals(Translation, other.Translation);
		}
	}
}