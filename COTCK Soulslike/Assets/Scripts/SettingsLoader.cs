using UnityEngine;
using UnityEngine.Audio;

public class SettingsLoader : MonoBehaviour
{
    private PlayerSettings playerSettings = new PlayerSettings();
    public AudioMixer audioMixer;

    void Start()
    {
        LoadAndApplySettings();
    }

    private void LoadAndApplySettings()
    {
        SettingsSaveManager.LoadSettings(playerSettings);
        ApplySettings();
    }

    private void ApplySettings()
    {
        Screen.SetResolution(playerSettings.resolutionWidth, playerSettings.resolutionHeight, GetFullScreenModeFromEnum(playerSettings.displayMode));
        QualitySettings.vSyncCount = playerSettings.vsync;

        // Set frame rate limit
        Application.targetFrameRate = playerSettings.frameRateLimit;

        if (Camera.main != null)
        {
            // Set FOV
            Camera.main.fieldOfView = playerSettings.fov;
        }

        // Set brightness
        RenderSettings.ambientLight = Color.white * playerSettings.brightness;

        // Set graphics quality
        QualitySettings.SetQualityLevel(playerSettings.graphicsQuality);

        // Set texture quality
        QualitySettings.globalTextureMipmapLimit = playerSettings.textureQuality;

        QualitySettings.antiAliasing = playerSettings.antiAliasing;

        // Set audio volumes
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(playerSettings.masterVolume / 100f) * 20);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(playerSettings.musicVolume / 100f) * 20);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(playerSettings.sfxVolume / 100f) * 20);

        // TODO Set Mouse Sensitivity
    }

    private FullScreenMode GetFullScreenModeFromEnum(PlayerSettings.DisplayMode mode)
    {
        switch (mode)
        {
            case PlayerSettings.DisplayMode.Fullscreen:
                return FullScreenMode.FullScreenWindow;
            case PlayerSettings.DisplayMode.Windowed:
                return FullScreenMode.Windowed;
            case PlayerSettings.DisplayMode.BorderlessWindowed:
                return FullScreenMode.MaximizedWindow;
            default:
                return FullScreenMode.FullScreenWindow;
        }
    }
}
