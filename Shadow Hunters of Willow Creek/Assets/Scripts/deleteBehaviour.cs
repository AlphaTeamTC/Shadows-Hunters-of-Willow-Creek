using UnityEngine;
using UnityEditor;

public class deleteBehaviour : EditorWindow
{
    [MenuItem("Tools/Delete All Hitboxes")]
    public static void DeleteHitboxes()
    {
        // Get all GameObjects in the scene
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        // Iterate through each object and delete any Collider component found
        int deletedCount = 0;
        foreach (GameObject obj in allObjects)
        {
            Collider[] colliders = obj.GetComponents<Collider>();
            foreach (Collider collider in colliders)
            {
                DestroyImmediate(collider);
                deletedCount++;
            }
        }

        // Log the number of colliders deleted
        Debug.Log($"Deleted {deletedCount} hitbox(es) from the scene.");
    }
}