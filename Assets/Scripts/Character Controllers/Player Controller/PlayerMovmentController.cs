using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    PlayerAttributes playerAttributes;
    CoinAttribute coinAttribute;
    Rigidbody playerRigidbody;

    float laneWidth = 4.5f;  // Distance to move left or right
    float targetX = 0f;      // Target X position for lane switching

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50f; // Adjust for swipe sensitivity

    void Start()
    {
        targetX = transform.position.x;
        playerRigidbody = GetComponent<Rigidbody>();

        playerAttributes = GameManager.Instance.dataManager.playerAttributes;
        coinAttribute = GameManager.Instance.dataManager.coinAttribute;
    }

    void Update()
    {
        CharacterController();
        coinAttribute.totalCoins = PlayerPrefs.GetInt("Total_Coins", 0);
    }

    #region Player Movements
    void CharacterController()
    {
        // Move forward constantly if player is alive
        if (!playerAttributes.IsPlayerDead)
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * playerAttributes.PlayerDefSpeed), Space.World);

            // Detect touch input for swiping
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Handle swiping for lane changes
                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    endTouchPosition = touch.position;
                    Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                    // Horizontal swipe detection
                    if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y) && Mathf.Abs(swipeDelta.x) > swipeThreshold)
                    {
                        // Swipe left
                        if (swipeDelta.x < 0)
                        {
                            targetX -= laneWidth;
                        }
                        // Swipe right
                        else if (swipeDelta.x > 0)
                        {
                            targetX += laneWidth;
                        }

                        // Clamp targetX to stay within lane boundaries
                        targetX = Mathf.Clamp(targetX, -laneWidth, laneWidth);
                    }
                    // Vertical swipe for jump
                    else if (swipeDelta.y > swipeThreshold && playerAttributes.IsPlayerOnGround)
                    {
                        Jump();
                    }
                }
            }

            // Smoothly move the player to the target X position
            Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * playerAttributes.PlayerTurnSpeed);
        }
    }

    void Jump()
    {
        if (playerAttributes.IsPlayerOnGround)
        {
            playerAttributes.IsPlayerOnGround = false;
            playerRigidbody.AddForce(Vector3.up * playerAttributes.PlayerJumpFores, ForceMode.Impulse);
        }
    }
    #endregion

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerAttributes.IsPlayerOnGround = true;
        }

        #region Is Player Dead
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            playerAttributes.IsPlayerDead = true;
            // Trigger player death animation here
        }
        #endregion
    }

    void OnTriggerEnter(Collider other)
    {
        #region Coin Collect
        if (other.CompareTag("Coin"))
        {
            coinAttribute.coinOnRun++;
            coinAttribute.totalCoins++;

            PlayerPrefs.SetInt("Total_Coins", coinAttribute.totalCoins);
            PlayerPrefs.Save();

            Debug.Log("Coin collected! : " + coinAttribute.coinOnRun);
            Debug.Log($"Total Coins: {coinAttribute.totalCoins}");
            other.gameObject.SetActive(false);  // Disable the coin object
        }
        #endregion
    }
}
