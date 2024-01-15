using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectFunction : MonoBehaviour
{
   public void ChangeObjectMaterial(GameObject target)
    {
        // Access the Renderer component attached to the GameObject
        Renderer renderer = target.GetComponent<Renderer>();

        // Check if the renderer component exists
        if (renderer != null)
        {
            Material myMaterial = renderer.material;
            // Change the material to the newMaterial
            myMaterial.color = Color.red;
        }
        else
        {
            Debug.LogError("Renderer component not found on the GameObject.");
        }
    }
}
