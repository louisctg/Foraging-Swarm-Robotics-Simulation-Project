using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Arena_Manager : MonoBehaviour
{
    [SerializeField]
    private TileBase grassTile;

    [SerializeField]
    private TileBase grassBottomTile;
    [SerializeField]
    private TileBase grassTopTile;
    [SerializeField]
    private TileBase grassRightTile;
    [SerializeField]
    private TileBase grassLeftTile;

    [SerializeField]
    private TileBase grassBottomLeftTile;
    [SerializeField]
    private TileBase grassBottomRightTile;
    [SerializeField]
    private TileBase grassTopLeftTile;
    [SerializeField]
    private TileBase grassTopRightTile;

    private Tilemap groundTiles;
    private Tilemap groundBorderTiles;
    private Tilemap seaTiles;

    [SerializeField]
    private int tileMapSize = 20;
    private int currentTileMapSize;

    // Start is called before the first frame update
    void Start()
    {
        groundTiles = transform.Find("Ground_Tiles").GetComponent<Tilemap>();
        groundBorderTiles = transform.Find("Ground_Border_Tiles").GetComponent<Tilemap>();
        seaTiles = transform.Find("Sea_Tiles").GetComponent<Tilemap>();

        //NOTE: Convert into method
        groundTiles.ClearAllTiles();
        for(int i = 0; i < tileMapSize; i++)
        {
            for(int j = 0; j < tileMapSize; j++)
            {
                groundTiles.SetTile(new Vector3Int(i, j, 0), grassTile);
            }
        }

        currentTileMapSize = tileMapSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTileMapSize != tileMapSize)
        {
            groundTiles.ClearAllTiles();
            for (int i = 0; i < tileMapSize; i++)
            {
                for (int j = 0; j < tileMapSize; j++)
                {
                    groundTiles.SetTile(new Vector3Int(i, j, 0), grassTile);
                }
            }
        }
        currentTileMapSize = tileMapSize;
    }
}
