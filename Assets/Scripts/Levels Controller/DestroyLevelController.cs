using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLevelController: MonoBehaviour
{
    public string parentName;
    void Update()
    {

        parentName = transform.name;
        StartCoroutine(DestroyClone());
    }
    IEnumerator DestroyClone()
    {
        yield return new WaitForSeconds(120);
        if (parentName == "SriLanka(Clone)" || parentName == "China(Clone)" || parentName == "Nepal(Clone)")
        {
            Destroy(gameObject);
        }

    }
}
