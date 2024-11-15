using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrop : MonoBehaviour
{
    [SerializeField] GameObject branch;

    private Rigidbody rigidbody;

    private bool isPlayerDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = branch.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerDetected)
        {
            rigidbody.useGravity = true;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Detected");
            isPlayerDetected = true;
        }
        else
        {
            Debug.Log("Player Did not collide");
        }
    }
}