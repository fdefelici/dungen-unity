using System;

public class XFlatHeightMap : IXHeightMap {
    private float _height;

    public XFlatHeightMap(float fixedHeight) {
        _height = fixedHeight;
    }

    public float yFor(float x, float z) {
        return _height;
    }
}