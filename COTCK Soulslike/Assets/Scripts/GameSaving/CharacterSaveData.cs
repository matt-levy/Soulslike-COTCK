using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// Since we want to reference this data for every file, this script is not
// a monobehavior, but instead serializable
public class CharacterSaveData
{
    [Header("Character Name")]
    public string characterName = "Character";

    [Header("Time Played")]
    public float secondsPlayed;

    // You can only serialize basic data types, so no Vector3
    [Header("World Coordinates")]
    public float xPos;
    public float yPos;
    public float zPos;

    [Header("Stats")]
    public int vitality;
    public int endurance;
    public int currentHealth;
    public int currentStamina;
}
