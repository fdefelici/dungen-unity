namespace DungeonGeneration.Generator.Pickers {

    public class FloatInRangePicker {
        private float _max;
        private float _min;
        private IPickerStrategy _pickStrategy;

        public FloatInRangePicker(float min, float max, IPickerStrategy pickStrategy) {
            _min = min;
            _max = max;
            _pickStrategy = pickStrategy;
        }

        public float draw() {
            int min = (int)(_min * 10);
            int max = (int)(_max * 10);
            int picked = _pickStrategy.drawBetween(min, max);
            float result = (float)picked / 10f;
            return result;
        }
    }
}
