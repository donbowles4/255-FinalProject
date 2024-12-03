using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Terrain terrain; 
    public GameObject targetObject; 
    public int detailPrototypeIndex = 0; 
    public int grassDensity = 10; 
    public float placementRadius = 5f; 

    private void Start()
    {
        //this is what I am using for exceptions. its not the usual try catch but I think it counts
        if (terrain == null)
        {
            Debug.LogError("Terrain is not assigned in the GameManager!");
        }
        if (targetObject == null)
        {
            Debug.LogError("Target object is not assigned in the GameManager!");
        }
    }

    public void PlaceGrass()
    {
        if (terrain == null || targetObject == null) return;

        //get terrain data
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;
        Vector3 terrainPosition = terrain.transform.position;

        //get the target object's position
        Vector3 targetPosition = targetObject.transform.position;

        // Randomly generate grass within the placement radius
        for (int i = 0; i < grassDensity; i++)
        {
            // Random offsets within the radius
            float randomX = Random.Range(-placementRadius, placementRadius);
            float randomZ = Random.Range(-placementRadius, placementRadius);

            // Calculate the position for grass placement
            Vector3 position = new Vector3(
                targetPosition.x + randomX,
                targetPosition.y,
                targetPosition.z + randomZ
            );

            //data validation because it kept breaking
            if (position.x >= terrainPosition.x && position.x <= terrainPosition.x + terrainSize.x &&
                position.z >= terrainPosition.z && position.z <= terrainPosition.z + terrainSize.z)
            {
                //convert world position to detail map coords
                int detailX = Mathf.FloorToInt((position.x - terrainPosition.x) / terrainSize.x * terrainData.detailResolution);
                int detailZ = Mathf.FloorToInt((position.z - terrainPosition.z) / terrainSize.z * terrainData.detailResolution);

                //place grass if ur allowewd to
                if (detailX >= 0 && detailX < terrainData.detailWidth && detailZ >= 0 && detailZ < terrainData.detailHeight)
                {
                    //get detail layer/modify
                    int[,] detailLayer = terrainData.GetDetailLayer(0, 0, terrainData.detailWidth, terrainData.detailHeight, detailPrototypeIndex);
                    detailLayer[detailZ, detailX] += 1; 
                    terrainData.SetDetailLayer(0, 0, detailPrototypeIndex, detailLayer);
                }
            }
        }

        Debug.Log("Grass placed around the target object.");
    }
}
