using NoiseTest;
using UnityEngine;

public class FogGeneration : MonoBehaviour
{
    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public float fogSpeed = 30f;

    //public AnimationCurve curve;

    public float timer = 0f;
    public float renderTimer = 0f;

    private OpenSimplexNoise simplex;

    void Start()
    {
        simplex = new OpenSimplexNoise();
    }

    void Update()
    {
        timer += Time.deltaTime;
        renderTimer += Time.deltaTime;
        if (renderTimer > 0.1f)
        {
            RenderTexture();
            renderTimer = 0f;
        }
    }

    void RenderTexture()
    {

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        // Generate Perlin Noise Map for the texture

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * scale +  offsetX;
        float yCoord = (float)y / height * scale + offsetY;
        float zCoord = timer / fogSpeed;

        //float sample = Mathf.PerlinNoise(xCoord, yCoord);
        //float sampleTime = Mathf.PerlinNoise(0, zCoord);
        float sample = (float)simplex.Evaluate(xCoord, yCoord, zCoord);

        float sampleY;

        if (sample < 0f)
        {
            return new Color(0, 0, 0, 0);
        }
        else
        {
            //sampleY = curve.Evaluate(sample);
            sampleY = sample;
            return new Color(sampleY, sampleY, sampleY);
        }
        
    }
}
