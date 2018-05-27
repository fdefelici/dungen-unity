using System;
using UnityEngine;

public class XPerlinHeightMap : IXHeightMap {
    private float _delta;
    private float _maxHeight;
    private float _minHeight;
    private XPerlinNoise _noise;

    public XPerlinHeightMap(float minHeight, float maxHeight, int seed) {
        _minHeight = minHeight;
        _maxHeight = maxHeight;
        _delta = _maxHeight - _minHeight;
        _noise = new XPerlinNoise(1f, seed);
    }

    public float yFor(float x, float z) {
        return _minHeight + _delta * _noise.drawFor(x, z);
    }

    class XPerlinNoise {
        float[] xOffsets;
        float[] yOffsets; // Offsets provide randomness to the noise by offsetting the points of perlin noise we sample.
        float[] amplitudes; // Descending coefficients for the contribution of each layer.
        float[] frequencies; // Ascending coefficients for how closely together points of each layer are sampled.

        const int OFFSET_MINIMUM = -10000;
        const int OFFSET_MAXIMUM = 10000;

        public XPerlinNoise(float scale, int seed) : this(1, 1f, 1f, scale, seed) { }

        public XPerlinNoise(int numLayers, float amplitudePersistance, float frequencyGrowth, float scale, int seed) {
            CreateOffsets(seed, numLayers);
            amplitudes = GetArrayOfExponents(amplitudePersistance, numLayers);
            frequencies = GetArrayOfExponents(frequencyGrowth, numLayers);
            for (int i = 0; i < frequencies.Length; i++) {
                frequencies[i] /= scale;
            }
        }

        public float drawFor(float x, float y) {
            float height = 0f;
            for (int i = 0; i < amplitudes.Length; i++) {
                float freq = frequencies[i];
                float perlinValue = Mathf.PerlinNoise(xOffsets[i] + x * freq, yOffsets[i] + y * freq) * 2 - 1;
                height += perlinValue * amplitudes[i];
            }
            return Mathf.InverseLerp(-1f, 1f, height);
        }

        void CreateOffsets(int seed, int numLayers) {
            xOffsets = new float[numLayers];
            yOffsets = new float[numLayers];
            System.Random random = new System.Random(seed);
            for (int i = 0; i < numLayers; i++) {
                xOffsets[i] = random.Next(OFFSET_MINIMUM, OFFSET_MAXIMUM);
                yOffsets[i] = random.Next(OFFSET_MINIMUM, OFFSET_MAXIMUM);
            }
        }

        float[] GetArrayOfExponents(float factor, int length) {
            float[] exponents = new float[length];
            for (int i = 0; i < length; i++) {
                exponents[i] = Mathf.Pow(factor, i);
            }
            return exponents;
        }
    }
}