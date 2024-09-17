using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class InventorySaveManager
{
    private static string savePath = Application.persistentDataPath + "/playerInventory.json";

    public static void SaveInventory(PlayerInventoryManager inventoryManager)
    {
        // Convert to JSON
        string json = JsonUtility.ToJson(inventoryManager, true);

        // Write to file
        File.WriteAllText(savePath, json);
        Debug.Log("Player inventory saved");
    }

    public static void LoadInventory(PlayerInventoryManager inventoryManager)
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            // Overwrite inventoryManager's data
            JsonUtility.FromJsonOverwrite(json, inventoryManager);
            Debug.Log("Player inventory loaded");
        }
        else
        {
            Debug.LogWarning("Player inventory file not found");
        }
    }
}
