using System;

namespace TranslationTrainer.Domain
{
	public class ExerciseNotFoundException : Exception
	{
		public ExerciseNotFoundException(Guid exerciseId) 
			: base($"Exercise with id {exerciseId} not found")
		{
		}

		public ExerciseNotFoundException(string message) : base(message)
		{
		}

		public ExerciseNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}