using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public GameObject spawnPointsParent;
    public WorldAIManager worldAIManager;


    public void Respawn()
    {
        worldAIManager.DespawnAllCharacters();
        worldAIManager.SpawnAllCharacters();
    }

    // Find the closest respawn point
    public Vector3 FindClosestRespawnPointForPlayer(Vector3 playerPos)
    {
        // Get all the children game objects
        Transform[] children = spawnPointsParent.GetComponentsInChildren<Transform>();
        List<GameObject> childObjects = new List<GameObject>();

        foreach (Transform child in children)
        {
            // Exclude the parent
            if (child != spawnPointsParent.transform)
            {
                childObjects.Add(child.gameObject);
            }
        }

        // Initialize variables to track the closest point
        GameObject closestPoint = null;
        float closestDistance = Mathf.Infinity;

        // Position of the current GameObject
        Vector3 currentPosition = playerPos;

        // Loop through the child objects to find the closest one
        foreach (GameObject point in childObjects)
        {
            if (point != null)
            {
                float distance = Vector3.Distance(currentPosition, point.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = point;
                }
            }
        }

        return closestPoint.transform.position;
    }
}
