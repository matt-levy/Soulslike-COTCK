using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldAIManager : MonoBehaviour
{
    public static WorldAIManager instance;

    [Header("DEBUG")]
    [SerializeField] bool despawnCharacters = false;
    [SerializeField] bool spawnCharacters = false;

    [Header("Characters")]
    [SerializeField] private GameObject[] aiCharacters;
    [SerializeField] private List<GameObject> spawnedInCharacters;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(WaitForSceneToLoadThenSpawnCharacters());
    }

    private void Update()
    {
        if (despawnCharacters)
        {
            despawnCharacters = false;
            DespawnAllCharacters();
        }

        if (spawnCharacters)
        {
            spawnCharacters = false;
            SpawnAllCharacters();
        }
    }

    private IEnumerator WaitForSceneToLoadThenSpawnCharacters()
    {
        while (!SceneManager.GetActiveScene().isLoaded)
        {
            yield return null;
        }

        SpawnAllCharacters();
    }


    public void SpawnAllCharacters()
    {
        foreach (var character in aiCharacters)
        {
            GameObject instantiatedCharacter = Instantiate(character);
            spawnedInCharacters.Add(instantiatedCharacter);
        }
    }

    public void DespawnAllCharacters()
    {
        foreach (var character in spawnedInCharacters)
        {
            Destroy(character);
        }
    }

    private void DisableAllCharacters()
    {
        // TO DO disable character gameobjects
        // Can be used to disable characters that are far from players to save memory
        // Characters can be split into areas (AREA_00, AREA_01, etc.)
    }
}
