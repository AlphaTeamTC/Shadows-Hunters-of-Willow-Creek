using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxMaterials;  // Assign each Skybox material in the Inspector
    private int currentSkyboxIndex = 0;

    void OnEnable()
    {
        if (skyboxMaterials.Length > 0)
        {
            // Apply the current skybox material
            RenderSettings.skybox = skyboxMaterials[currentSkyboxIndex];

            // Update to the next skybox material for future activations
            currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxMaterials.Length;

            // Update lighting to reflect the new skybox
            DynamicGI.UpdateEnvironment();
        }
        else
        {
            Debug.LogWarning("No skybox materials assigned.");
        }
    }
}