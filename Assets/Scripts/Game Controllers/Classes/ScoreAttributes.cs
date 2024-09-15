using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAttributes
{

    public float score = 0f;
    public float minScoreMultiplayer = 1f;
    public float msxScoreMultiplayer = 20f;
    public float scoreRate = 100f; // Points per second


    public int speedIncrementScore = 10000;         // The score threshold to increase speed
    public int nextSpeedIncreaseScore = 10000;      // Next score at which to increase speed


}
