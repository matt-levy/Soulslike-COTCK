using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementManager : MonoBehaviour
{
    private Transform playerTransform;
    private NPCManager npcManager;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        npcManager = GetComponent<NPCManager>();

    }

    private void Update()
    {
        // If dialogue is active, face the player
        if (npcManager.dialogueManager != null && npcManager.dialogueManager.IsDialogueActive())
        {
            FacePlayer();
        }
    }

    private void FacePlayer()
    {
        if (playerTransform == null) return;

        // Calculate the direction to look at the player
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0;

        // Rotate NPC to face the player
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
