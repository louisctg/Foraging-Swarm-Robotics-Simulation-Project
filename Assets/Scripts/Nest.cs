using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    private Arena_Manager arenaManager;

    [SerializeField]
    private int maxRobots;
    private int numberRobotsSpawned = 0;

    [SerializeField]
    private float angleOffset;
    private float currentAngle = 0.0f;
    [SerializeField]
    private float spawnFrequency;

    [SerializeField]
    private GameObject robotPrefab;
    [SerializeField]
    private GameObject robots;

    // Start is called before the first frame update
    void Start()
    {
        arenaManager = transform.parent.gameObject.GetComponent<Arena_Manager>();

        robots = arenaManager.robots;

        angleOffset = 90.0f;
        spawnFrequency = 2.0f;
        maxRobots = 20;

        robots = transform.parent.Find("Robots").gameObject;

        StartCoroutine("SpawnRobotsWithDelay", spawnFrequency);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRobotsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            SpawnRobot();
        }
    }

    private void SpawnRobot()
    {
        if(numberRobotsSpawned < maxRobots)
        {
            int arenaSize = arenaManager.GetArenaSize();
            Instantiate(robotPrefab, new Vector3(arenaSize / 2, arenaSize / 2), Quaternion.identity, robots.transform);     
        }
    }
}
