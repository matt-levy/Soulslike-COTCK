using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    public GameObject displaySettingsPanel;
    public GameObject audioSettingsPanel;
    public GameObject graphicsSettingsPanel;
    public GameObject controlSettingsPanel;

    // Display settings
    public Dropdown resolutionDropdown;
    public Dropdown displayModeDropdown;
    public Toggle vsyncToggle;
    public Slider frameRateLimitSlider;
    public Slider fovSlider;
    public Slider brightnessSlider;

    // Graphics settings
    public Dropdown graphicsQualityDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown antiAliasingDropdown;

    // Audio settings
    public AudioMixer audioMixer;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    // Control settings
    public Slider sensitivitySlider;

    // UI Text
    public Text frameRateLimitText;
    public Text fovText;
    public Text brightnessText;
    public Text masterVolumeText;
    public Text musicVolumeText;
    public Text sfxVolumeText;
    public Text sensitivityText;

    private PlayerSettings playerSettings = new PlayerSettings();

    void Start()
    {
        LoadPlayerSettings();
    }

    private void Update()
    {
        UpdateUIText();
    }

    private void UpdateUIText()
    {
        frameRateLimitText.text = frameRateLimitSlider.value.ToString();
        fovText.text = fovSlider.value.ToString();
        brightnessText.text = brightnessSlider.value.ToString();
        masterVolumeText.text = masterVolumeSlider.value.ToString();
        musicVolumeText.text = musicVolumeSlider.value.ToString();
        sfxVolumeText.text = sfxVolumeSlider.value.ToString();
        sensitivityText.text = sensitivitySlider.value.ToString();
    }

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

    // Resolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = GetResolutionFromDropdown(resolutionIndex);
        playerSettings.resolutionWidth = resolution.width;
        playerSettings.resolutionHeight = resolution.height;
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Display Mode
    public void SetDisplayMode(int displayModeIndex)
    {
        playerSettings.displayMode = (PlayerSettings.DisplayMode)displayModeIndex;
        Screen.fullScreenMode = GetFullScreenModeFromEnum(playerSettings.displayMode);
    }

    // VSync
    public void SetVSync(bool isVSyncOn)
    {
        playerSettings.vsync = isVSyncOn ? 1 : 0;
        QualitySettings.vSyncCount = playerSettings.vsync;
    }

    // Frame Rate Limit
    public void SetFrameRateLimit(float frameRateLimit)
    {
        playerSettings.frameRateLimit = Mathf.RoundToInt(frameRateLimit);
        Application.targetFrameRate = playerSettings.frameRateLimit == 0 ? -1 : playerSettings.frameRateLimit;
    }

    // FOV
    public void SetFOV(float fov)
    {
        playerSettings.fov = Mathf.RoundToInt(fov);
        Camera.main.fieldOfView = fov;
    }

    // Brightness
    public void SetBrightness(float brightness)
    {
        playerSettings.brightness = brightness;
        RenderSettings.ambientLight = Color.white * brightness;
    }

    // Graphics Quality
    public void SetGraphicsQuality(int qualityIndex)
    {
        playerSettings.graphicsQuality = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Texture Quality
    public void SetTextureQuality(int textureIndex)
    {
        playerSettings.textureQuality = textureIndex;

        switch (textureIndex)
        {
            // Low
            case 0:
                QualitySettings.globalTextureMipmapLimit = 0;
                break;
            // Medium
            case 1:
                QualitySettings.globalTextureMipmapLimit = 1;
                break;
            // High
            case 2:
                QualitySettings.globalTextureMipmapLimit = 2;
                break;
            default:
                QualitySettings.globalTextureMipmapLimit = 0;
                break;
        }
    }

    // Anti-Aliasing
    public void SetAntiAliasing(int aaIndex)
    {
        playerSettings.antiAliasing = (int)Mathf.Pow(2, aaIndex);
        QualitySettings.antiAliasing = playerSettings.antiAliasing;
    }

    // Master Volume
    public void SetMasterVolume(float volume)
    {
        int vol = Mathf.RoundToInt(volume);
        playerSettings.masterVolume = vol;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(vol) * 20);
    }

    // Music Volume
    public void SetMusicVolume(float volume)
    {
        int vol = Mathf.RoundToInt(volume);
        playerSettings.musicVolume = vol;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(vol) * 20);
    }

    // SFX Volume
    public void SetSFXVolume(float volume)
    {
        int vol = Mathf.RoundToInt(volume);
        playerSettings.sfxVolume = vol;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(vol) * 20);
    }

    // Sensitivity
    public void SetSensitivity(float sensitivity)
    {
        playerSettings.sensitivity = sensitivity;
    }

    public void ApplySettings()
    {
        SetResolution(resolutionDropdown.value);
        SetDisplayMode(displayModeDropdown.value);
        SetVSync(vsyncToggle.isOn);
        SetFrameRateLimit(frameRateLimitSlider.value);
        SetFOV(fovSlider.value);
        SetBrightness(brightnessSlider.value);
        SetGraphicsQuality(graphicsQualityDropdown.value);
        SetTextureQuality(textureQualityDropdown.value);
        SetAntiAliasing(antiAliasingDropdown.value);
        SetMasterVolume(masterVolumeSlider.value);
        SetMusicVolume(musicVolumeSlider.value);
        SetSFXVolume(sfxVolumeSlider.value);
        SetSensitivity(sensitivitySlider.value);
        SavePlayerSettings();
    }

    public void SavePlayerSettings()
    {
        SettingsSaveManager.SaveSettings(playerSettings);
    }

    public void LoadPlayerSettings()
    {
        SettingsSaveManager.LoadSettings(playerSettings);
        UpdateUIFromLoadedSettings();
    }

    private void UpdateUIFromLoadedSettings()
    {
        resolutionDropdown.value = GetResolutionIndex(playerSettings.resolutionWidth, playerSettings.resolutionHeight);
        displayModeDropdown.value = (int)playerSettings.displayMode;
        vsyncToggle.isOn = playerSettings.vsync == 1;
        frameRateLimitSlider.value = playerSettings.frameRateLimit;
        fovSlider.value = playerSettings.fov;
        brightnessSlider.value = playerSettings.brightness;
        graphicsQualityDropdown.value = playerSettings.graphicsQuality;
        textureQualityDropdown.value = playerSettings.textureQuality;
        antiAliasingDropdown.value = (int)(Mathf.Log(playerSettings.antiAliasing) / Mathf.Log(2));
        masterVolumeSlider.value = playerSettings.masterVolume;
        musicVolumeSlider.value = playerSettings.musicVolume;
        sfxVolumeSlider.value = playerSettings.sfxVolume;
        sensitivitySlider.value = playerSettings.sensitivity;
    }

    // Convert dropdown resolution index to resolution
    private Resolution GetResolutionFromDropdown(int resolutionIndex)
    {
        switch (resolutionIndex)
        {
            case 0: return new Resolution { width = 1280, height = 720 };
            case 1: return new Resolution { width = 1366, height = 768 };
            case 2: return new Resolution { width = 1920, height = 1080 };
            case 3: return new Resolution { width = 2560, height = 1440 };
            default: return Screen.currentResolution;
        }
    }

    private int GetResolutionIndex(int width, int height)
    {
        if (width == 1280 && height == 720) return 0;
        if (width == 1366 && height == 768) return 1;
        if (width == 1920 && height == 1080) return 2;
        if (width == 2560 && height == 1440) return 3;
        return 0;
    }

    // Convert dropdown display mode index to display mode
    private FullScreenMode GetDisplayModeFromDropdown(int displayModeIndex)
    {
        switch (displayModeIndex)
        {
            case 0: return FullScreenMode.FullScreenWindow;
            case 1: return FullScreenMode.Windowed;
            case 2: return FullScreenMode.MaximizedWindow;
            default: return Screen.fullScreenMode;
        }
    }

    // Convert DisplayMode enum to FullScreenMode
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
