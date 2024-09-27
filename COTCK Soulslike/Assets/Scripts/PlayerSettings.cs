using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public enum DisplayMode
    {
        Fullscreen,
        Windowed,
        BorderlessWindowed
    }

    public int resolutionWidth;
    public int resolutionHeight;
    public DisplayMode displayMode;
    public int vsync;
    public int frameRateLimit;
    public int fov;
    public float brightness;

    public int graphicsQuality;
    public int textureQuality;
    public int antiAliasing;

    public int masterVolume;
    public int musicVolume;
    public int sfxVolume;

    public float sensitivity;

    public void ResetToDefaults()
    {
        resolutionWidth = 1920;
        resolutionHeight = 1080;
        displayMode = DisplayMode.Fullscreen;
        vsync = 1;
        frameRateLimit = 60;
        fov = 60;
        brightness = .5f;
        graphicsQuality = 2;
        textureQuality = 2;
        antiAliasing = 2;
        masterVolume = 100;
        musicVolume = 100;
        sfxVolume = 100;
        sensitivity = 12.0f;
    }

}
