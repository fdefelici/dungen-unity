namespace DungeonGeneration.Generator.Pickers {

    public interface IPickerStrategy {
        int drawBetween(int min, int max);
    }
}
