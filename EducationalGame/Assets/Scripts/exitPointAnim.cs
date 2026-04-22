using UnityEngine;

//simple script to allow exit point to oscillate 

public class HopAnimation : MonoBehaviour
{
    public float hopHeight = 0.2f;
    public float hopSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * hopSpeed) * hopHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}