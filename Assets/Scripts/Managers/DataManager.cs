using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public PlayerAttributes playerAttributes;
    public CoinAttribute coinAttribute;
    public ScoreAttributes scoreAttributes;

    void Start()
    {
        playerAttributes = new PlayerAttributes();
        coinAttribute = new CoinAttribute();
        scoreAttributes = new ScoreAttributes();
    }


    void Update()
    {

    }
}
