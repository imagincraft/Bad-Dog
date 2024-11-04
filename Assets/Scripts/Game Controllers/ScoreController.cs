using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    TMP_Text scoreDisplay;
    // PlayerMovmentController playerMovement;  //* Reference to the PlayerMovementController script
    PlayerAttributes playerAttributes;
    public ScoreAttributes scoreAttributes;
    //* public ScoreAttributes scoreAttributes = new ScoreAttributes();


    void Start()
    {

        if (scoreDisplay == null)
        {

            scoreDisplay = GameObject.Find("PlayerScore_Text_TMP").GetComponent<TMP_Text>();

        }


        //? Find and assign the PlayerMovementController script if not set
        /*   if (playerMovement == null)
          {
              playerMovement = GameObject.FindObjectOfType<PlayerMovmentController>();
          } */

        playerAttributes = GameManager.Instance.dataManager.playerAttributes;
        scoreAttributes = GameManager.Instance.dataManager.scoreAttributes;
    }


    void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        // score = playerMovement.gameObject.transform.position.z * distanceMultiplier;
        if (!playerAttributes.isPlayerDead)
        {

            scoreAttributes.score += Time.deltaTime * scoreAttributes.scoreRate * scoreAttributes.minScoreMultiplayer;
        }

        scoreDisplay.text = scoreAttributes.score.ToString("0");


        // Check if the score has crossed the threshold for the next speed increment
        if (scoreAttributes.score >= scoreAttributes.nextSpeedIncreaseScore)
        {
            IncreasePlayerSpeed();
        }
    }




    //* Function to increase the player's speed and set the next threshold
    private void IncreasePlayerSpeed()
    {
        if (playerAttributes.playerDefSpeed != playerAttributes.playerMaxSpeed)
        {
            playerAttributes.playerDefSpeed += 2;                                    // Increase player speed by 1
            scoreAttributes.nextSpeedIncreaseScore += scoreAttributes.speedIncrementScore;  // Set the next score threshold for the speed increase
            Debug.Log($"Speed increased! Current speed: {playerAttributes.playerDefSpeed}, Next speed increase at: {scoreAttributes.nextSpeedIncreaseScore}");
        }
    }
}
