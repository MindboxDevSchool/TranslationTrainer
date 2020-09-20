using TranslationTrainer;
using TranslationTrainer.Application;
using TranslationTrainer.Domain;
using TranslationTrainer.Infrastructure;

namespace Usage
{
    public class CompositionRoot
    {
        public static CompositionRoot Create()
        {
            var userRepository = new UserRepository();
            var translationTrainerSettings = new TranslationTrainerSettings(10, 3);
            var exerciseRepository = new ExerciseRepository();
            var wordsRepository = new WordsRepository();
            return new CompositionRoot()
            {
                ExerciseService = new ExerciseService(
                    new ExerciseFactory(userRepository, wordsRepository, translationTrainerSettings),
                    new ExerciseFinisher(wordsRepository, userRepository, exerciseRepository,
                        translationTrainerSettings),
                    exerciseRepository)
            };
        }
        
        public IExerciseService ExerciseService { get; private set; }
        
        public IUserService UserService { get; private set; }
    }
}