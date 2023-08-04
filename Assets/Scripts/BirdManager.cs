using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BirdManager : MonoBehaviour
{
    public GameObject birdPrefab;  // Drag your bird prefab here in the Inspector


    public void SpawnBird(Vector3 position, Transform oldBirdTransform) 
    {
        // Instantiate the new bird
        GameObject newBird = Instantiate(birdPrefab, position, Quaternion.identity);

        
    }
}
