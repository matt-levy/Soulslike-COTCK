using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUIManager : MonoBehaviour
{
    // Static instance of InteractionUIManager for global access
    public static InteractionUIManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        Hide();
    }

    public static void Show()
    {
        if (Instance != null)
        {
            Instance.gameObject.SetActive(true);
        }
    }

    public static void Hide()
    {
        if (Instance != null)
        {
            Instance.gameObject.SetActive(false);
        }
    }
}
