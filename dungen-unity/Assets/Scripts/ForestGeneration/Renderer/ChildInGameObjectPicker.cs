using DungeonGeneration.Generator.Pickers;
using UnityEngine;

public class ChildInGameObjectPicker {
    private GameObject _container;
    private IntInRangePicker _indexPicker;
    private IPickerStrategy _pickStrategy;

    public ChildInGameObjectPicker(GameObject container, IPickerStrategy pickStrategy) {
        _container = container;
        _pickStrategy = pickStrategy;

        int maxNum = container.transform.childCount;
        _indexPicker = new IntInRangePicker(0, maxNum - 1, pickStrategy);
    }

    public GameObject draw() {
        int index = _indexPicker.draw();
        GameObject result = _container.transform.GetChild(index).gameObject;
        return result;
    }
}
