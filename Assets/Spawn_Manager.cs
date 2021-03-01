using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f,1.0f)]
    private float probability;

    private Arena_Manager arenaManager;

    [SerializeField]
    private GameObject foragingMaterials;

    [SerializeField]
    private GameObject foragingItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        arenaManager = GetComponent<Arena_Manager>();

        probability = 0.1f;
        for(int x = 0; x < arenaManager.GetArenaSize(); x++)
        {
            for(int y = 0; y < arenaManager.GetArenaSize(); y++)
            {
                float rand = Random.Range(0.0f, 1.0f);

                if (rand <= probability)
                {
                    GameObject newForagingItem = Instantiate(foragingItemPrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity, foragingMaterials.transform);
                    newForagingItem.name = "Foraging Item (" + x + "," + y + ")";
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
