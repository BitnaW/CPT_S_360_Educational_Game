using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;
    public Transform[] spawnPoints;

    void Start()
    {
        int i = 0;
        foreach (GameObject target in targets)
        {
            Instantiate(target, spawnPoints[i].position, Quaternion.identity);
            i++;
        }
    }

    
}