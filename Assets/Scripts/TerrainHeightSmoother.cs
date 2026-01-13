using UnityEngine;

[ExecuteInEditMode]
public class TerrainHeightSmoother : MonoBehaviour
{
    [Range(1, 10)] public int smoothIterations = 3;
    [Range(3, 9)] public int kernelSize = 5;

    [ContextMenu("Smooth Terrain")]
    public void SmoothTerrain()
    {
        Terrain terrain = GetComponent<Terrain>();
        if (terrain == null || terrain.terrainData == null) return;

        TerrainData tData = terrain.terrainData;
        int w = tData.heightmapResolution;
        int h = tData.heightmapResolution;

        float[,] heights = tData.GetHeights(0, 0, w, h);

        for (int iter = 0; iter < smoothIterations; iter++)
        {
            float[,] smoothed = new float[w, h];

            for (int y = 1; y < h - 1; y++)
            {
                for (int x = 1; x < w - 1; x++)
                {
                    float avg = 0f;
                    for (int oy = -1; oy <= 1; oy++)
                        for (int ox = -1; ox <= 1; ox++)
                            avg += heights[y + oy, x + ox];
                    smoothed[y, x] = avg / 9f;
                }
            }

            heights = smoothed;
        }

        tData.SetHeights(0, 0, heights);
        Debug.Log($"Terrain smoothed with {smoothIterations} iterations.");
    }
}