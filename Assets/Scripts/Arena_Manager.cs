using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Arena_Manager : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    private Tilemap groundTiles;
    private Tilemap groundBorderTiles;
    private Tilemap seaTiles;

    // For groundTiles
    [SerializeField]
    private TileBase grassTile;

    // For groundBorderTiles
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

    // For seaTiles
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
        mainCamera.transform.position = new Vector3(tileMapSize / 2.0f, tileMapSize / 2.0f, -10);

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
        groundBorderTiles.ClearAllTiles();
        seaTiles.ClearAllTiles();

        // Draw the ground
        for (int x = 0; x < currentTileMapSize; x++)
        {
            // Draw centre arena
            for (int y = 0; y < currentTileMapSize; y++)
            {
                groundTiles.SetTile(new Vector3Int(x, y, 0), grassTile);
            }
        }

        // Draw the top and bottom border
        for (int x = 0; x < currentTileMapSize; x++)
        {
            groundBorderTiles.SetTile(new Vector3Int(x, currentTileMapSize, 0), grassTopTile);
            groundBorderTiles.SetTile(new Vector3Int(x, -1, 0), grassBottomTile);
        }

        // Draw left and right border
        for (int y = 0; y < currentTileMapSize; y++)
        {
            groundBorderTiles.SetTile(new Vector3Int(-1, y, 0), grassLeftTile);
            groundBorderTiles.SetTile(new Vector3Int(currentTileMapSize, y, 0), grassRightTile);
        }

        // Draw the corners of the border
        groundBorderTiles.SetTile(new Vector3Int(-1, -1, 0), grassBottomLeftTile);
        groundBorderTiles.SetTile(new Vector3Int(currentTileMapSize, -1, 0), grassBottomRightTile);
        groundBorderTiles.SetTile(new Vector3Int(-1, currentTileMapSize, 0), grassTopLeftTile);
        groundBorderTiles.SetTile(new Vector3Int(currentTileMapSize, currentTileMapSize, 0), grassTopRightTile);

        float height = currentTileMapSize * 1.5f;
        float width = (height / 9.0f) * 16.0f;
        int yAddOn = Mathf.RoundToInt((height - currentTileMapSize) / 2);
        int xAddOn = Mathf.RoundToInt((width - currentTileMapSize) / 2);


        // Draw the sea (i.e. to differenciate between the ground and out of bounds area)
        for (int x = -xAddOn; x < currentTileMapSize + xAddOn; x++)
        {
            for (int y = -yAddOn; y < currentTileMapSize + yAddOn; y++)
            {
                seaTiles.SetTile(new Vector3Int(x, y, 0), seaTile);
            }
        }
        

        collisionObjects.transform.position = new Vector2(currentTileMapSize / 2.0f, currentTileMapSize / 2.0f);

        // Instansiate border
        borderPrefab.transform.localScale = new Vector3(1, currentTileMapSize + 2);

        // Destroy existing prefabs (if any)
        Destroy(topBorder);
        Destroy(bottomBorder);
        Destroy(leftBorder);
        Destroy(rightBorder);

        // Initiate new prefabs
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

    public float GetArenaSize()
    {
        return tileMapSize;
    }
}
