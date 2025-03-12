using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject playerGameObject; // Drag the player GameObject here in the Inspector
    public float yOffset = 5f; // Offset above the player's Y position
    public float maxY = 50f;
    public float minX = -30f;
    public float maxX = 30f;
    public int maxPlatformsPerLevel = 5;
    public float minDistanceBetweenPlatforms = 2f; // Minimum distance between platforms
    public float distanceBetweenPlatforms = 5f; // Distance between platforms, default is 5 units

    private void Start()
    {
        // Check if playerGameObject is assigned
        if (playerGameObject == null)
        {
            Debug.LogError("Player GameObject is not assigned!");
            return;
        }

        // Get the Y coordinate of the player GameObject
        float playerY = playerGameObject.transform.position.y;

        // Start spawning platforms above the player's Y position
        SpawnPlatforms(playerY + yOffset);
    }

    void SpawnPlatforms(float startY)
    {
        float currentY = startY;
        while (currentY < maxY)
        {
            // List to store spawned platform positions
            List<Vector2> spawnedPositions = new List<Vector2>();

            for (int i = 0; i < maxPlatformsPerLevel; i++)
            {
                // Randomly generate position for the platform within the specified range
                Vector2 spawnPosition = GetRandomPosition(currentY, spawnedPositions);

                // Spawn platform
                Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

                // Add spawned position to the list
                spawnedPositions.Add(spawnPosition);
            }
            currentY += distanceBetweenPlatforms; // Increment Y position by the chosen distance
        }
    }

    Vector2 GetRandomPosition(float currentY, List<Vector2> existingPositions)
    {
        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), currentY);

        // Check if the new position is too close to existing positions
        foreach (Vector2 existingPosition in existingPositions)
        {
            if (Vector2.Distance(spawnPosition, existingPosition) < minDistanceBetweenPlatforms)
            {
                // If too close, adjust the position
                return GetRandomPosition(currentY, existingPositions);
            }
        }

        return spawnPosition;
    }
}

