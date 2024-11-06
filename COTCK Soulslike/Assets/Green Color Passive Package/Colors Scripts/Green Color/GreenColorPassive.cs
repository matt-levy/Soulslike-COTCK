using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Color Passives/Green Color Passive")]
public class GreenColorPassive : ColorPassive
{
    public GameObject plantAllyPrefab;
    public float summonDistance = 2f;

    // Additional properties can be added here (e.g., damage, attack range, etc.)

    public void SummonPlantAlly(Transform playerTransform)
    {
        if (plantAllyPrefab != null)
        {
            GameObject ally = Instantiate(plantAllyPrefab, playerTransform.position + playerTransform.forward * summonDistance, Quaternion.identity);
            PlantAllyManager plantAllyManager = ally.GetComponent<PlantAllyManager>();
            plantAllyManager.playerTransform = playerTransform;
            // You might want to add additional setup here (e.g., setting up references)
        }
    }
}

