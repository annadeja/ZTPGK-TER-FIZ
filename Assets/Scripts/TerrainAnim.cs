using UnityEngine;
using System.Collections;
using System;
//! Sample terrain animator/generator
public class TerrainAnim : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    private TerrainData terrainData;
    [SerializeField] private Sprite sign;
    private Texture2D signTexture;
    private int xRes;
    private int yRes;
    private float[] harmonicFrequencies = new float[5];
    float[,] terrHeights;
    float[,] originalTerrainSectionHeight;
    public int numberOfPasses = 5;
    public int radiusOfAnimation = 100;
    // Use this for initialization
    void Start()
    {
        // Get terrain and terrain data handles
        terrainData = terrain.terrainData;
        // Get terrain dimensions in tiles (X tiles x Y tiles)
        xRes = terrainData.heightmapWidth;
        yRes = terrainData.heightmapHeight;
        // Set heightmap
        harmonicFrequencies[0] = 1.0f;
        for (int i = 1; i<harmonicFrequencies.Length;i++)
            harmonicFrequencies[i] = harmonicFrequencies[i-1] + UnityEngine.Random.Range(1.0f, 3.0f);
        //signTexture = sign.texture;
        RandomizeTerrain();
    }
    // Update is called once per frame
    void Update()
    {
        // Call animation function
        AnimTerrain();
    }

    private float[,] LoadSign()
    {
        var pixelArray = sign.texture.GetPixels32();
        float grayscale = 0.0f;
        for (int i = 0; i < sign.texture.width; i++)
        {
            for (int j = 0; j < sign.texture.height; j++)
            {
                grayscale = 255.0f - (float)(pixelArray[i * sign.texture.width + j].r + pixelArray[i * sign.texture.width + j].g + pixelArray[i * sign.texture.width + j].b) / 3.0f;
                //Debug.Log(grayscale / 250.0f);
                grayscale /= 10000.0f;
                if (grayscale != 0.0f)
                    terrHeights[i, j] += grayscale;
            }
        }
        return terrHeights;
    }

    // Set the terrain using noise pattern
    private void RandomizeTerrain()
    {
        // Extract entire heightmap (expensive!)
        terrHeights = terrainData.GetHeights(0, 0, xRes, yRes);
        // STUDENT'S CODE //
        // ...
        float[] scale = new float[5];
        for (int k = 0; k < numberOfPasses; k++)
        {
            scale[k] = UnityEngine.Random.Range(1.0f, 3.5f);
        }
        for (int i = 0; i < xRes; i++)
        {
            for (int j = 0; j < yRes; j++)
            {
                float xCoeff = (float)i / xRes * 1.5f;
                float yCoeff = (float)j / yRes * 1.5f;
                terrHeights[i, j] = 0;
                for (int k = 0; k < numberOfPasses; k++)
                {
                    terrHeights[i, j] +=
                    Mathf.PerlinNoise(xCoeff * scale[k], yCoeff * scale[k]);
                }
                terrHeights[i, j] /= (float)numberOfPasses*numberOfPasses;
            }
        }
        //
        // Set terrain heights (terrHeights[coordX, coordY] = heightValue) in a loop
        //
        // You can sample perlin's noise (Mathf.PerlinNoise (xCoeff, yCoeff)) using
        //coefficients
        // between 0.0f and 1.0f
        //
        // You can combine 2-3 layers of noise with different resolutions and amplitudes for
        //a better effect
        // ...
        // END OF STUDENT'S CODE //
        // Set entire heightmap (expensive!)
        terrHeights = LoadSign();
        terrainData.SetHeights(0, 0, terrHeights);
        originalTerrainSectionHeight = terrainData.GetHeights(147, 168, radiusOfAnimation * 2,
        radiusOfAnimation * 2);
    }
    // Animate part of the terrain
    private void AnimTerrain()
    {
        // STUDENT'S CODE //
        // ...
        // Extract PART of the terrain e.g. 40x40 tiles (select corner (x, y) and extracted
        //patch size)
        // GetHeights(5, 5, 10, 10) will extract 10x10 tiles at position (5, 5)
        //
        // Animate it using Time.time and trigonometric functions
        //
        // 3d generalizaton of sinc(x) function can be used to create the teardrop effect
        //(sinc(x) = sin(x) / x)
        //
        // It is reasonable to store animated part of the terrain in temporary variable e.g.
        //in RandomizeTerrain()
        // function. Later, in AnimTerrain() this temporary area can be combined with
        //calculated Z(height) value.
        // Make sure you make a deep copy instead of shallow one (Clone(), assign operator).
        //
        // Set PART of the terrain (use extraction parameters)
        //
        // END OF STUDENT'S CODE //
        terrHeights = terrainData.GetHeights(147, 168, radiusOfAnimation * 2,
        radiusOfAnimation * 2);
        Vector2 middle = new Vector2(radiusOfAnimation, radiusOfAnimation);
        for (int i = 0; i < radiusOfAnimation * 2; i++)
        {
            for (int j = 0; j < radiusOfAnimation * 2; j++)
            {
                double calculatedHeight = 0.0f;
                Vector2 point = new Vector2(i, j);
                double distance = Vector2.Distance(point, middle);
                double difference = (radiusOfAnimation - distance) / radiusOfAnimation;
                if (difference < 0) difference = 0;
                for(int k = 0; k < harmonicFrequencies.Length; k++)
                {
                    calculatedHeight += Math.Sin(Time.time * harmonicFrequencies[k] + distance / 100) / 2f / harmonicFrequencies[k];
                }
                
                terrHeights[i, j] = (float)(originalTerrainSectionHeight[i, j] * calculatedHeight * (float)(Math.Cos(Time.time * 2.5f)) + originalTerrainSectionHeight[i, j]);
                
            }
        }
        terrainData.SetHeights(147, 168, terrHeights);
    }
}