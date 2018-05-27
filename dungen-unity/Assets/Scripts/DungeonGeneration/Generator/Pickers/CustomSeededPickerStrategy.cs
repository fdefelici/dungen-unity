using System;
using DungeonGeneration.Logging;

namespace DungeonGeneration.Generator.Pickers {

    public class CustomSeededPickerStrategy : IPickerStrategy {
        private int _originalSeed;
        private int _currentSeed;
        private IXLogger _logger;

        public CustomSeededPickerStrategy(int seed) {
            _originalSeed = seed;
            _currentSeed = seed;
            _logger = new NullLogger();
        }

        public void setLogger(IXLogger logger) {
            _logger = logger;
        }

        public int drawBetween(int valueA, int valueB) {
            //_logger.info("seed: " + _currentSeed);
            //_logger.info("range: " + valueA + " " + valueB);


            double baseNumber = Math.Sin(_currentSeed) * 10000;
            //_logger.info("base: " + baseNumber);
            double percentage = baseNumber - Math.Floor(baseNumber);
            //_logger.info("percentage: " + percentage);

            double rangeDiff = Math.Abs(valueB - valueA) + 1;
            //_logger.info("rangeDiff: " + rangeDiff);

            double sel = percentage * rangeDiff - 1;
            //_logger.info("selection: " + sel);

            //Can't use the enum because DuoCode (C# to JS Converter) 
            // fails converting MidpointRounding.AwayFromZero 
            int index = (int)Math.Round(sel, 0);
            //_logger.info("index before: " + index);
            if (index < 0) index = 0;
            //_logger.info("index after: " + index);

            int result;
            if (valueA <= valueB) {
                result = valueA + index;
            } else {
                result = valueA - index;
            }
            //_logger.info("result: " + result);
            if (_originalSeed >= 0) _currentSeed++;
            else _currentSeed--;
            return result;
        }

        
    }
}