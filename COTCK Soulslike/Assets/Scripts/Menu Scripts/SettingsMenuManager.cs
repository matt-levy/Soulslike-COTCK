using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuManager : MonoBehaviour
{
    public GameObject displaySettingsPanel;
    public GameObject audioSettingsPanel;
    public GameObject graphicsSettingsPanel;
    public GameObject controlSettingsPanel;

    public void ShowDisplaySettings()
    {
        displaySettingsPanel.SetActive(true);
        audioSettingsPanel.SetActive(false);
        graphicsSettingsPanel.SetActive(false);
        controlSettingsPanel.SetActive(false);
    }

    public void ShowGraphicsSettings()
    {
        displaySettingsPanel.SetActive(false);
        audioSettingsPanel.SetActive(false);
        graphicsSettingsPanel.SetActive(true);
        controlSettingsPanel.SetActive(false);
    }

    public void ShowAudioSettings()
    {
        displaySettingsPanel.SetActive(false);
        audioSettingsPanel.SetActive(true);
        graphicsSettingsPanel.SetActive(false);
        controlSettingsPanel.SetActive(false);
    }

    public void ShowControlSettings()
    {
        displaySettingsPanel.SetActive(false);
        audioSettingsPanel.SetActive(false);
        graphicsSettingsPanel.SetActive(false);
        controlSettingsPanel.SetActive(true);
    }
}
