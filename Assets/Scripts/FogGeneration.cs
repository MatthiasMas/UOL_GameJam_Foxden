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

    private float timer = 0f;
    private float renderTimer = 0f;

    private OpenSimplexNoise simplex;
    public PlayerMovement player;

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

    private void FixedUpdate()
    {
        offsetX = player.gameObject.transform.position.x;
        offsetY = player.gameObject.transform.position.y;
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
        float xCoord1 = (float)x / width * scale + offsetX - player.deltaOffset.x;
        float yCoord1 = (float)y / height * scale + offsetY - player.deltaOffset.y;
        float zCoord1 = timer / fogSpeed;

        float xCoord2 = (float)x / width * scale * 2 + offsetX - player.deltaOffset.x;
        float yCoord2 = (float)y / height * scale * 2 + offsetY - player.deltaOffset.y;
        float zCoord2 = timer / fogSpeed * 0.8f;


        //float sample = Mathf.PerlinNoise(xCoord, yCoord);
        //float sampleTime = Mathf.PerlinNoise(0, zCoord);
        float sample = (float)simplex.Evaluate(xCoord1, yCoord1, zCoord1);
        //sample += (float)simplex.Evaluate(xCoord2, yCoord2, zCoord2);

        float sampleY;

        if (sample < 0f)
        {
            return new Color(0, 0, 0, 0);
        }
        else
        {
            sampleY = sample;
            return new Color(sampleY, sampleY, sampleY, 0.4f);
        }
    }
}
