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
    private TileBase seaTile;

    [SerializeField]
    [Range(2,200)]
    private int tileMapSize = 20;
    private int currentTileMapSize;

    [SerializeField]
    private GameObject collisionObjects;

    [SerializeField]
    private GameObject borderPrefab;

    private GameObject topBorder;
    private GameObject bottomBorder;
    private GameObject leftBorder;
    private GameObject rightBorder;


    // Start is called before the first frame update
    void Start()
    {
        groundTiles = transform.Find("Ground_Tiles").GetComponent<Tilemap>();
        groundBorderTiles = transform.Find("Ground_Border_Tiles").GetComponent<Tilemap>();
        seaTiles = transform.Find("Sea_Tiles").GetComponent<Tilemap>();
        DrawTilemaps();
    }

    private void DrawTilemaps()
    {
        currentTileMapSize = tileMapSize;

        // Clear all tileMaps
        groundTiles.ClearAllTiles();
        seaTiles.ClearAllTiles();

        // Draw the ground
        for (int i = 0; i < currentTileMapSize; i++)
        {
            for (int j = 0; j < currentTileMapSize; j++)
            {
                groundTiles.SetTile(new Vector3Int(i, j, 0), grassTile);
            }
        }

        // Draw the sea (i.e. to differenciate between the ground and out of bounds area)
        for (int i = -5; i < currentTileMapSize + 5; i++)
        {
            for (int j = -5; j < currentTileMapSize + 5; j++)
            {
                seaTiles.SetTile(new Vector3Int(i, j, 0), seaTile);
            }
        }

        collisionObjects.transform.position = new Vector2(currentTileMapSize / 2.0f, currentTileMapSize / 2.0f);

        // Instansiate border
        borderPrefab.transform.localScale = new Vector3(1, currentTileMapSize);

        Destroy(topBorder);
        Destroy(bottomBorder);
        Destroy(leftBorder);
        Destroy(rightBorder);

        topBorder = Instantiate(borderPrefab, new Vector2(currentTileMapSize / 2.0f, currentTileMapSize + 0.5f), Quaternion.Euler(0, 0, 90), collisionObjects.transform);
        bottomBorder = Instantiate(borderPrefab, new Vector2(currentTileMapSize / 2.0f, -0.5f), Quaternion.Euler(0, 0, 90), collisionObjects.transform);

        leftBorder = Instantiate(borderPrefab, new Vector2(-0.5f, currentTileMapSize / 2.0f), Quaternion.identity, collisionObjects.transform);
        rightBorder = Instantiate(borderPrefab, new Vector2(currentTileMapSize + 0.5f, currentTileMapSize / 2.0f), Quaternion.identity, collisionObjects.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTileMapSize != tileMapSize)
        {
            DrawTilemaps();
        }
    }
}
