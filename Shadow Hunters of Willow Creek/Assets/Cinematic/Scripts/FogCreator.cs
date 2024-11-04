using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCreator : MonoBehaviour
{
    public Color fogColor = Color.gray;
    public float fogDensity = 0.02f;
    public bool enableFog = true;

    void Start()
    {
        // Enable or disable fog
        RenderSettings.fog = enableFog;
        
        if (enableFog)
        {
            // Set fog mode and properties
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogDensity;
        }
    }

    // Optional method to change fog properties during runtime
    public void UpdateFogProperties(Color newColor, float newDensity)
    {
        RenderSettings.fogColor = newColor;
        RenderSettings.fogDensity = newDensity;
    }
}
