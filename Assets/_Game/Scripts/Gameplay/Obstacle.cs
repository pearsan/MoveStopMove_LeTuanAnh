using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    [SerializeField] private Material transparentMaterial; // Assign the transparent material in the Inspector
    [SerializeField] private Material originalMaterial; // To store the original material
    [SerializeField] private List<Renderer> obstacleRenderer; // Reference to the Renderer component

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Ultilities.PLAYER)) // Check if the colliding object is the player
        {
            // Change material for each LOD renderer
            foreach (var renderer in obstacleRenderer)
            {
                renderer.material = transparentMaterial; // Change to transparent material
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Ultilities.PLAYER)) // Check if the player exits the trigger
        {
            // Restore original material for each LOD renderer
            foreach (var renderer in obstacleRenderer)
            {
                renderer.material = originalMaterial; // Restore original material
            }
        }
    }
}