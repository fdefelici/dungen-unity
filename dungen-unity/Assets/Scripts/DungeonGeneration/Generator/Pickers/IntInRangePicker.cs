namespace DungeonGeneration.Generator.Pickers {

    public class IntInRangePicker {
        private int _max;
        private int _min;
        private IPickerStrategy _pickStrategy;

        public IntInRangePicker(int min, int max, IPickerStrategy pickStrategy) {
            _min = min;
            _max = max;
            _pickStrategy = pickStrategy;
        }

        public int draw() {
            return _pickStrategy.drawBetween(_min, _max);
        }
    }
}
