using UnityEngine;
using System;
using static FastNoiseLite;

// Noise code and map generation is from Sebastian Lague's tutorial series on youtube for "Procedural Landmass Generation"
// https://github.com/SebLague/Procedural-Landmass-Generation
// https://youtu.be/wbpMiKiSKm8?si=LvZC6nEyRJAXL9fl
// https://www.youtube.com/@SebastianLague

namespace Snorlax.Procedural.Noise
{
    [CreateAssetMenu(menuName = "Snorlax's Tools/Noise")]
    public class Noise : ScriptableObject
    {
        #region Classes
        [System.Serializable]
        public class FractalVar
        {
            public FastNoiseLite.FractalType fractalType;
            [Range(1, 8)]
            public int octaves;
            
            [Range(0, 4)]
            public float lacunarity;
            [Range(0, 2)]
            public float gain;
            public float weightedStrength;
            public float pingPongStrength;
        }

        [System.Serializable]
        public class CellularVar
        {
            public FastNoiseLite.CellularDistanceFunction DistanceFunction;
            public FastNoiseLite.CellularReturnType ReturnType;
            public float Jitter;
        }

        [System.Serializable]
        public class DomainWarpVar
        {
            public bool Enabled;
            public FastNoiseLite.DomainWarpType type;
            public FastNoiseLite.RotationType3D RotationType3D;
            public float Amplitude;
            [Range(0.001f, 1f)] public float frequency;
            public FastNoiseLite.DomainFractalType fractalType;
            [Range(1, 8)] public int octaves;
            [Range(0, 4)] public float lacunarity;
            [Range(0, 2)] public float gain;
        }

        public enum DrawMode
        {
            NoiseMap, ColorMap
        }
        #endregion

        [Header("Main")]
        public FastNoiseLite.NoiseType NoiseType;
        public int seed;

        [Range(0.001f, 1f)] public float frequency;
        public Vector3 offset;
        public bool is3D;
        public FastNoiseLite noise = new FastNoiseLite();
        public FastNoiseLite domainWrap = new FastNoiseLite();

        [Header("Preview")]
        public DrawMode drawMode = DrawMode.NoiseMap;
        public Gradient gradient;

        [Space(10)]

        public FractalVar Fractal = new FractalVar();
        public CellularVar Cellular = new CellularVar();
        public DomainWarpVar DomainWrap = new DomainWarpVar();

        private void OnEnable()
        {
            SetNoiseParameters();
        }

        public float[,] GenerateNoiseMap(int width, int height)
        {
            return GenerateNoiseMap(width, height, Vector3.zero);
        }

        public float[,] GenerateNoiseMap(int width, int height, Vector3 sample)
        {
            float[,] noiseMap = new float[width, height];
            SetNoiseParameters();

            float halfWidth = width / 2f;
            float halfHeight = height / 2f;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var xPos = (x - halfWidth) * frequency + (sample.x + offset.x) * frequency;
                    var yPos = (y - halfHeight)  * frequency - (sample.y - offset.y) * frequency;
                    var zPos = (sample.z + offset.z);

                    if (is3D)
                    {
                        if(DomainWrap.Enabled) domainWrap.DomainWarp(ref xPos, ref yPos, ref zPos);
                        noiseMap[x, y] = noise.GetNoise(xPos, yPos, zPos);
                    }
                    else
                    {
                        if (DomainWrap.Enabled) domainWrap.DomainWarp(ref xPos, ref yPos);
                        noiseMap[x, y] = noise.GetNoise(xPos, yPos);
                    }
                }
            }

            return noiseMap;
        }

        /// <summary>
        /// Sample is a targeted section of the noise map for endless terrain
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="sample"></param>
        /// <returns></returns>
        public float[,] GenerateNoiseMap(int width, int height, Vector2 sample)
        {
            return GenerateNoiseMap(width, height, new Vector3(sample.x, sample.y, 0));
        }

        public float GetNoise(float x, float y)
        {
            return noise.GetNoise(x, y);
        }

        public void SetNoiseParameters()
        {
            noise.SetSeed(seed);

            noise.SetNoiseType(NoiseType);
            noise.SetFrequency(this.frequency);

            noise.SetFractalType(Fractal.fractalType);
            noise.SetFractalOctaves(Fractal.octaves);
            noise.SetFractalLacunarity(Fractal.lacunarity);
            noise.SetFractalGain(Fractal.gain);

            noise.SetFractalWeightedStrength(Fractal.weightedStrength);

            if (Fractal.fractalType == FractalType.PingPong)
            {
                noise.SetFractalPingPongStrength(Fractal.pingPongStrength);
            }

            if (NoiseType == NoiseType.Cellular)
            {
                noise.SetCellularDistanceFunction(Cellular.DistanceFunction);
                noise.SetCellularReturnType(Cellular.ReturnType);
                noise.SetCellularJitter(Cellular.Jitter);
            }

            if (DomainWrap.Enabled)
            {
                domainWrap.SetSeed(seed);
                domainWrap.SetDomainFractalType(DomainFractalType.None);
                domainWrap.SetDomainWarpAmp(DomainWrap.Amplitude);
                domainWrap.SetFrequency(DomainWrap.frequency);

                domainWrap.SetDomainWarpType(DomainWrap.type);
                domainWrap.SetRotationType3D(DomainWrap.RotationType3D);
                
                domainWrap.SetFractalOctaves(DomainWrap.octaves);
                domainWrap.SetFractalLacunarity(DomainWrap.lacunarity);
                domainWrap.SetFractalGain(DomainWrap.gain);
            }
        }

        public Texture2D GenerateTexture2D(int width, int height, DrawMode drawMode, Gradient gradient = null)
        {
            Texture2D map = null;
            if (drawMode == DrawMode.NoiseMap)
            {
                map = TextureFromHeightMap(GenerateNoiseMap(width, height));
            }
            else
            {
                if (gradient == null) gradient = this.gradient;

                var colorMap = GenerateColorMap(width, height, gradient);
                map = TextureFromColorMap(colorMap, width, height);
            }

            return map;
        }

        /// <summary>
        /// Returns black and white texture
        /// </summary>
        /// <param name="heightMap"></param>
        /// <returns></returns>
        public Texture2D TextureFromHeightMap(float[,] heightMap)
        {
            int width = heightMap.GetLength(0);
            int height = heightMap.GetLength(1);

            Color[] colorMap = new Color[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
                }
            }

            return TextureFromColorMap(colorMap, width, height);
        }

        /// <summary>
        /// Generates colored map based on gradient given. Note that values will go below -1 hence will be heavily skewed towards the negative
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="gradient"></param>
        /// <returns></returns>
        public Color[] GenerateColorMap(int width, int height, Gradient gradient)
        {
            float[,] noiseMap = GenerateNoiseMap(width, height);

            Color[] colorMap = new Color[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float currentHeight = noiseMap[x, y];
                    colorMap[y * width + x] = gradient.Evaluate(currentHeight);
                }
            }

            return colorMap;
        }

        private Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
        {
            Texture2D texture = new Texture2D(width, height);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.SetPixels(colorMap);
            texture.Apply();
            return texture;
        }
    }
}