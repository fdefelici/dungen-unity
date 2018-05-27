namespace DungeonGeneration.Generator.Pickers {
    public class MockPickerStrategy : IPickerStrategy {
        private int _whichSelect;

        public MockPickerStrategy(int whichSelect) {
            _whichSelect = whichSelect;
        }

        public int drawBetween(int min, int max) {
            return min + _whichSelect - 1;
        }
    }
}

