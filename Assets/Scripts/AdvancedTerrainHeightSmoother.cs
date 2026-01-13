using UnityEngine;

[ExecuteInEditMode]
public class AdvancedTerrainHeightSmoother : MonoBehaviour
{
    [Range(1, 10)] public int smoothIterations = 6;
    [Range(3, 11)] public int kernelSize = 7;
    [Range(0.01f, 1f)] public float heightSensitivity = 0.15f;

    [ContextMenu("Smooth Terrain - Advanced")]
    public void SmoothTerrainAdvanced()
    {
        Terrain terrain = GetComponent<Terrain>();
        if (terrain == null || terrain.terrainData == null)
        {
            Debug.LogWarning("AdvancedTerrainHeightSmoother: No Terrain component found.");
            return;
        }

        TerrainData tData = terrain.terrainData;
        int width = tData.heightmapResolution;
        int height = tData.heightmapResolution;
        float[,] heights = tData.GetHeights(0, 0, width, height);

        int halfKernel = kernelSize / 2;

        for (int iter = 0; iter < smoothIterations; iter++)
        {
            float[,] newHeights = new float[width, height];

            for (int y = halfKernel; y < height - halfKernel; y++)
            {
                for (int x = halfKernel; x < width - halfKernel; x++)
                {
                    float center = heights[y, x];
                    float sum = 0f;
                    float weightSum = 0f;

                    for (int oy = -halfKernel; oy <= halfKernel; oy++)
                    {
                        for (int ox = -halfKernel; ox <= halfKernel; ox++)
                        {
                            float sample = heights[y + oy, x + ox];
                            float dist2 = ox * ox + oy * oy;
                            float spatialWeight = Mathf.Exp(-dist2 * 0.15f);
                            float heightDiff = sample - center;
                            float heightWeight = Mathf.Exp(-Mathf.Pow(heightDiff, 2) / (heightSensitivity * heightSensitivity));
                            float totalWeight = spatialWeight * heightWeight;

                            sum += sample * totalWeight;
                            weightSum += totalWeight;
                        }
                    }

                    newHeights[y, x] = sum / weightSum;
                }
            }

            heights = newHeights;
        }

        tData.SetHeights(0, 0, heights);
        Debug.Log($"Advanced terrain smoothing applied: kernel {kernelSize}, iterations {smoothIterations}, sensitivity {heightSensitivity}");
    }
}