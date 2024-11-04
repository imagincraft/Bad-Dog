using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    CoinAttribute coinAttribute;
    // CoinAttribute coinAttribute = new CoinAttribute();
    // public GameObject coinPref;

    void Start()
    {

        coinAttribute = GameManager.Instance.dataManager.coinAttribute;

    }


    void Update()
    {

        // LeanTween.rotate(this.gameObject,Vector3.up,2f);

        // this.gameObject.transform.rotation

    }


}
