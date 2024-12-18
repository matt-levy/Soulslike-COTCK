using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq.Expressions;

public class SaveFileDataWriter
{
    public string saveDataDirectoryPath = "";
    public string saveFileName = "";

    // Before creating a new save file, check if it already exists
    // Max 10 character slots
    public bool CheckIfFileExists()
    {
        if (File.Exists(Path.Combine(saveDataDirectoryPath, saveFileName)))
        {
            return true;
        }
        return false;
    }

    // Used to delete character save files
    public void DeleteSaveFile()
    {
        File.Delete(Path.Combine(saveDataDirectoryPath, saveFileName));
    }

    // Used to create a save file upon starting a new game
    public void CreateNewCharacterSaveFile(CharacterSaveData characterData)
    {
        // Make a path to the file (a location on your machine)
        string savePath = Path.Combine(saveDataDirectoryPath, saveFileName);

        try
        {
            // Create directory the file will be written to, if it DNE
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            Debug.Log("Creating Save File at save path " + savePath);

            // Serialize C# game data object to json format
            string dataToStore = JsonUtility.ToJson(characterData, true);

            // Write file to our system
            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                using (StreamWriter fileWriter = new StreamWriter(stream))
                {
                    fileWriter.Write(dataToStore);
                }
            }
        }
        catch(Exception ex)
        {
            Debug.LogError("Error whilst trying to save character data, game not saved " + savePath + "\n" + ex);
        }
    }

    // Used to load a save file upon loading a previous save
    public CharacterSaveData LoadSaveFile()
    {
        CharacterSaveData characterData = null;

        string loadPath = Path.Combine(saveDataDirectoryPath, saveFileName);

        if (File.Exists(loadPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
            }

            // Deserialize data from json to unity
            characterData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error reading save data\n" + ex);
            }
        }

        return characterData;
    }
}
