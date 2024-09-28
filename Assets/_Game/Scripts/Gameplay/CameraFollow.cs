using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float smoothSpeed = 0.125f; // Smoothing speed
    public Vector3 offset; // Offset position from player

    void FixedUpdate()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // // Optionally, you can make the camera look at the player
        // transform.LookAt(player);
        //
        // // Get the current rotation
        // Vector3 currentRotation = transform.rotation.eulerAngles;
        //
        // // Set Y and Z rotations to 0, keeping the X rotation
        // transform.rotation = Quaternion.Euler(currentRotation.x, 0f, 0f);
    }
}
