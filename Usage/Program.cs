using System;
using TranslationTrainer;
using TranslationTrainer.Application;
using TranslationTrainer.Domain;
using TranslationTrainer.Infrastructure;

namespace Usage
{
    class Program
    {
        static void Main(string[] args)
        {
            var compositionRoot = CompositionRoot.Create();
            var method = args[0];
            switch (method)
            {
                case "start":
                    compositionRoot.ExerciseService.StartExercise(Guid.Parse(args[1]));
                    break;
                case "commit":
                    compositionRoot.ExerciseService.CommitCurrentWord(Guid.Parse(args[1]), Boolean.Parse(args[2]));
            }
            
        }
    }
}