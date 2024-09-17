using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDeadPanel : MonoBehaviour
{
    public GameObject playerDeadPanel;
    TMP_Text scoreDisplay;
    TMP_Text coinDisplay;

    PlayerMovmentController playerMovementController;
    ScoreController scoreController;

    void Start()
    {
      
                //? Code refactored
        //* Using GameObject.Find with object name
        FindAndAssign(ref scoreDisplay, "Score_Text");
        FindAndAssign(ref coinDisplay, "coin-Text");
        FindAndAssign(ref playerDeadPanel, "PlayerDead_Obj");

        //* Using GameObject.FindObjectOfType without object name
        FindAndAssign(ref playerMovementController);
        FindAndAssign(ref scoreController);
    }

    private T FindAndAssign<T>(ref T field, string objectName = null) where T : UnityEngine.Object
    {
        if (field == null)
        {
            if (!string.IsNullOrEmpty(objectName))
            {
                field = GameObject.Find(objectName)?.GetComponent<T>();
            }
            else
            {
                field = GameObject.FindObjectOfType<T>();
            }
        }

        return field;
    }


    void Update()
    {
        //TODO before set the panel check if the player dead animation is Over
        if (playerMovementController.playerAttributes.isPlayerDead)
        {
            playerDeadPanel.SetActive(true);

            LeanTween.scale(playerDeadPanel, new Vector3(1, 1, 1), 0.5f);

            coinDisplay.text = playerMovementController.coinAttribute.totalCoins.ToString("0");
            scoreDisplay.text = scoreController.scoreAttributes.score.ToString("0");

            //TODO playerDeadPanel need to scale down after this panel functions are over
        }
    }

}
