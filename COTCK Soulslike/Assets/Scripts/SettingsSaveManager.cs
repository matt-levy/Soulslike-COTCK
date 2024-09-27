using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SettingsSaveManager
{
    private static string savePath = Application.persistentDataPath + "/playerSettings.json";

    public static void SaveSettings(PlayerSettings playerSettings)
    {
        // Convert PlayerSettings to JSON
        string json = JsonUtility.ToJson(playerSettings, true);

        // Write the JSON string to the file
        File.WriteAllText(savePath, json);
        Debug.Log("Player settings saved");
    }

    public static void LoadSettings(PlayerSettings playerSettings)
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            // Overwrite playerSettings data
            JsonUtility.FromJsonOverwrite(json, playerSettings);
            Debug.Log("Player settings loaded");
        }
        else
        {
            Debug.LogWarning("Player settings file not found");
            playerSettings.ResetToDefaults();
        }
    }
}
