using UnityEngine;


public class RoadCross : MonoBehaviour
{
    [SerializeField] GameObject[] roadCrosser;
    [SerializeField] int crossingSpeed;

    public bool isPlayerDitected = false;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Updating Position");


        if (isPlayerDitected)
        {
            for (int i = 0; i < roadCrosser.Length; i++)
            {
                if (i >= 0 && i < roadCrosser.Length)
                {
                    roadCrosser[i].transform.Translate(Vector3.back * (Time.deltaTime * crossingSpeed));
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Called");
        if (other.tag == "Player")
        {
            Debug.Log("Player Ditect");
            isPlayerDitected = true;
          
        }
        else
        {
            Debug.Log("Player Didnt collide");
        }
    }
}