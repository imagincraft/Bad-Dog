using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovmentController : MonoBehaviour
{
    public PlayerAttributes playerAttributes = new PlayerAttributes();
    
     float laneWidth = 4.5f;     //? Distance to move left or right
     float targetX;            //? Target X position for lane switching


    void Start()
    {
        targetX = transform.position.x;

    }

    void Update()
    {
        // * Control of the character ( Run / Jump / Left / Right)
        CharacterController();

    }

#region Player Movements
    void CharacterController()
    {

        //* Move forward constantly
        transform.Translate(Vector3.forward * Time.deltaTime * playerAttributes.playerDefSpeed, Space.World);

        //* Move to the left (A or LeftArrow)
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("A Pressed");
            if (targetX > -laneWidth)  // Ensure not moving beyond the left lane boundary
            {
                targetX -= laneWidth;  // Move left by laneWidth (4.5 units)
                                       //TODO : Set the left jump animation
            }
        }

        //* Move to the right (D or RightArrow)
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("D Pressed");
            if (targetX < laneWidth)  // Ensure not moving beyond the right lane boundary
            {
                targetX += laneWidth;  // Move right by laneWidth (4.5 units)
                                       //TODO : Set the right jump animation
            }
        }

        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * playerAttributes.playerTurnSpeed);



        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            if (playerAttributes.isPlayerOnGround)
            {
                playerAttributes.isPlayerOnGround = false;
                // Store the current position
                Vector3 currentPos = transform.position;

                // Jump 6 units up, and then return back to the original position
                LeanTween.moveY(this.gameObject, currentPos.y + 6f, 0.5f).setEaseOutQuad().setOnComplete(() =>
                {
                    // Once the jump is completed, move the player back down
                    LeanTween.moveY(this.gameObject, currentPos.y, 0.5f).setEaseInQuad();

                    //TODO : Set the jump animation
                });
            }
        }

      /* //* Pending decision
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) )
        {
            if (!playerAttributes.isPlayerOnGround)
            {
                
                // Store the current position
                Vector3 currentPos = transform.position;

                // Jump 6 units up, and then return back to the original position
                LeanTween.moveY(this.gameObject, currentPos.y, 0.5f).setEaseInQuad().setOnComplete(() =>
                {
                    // Once the jump is completed, move the player back down
                    // LeanTween.moveY(this.gameObject, currentPos.y, 0.5f).setEaseInQuad();

                    //TODO : Set the jump animation
                });
            }
        } */

    }
    #endregion

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerAttributes.isPlayerOnGround = true;
        }
    }


}