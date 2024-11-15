using UnityEngine;

public class RoadCross : MonoBehaviour
{
    [SerializeField] GameObject[] roadCrosser;
    [SerializeField] int crossingSpeed;
    public bool isPlayerDetected = false;
    private Vector3[] startPositions; // Array to store each object's start position
    private float moveDistance = 150f;
    public BoxCollider[] boxCollider;

    void Start()
    {
        // Store the initial positions of each roadCrosser object
        startPositions = new Vector3[roadCrosser.Length];
        boxCollider = new BoxCollider[roadCrosser.Length];
        for (int i = 0; i < roadCrosser.Length; i++)
        {
            startPositions[i] = roadCrosser[i].transform.position;
            boxCollider[i] = roadCrosser[i].GetComponent<BoxCollider>();
        }
    }

    void Update()
    {
        if (isPlayerDetected)
        {
            for (int i = 0; i < roadCrosser.Length; i++)
            {
                // Calculate the distance moved from the start position
                float distanceMoved = Vector3.Distance(startPositions[i], roadCrosser[i].transform.position);

                // Move if within the allowed range
                if (distanceMoved < moveDistance)
                {
                    roadCrosser[i].transform.Translate(Vector3.back * (Time.deltaTime * crossingSpeed));
                }
            }
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