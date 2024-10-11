using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
     Transform player;       // The player's transform
     Vector3 offset;         // Offset to maintain from the player
     float smoothSpeed = 0.125f; // How smooth the camera should follow

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform; // Assign player by tag if not set
        }

        // Optional: Initial offset based on current camera and player positions
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Keep the camera's X and Z axis following the player, but lock the Y-axis
        Vector3 newPos = new Vector3(player.position.x + offset.x, transform.position.y, player.position.z + offset.z);
        
        // Update the camera position
        transform.position = newPos;
    }
}
