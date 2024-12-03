using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassDestroyer : MonoBehaviour
{
    public Terrain terrain; //get terrain object
    public float radius = 1f; //mowing area radius
    public GameObject playerVehicle;
    
    void Update()
    {
        Vector3 collisionPoint = playerVehicle.transform.position;
        RemoveGrassAroundPoint(collisionPoint);
    }

    void RemoveGrassAroundPoint(Vector3 point)
    {
        //positioning world to local
        Vector3 terrainPosition = point - terrain.transform.position;

        //stupid normalization
        TerrainData terrainData = terrain.terrainData;
        int detailResolution = terrainData.detailResolution;
        float relativeX = terrainPosition.x / terrainData.size.x;
        float relativeZ = terrainPosition.z / terrainData.size.z;

        //get grass details
        int detailX = Mathf.Clamp((int)(relativeX * detailResolution), 0, detailResolution - 1);
        int detailZ = Mathf.Clamp((int)(relativeZ * detailResolution), 0, detailResolution - 1);

        //get radius of grass
        int radiusInDetails = Mathf.RoundToInt(radius * detailResolution / terrainData.size.x);
        int xStart = Mathf.Max(0, detailX - radiusInDetails);
        int zStart = Mathf.Max(0, detailZ - radiusInDetails);
        int xEnd = Mathf.Min(detailResolution, detailX + radiusInDetails);
        int zEnd = Mathf.Min(detailResolution, detailZ + radiusInDetails);

        //this gets rid of the grass, not entirely sure how, but it works and thats all that matters
        for (int layer = 0; layer < terrainData.detailPrototypes.Length; layer++)
        {
            int[,] details = terrainData.GetDetailLayer(xStart, zStart, xEnd - xStart, zEnd - zStart, layer);

            for (int x = 0; x < details.GetLength(0); x++)
            {
                for (int z = 0; z < details.GetLength(1); z++)
                {
                    details[x, z] = 0;
                }
            }

            terrainData.SetDetailLayer(xStart, zStart, layer, details);
        }
    }
}
