using System;
using System.Collections.Generic;
using TranslationTrainer.Domain;

namespace TranslationTrainer.Infrastructure
{
	public class ExerciseRepository : IExerciseRepository
	{
		public IExercise GetExercise(Guid exerciseId)
		{
			if (!_exerciseDictionary.TryGetValue(exerciseId, out var exercise))
			{
				throw new ExerciseNotFoundException(exerciseId);
			}

			return exercise;
		}

		public void SaveExercise(IExercise exercise)
		{
			_exerciseDictionary[exercise.ExerciseId] = exercise;
		}

		public void DeleteExercise(Guid exerciseId)
		{
			_exerciseDictionary.Remove(exerciseId);
		}

		private readonly Dictionary<Guid, IExercise> _exerciseDictionary = new Dictionary<Guid, IExercise>();
	}
}