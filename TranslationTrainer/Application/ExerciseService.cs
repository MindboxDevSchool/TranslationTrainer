using System;
using TranslationTrainer.Domain;

namespace TranslationTrainer.Application
{
	public class ExerciseService : IExerciseService
	{
		public ExerciseService(
			IExerciseFactory factory, 
			IExerciseFinisher finisher,
			IExerciseRepository repository)
		{
			_factory = factory ?? throw new ArgumentNullException(nameof(factory));
			_finisher = finisher ?? throw new ArgumentNullException(nameof(finisher));
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		public ExerciseStatus StartExercise(Guid userId)
		{
			var exercise = _factory.Create(userId);
			_repository.SaveExercise(exercise);
			return exercise.Status;
		}

		public ExerciseResult FinishExercise(Guid exerciseId)
		{
			var exerciseResults = _finisher.FinishExercise(exerciseId);
			return exerciseResults;
		}

		public ExerciseStatus CommitCurrentWord(Guid exerciseId, bool isCorrect)
		{
			var exercise = _repository.GetExercise(exerciseId);
			exercise.CommitCurrentTranslation(isCorrect);
			_repository.SaveExercise(exercise);
			return exercise.Status;
		}

		private readonly IExerciseFactory _factory;
		private readonly IExerciseFinisher _finisher;
		private readonly IExerciseRepository _repository;
	}
}