using System;

namespace DungeonGeneration.Generator.Pickers {
    /* 
        Implementation based on System.Random API
    */
    public class RandomSeededPickerStrategy : IPickerStrategy {
        private Random _random;
        private int _seed;

        public RandomSeededPickerStrategy(int seed) {
            _seed = seed;
            _random = new Random(seed);
        }

        public int drawBetween(int min, int max) {
            return _random.Next(min, max+1);
        }
    }
}