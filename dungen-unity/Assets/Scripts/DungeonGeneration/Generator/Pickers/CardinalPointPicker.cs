namespace DungeonGeneration.Generator.Pickers {
    public class CardinalPointPicker {
        private IntInRangePicker _intRangePicker;
        public CardinalPointPicker(IPickerStrategy aStrategy) {
            _intRangePicker = new IntInRangePicker(0, 3, aStrategy);
        }

        public CardinalPoint draw() {
            return (CardinalPoint)_intRangePicker.draw();
        }

        public CardinalPoint nextClockwise(CardinalPoint aPoint) {
            int next = ((int)aPoint + 1) % 4;
            return (CardinalPoint)next;
        }
    }
}