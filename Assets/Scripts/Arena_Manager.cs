using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Arena_Manager : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    Mesh groundMesh;
    Mesh groundBorderMesh;
    Mesh groundCornerMesh;
    Mesh seaMesh;

    [SerializeField]
    [Range(10,200)]
    private int gridMapSize = 200;
    private int currentGridSize;

    [SerializeField]
    private GameObject collisionObjects;
    [SerializeField]
    private GameObject borderPrefab;

    private GameObject topBorder;
    private GameObject bottomBorder;
    private GameObject leftBorder;
    private GameObject rightBorder;

    [SerializeField]
    private GameObject nestPrefab;

    public GameObject robots;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.transform.position = new Vector3(gridMapSize / 2.0f, gridMapSize / 2.0f, -10);

        currentGridSize = gridMapSize;

        Instantiate(nestPrefab, new Vector3(currentGridSize / 2, currentGridSize / 2), Quaternion.identity, transform);
        robots = this.transform.Find("Robots").gameObject;
        DrawGrids();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGridSize != gridMapSize)
        {
            DrawGrids();
        }
    }

    private void DrawGrids()
    {
        currentGridSize = gridMapSize;

        DrawGround();

        DrawBorders();

        DrawCorners();

        DrawSea();

        // Instansiate border
        borderPrefab.transform.localScale = new Vector3(1, currentGridSize + 2);

        // Destroy existing prefabs (if any)
        Destroy(topBorder);
        Destroy(bottomBorder);
        Destroy(leftBorder);
        Destroy(rightBorder);

        // Initiate new prefabs
        topBorder = Instantiate(borderPrefab, new Vector2(currentGridSize / 2.0f, currentGridSize + 0.5f), Quaternion.Euler(0, 0, 90), collisionObjects.transform);
        bottomBorder = Instantiate(borderPrefab, new Vector2(currentGridSize / 2.0f, -0.5f), Quaternion.Euler(0, 0, 90), collisionObjects.transform);
        leftBorder = Instantiate(borderPrefab, new Vector2(-0.5f, currentGridSize / 2.0f), Quaternion.identity, collisionObjects.transform);
        rightBorder = Instantiate(borderPrefab, new Vector2(currentGridSize + 0.5f, currentGridSize / 2.0f), Quaternion.identity, collisionObjects.transform);
    }

    private void DrawGround()
    {
        groundMesh = new Mesh();

        int noOfSquares = currentGridSize * currentGridSize;
        Vector3[] vertices = new Vector3[noOfSquares * 4];
        Vector2[] uv = new Vector2[noOfSquares * 4];
        int[] triangles = new int[noOfSquares * 6];

        int vertexIndex = 0;
        int triangleIndex = 0;

        for (int x = 0; x < currentGridSize; x++)
        {
            for (int y = 0; y < currentGridSize; y++)
            {
                vertices[vertexIndex] = new Vector3(x, y, 0);
                vertices[vertexIndex + 1] = new Vector3(x, y + 1, 0);
                vertices[vertexIndex + 2] = new Vector3(x + 1, y + 1, 0);
                vertices[vertexIndex + 3] = new Vector3(x + 1, y, 0);

                uv[vertexIndex] = new Vector2(0, 0);
                uv[vertexIndex + 1] = new Vector2(0, 1);
                uv[vertexIndex + 2] = new Vector2(1, 1);
                uv[vertexIndex + 3] = new Vector2(1, 0);

                triangles[triangleIndex++] = vertexIndex;
                triangles[triangleIndex++] = vertexIndex + 1;
                triangles[triangleIndex++] = vertexIndex + 2;
                triangles[triangleIndex++] = vertexIndex;
                triangles[triangleIndex++] = vertexIndex + 2;
                triangles[triangleIndex++] = vertexIndex + 3;

                vertexIndex += 4;
            }
        }

        groundMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        groundMesh.vertices = vertices;
        groundMesh.triangles = triangles;
        groundMesh.uv = uv;
        groundMesh.RecalculateNormals();

        transform.Find("Ground Tiles").GetComponent<MeshFilter>().mesh = groundMesh;
    }
    
    private void DrawBorders()
    {
        groundBorderMesh = new Mesh();

        int noOfSquares = currentGridSize * 4;
        Vector3[] vertices = new Vector3[noOfSquares * 4];
        Vector2[] uv = new Vector2[noOfSquares * 4];
        int[] triangles = new int[noOfSquares * 6];

        int vertexIndex = 0;
        int triangleIndex = 0;

        for (int x = 0; x < currentGridSize; x++)
        {
            vertices[vertexIndex] = new Vector3(x, -1, 0);
            vertices[vertexIndex + 1] = new Vector3(x, 0, 0);
            vertices[vertexIndex + 2] = new Vector3(x + 1, 0, 0);
            vertices[vertexIndex + 3] = new Vector3(x + 1, -1, 0);

            uv[vertexIndex] = new Vector2(1, 1);
            uv[vertexIndex + 1] = new Vector2(1, 0);
            uv[vertexIndex + 2] = new Vector2(0, 0);
            uv[vertexIndex + 3] = new Vector2(0, 1);

            triangles[triangleIndex++] = vertexIndex;
            triangles[triangleIndex++] = vertexIndex + 1;
            triangles[triangleIndex++] = vertexIndex + 2;
            triangles[triangleIndex++] = vertexIndex;
            triangles[triangleIndex++] = vertexIndex + 2;
            triangles[triangleIndex++] = vertexIndex + 3;

            vertexIndex += 4;
        }
        
        for (int x = 0; x < currentGridSize; x++)
        {
            vertices[vertexIndex] = new Vector3(x, currentGridSize, 0);
            vertices[vertexIndex + 1] = new Vector3(x, currentGridSize + 1, 0);
            vertices[vertexIndex + 2] = new Vector3(x + 1, currentGridSize + 1, 0);
            vertices[vertexIndex + 3] = new Vector3(x + 1, currentGridSize, 0);

            uv[vertexIndex] = new Vector2(0, 0);
            uv[vertexIndex + 1] = new Vector2(0, 1);
            uv[vertexIndex + 2] = new Vector2(1, 1);
            uv[vertexIndex + 3] = new Vector2(1, 0);

            triangles[triangleIndex++] = vertexIndex;
            triangles[triangleIndex++] = vertexIndex + 1;
            triangles[triangleIndex++] = vertexIndex + 2;
            triangles[triangleIndex++] = vertexIndex;
            triangles[triangleIndex++] = vertexIndex + 2;
            triangles[triangleIndex++] = vertexIndex + 3;

            vertexIndex += 4;
        }        
        
        for (int y = 0; y < currentGridSize; y++)
        {
            vertices[vertexIndex] = new Vector3(-1, y, 0);
            vertices[vertexIndex + 1] = new Vector3(-1, y+1, 0);
            vertices[vertexIndex + 2] = new Vector3(0, y+1, 0);
            vertices[vertexIndex + 3] = new Vector3(0, y, 0);

            uv[vertexIndex] = new Vector2(0, 1);
            uv[vertexIndex + 1] = new Vector2(1, 1);
            uv[vertexIndex + 2] = new Vector2(1, 0);
            uv[vertexIndex + 3] = new Vector2(0, 0);

            triangles[triangleIndex++] = vertexIndex;
            triangles[triangleIndex++] = vertexIndex + 1;
            triangles[triangleIndex++] = vertexIndex + 2;
            triangles[triangleIndex++] = vertexIndex;
            triangles[triangleIndex++] = vertexIndex + 2;
            triangles[triangleIndex++] = vertexIndex + 3;

            vertexIndex += 4;
        }        
        
        for (int y = 0; y < currentGridSize; y++)
        {
            vertices[vertexIndex] = new Vector3(currentGridSize, y, 0);
            vertices[vertexIndex + 1] = new Vector3(currentGridSize, y+1, 0);
            vertices[vertexIndex + 2] = new Vector3(currentGridSize + 1, y+1, 0);
            vertices[vertexIndex + 3] = new Vector3(currentGridSize + 1, y, 0);

            uv[vertexIndex] = new Vector2(1, 0);
            uv[vertexIndex + 1] = new Vector2(0, 0);
            uv[vertexIndex + 2] = new Vector2(0, 1);
            uv[vertexIndex + 3] = new Vector2(1, 1);

            triangles[triangleIndex++] = vertexIndex;
            triangles[triangleIndex++] = vertexIndex + 1;
            triangles[triangleIndex++] = vertexIndex + 2;
            triangles[triangleIndex++] = vertexIndex;
            triangles[triangleIndex++] = vertexIndex + 2;
            triangles[triangleIndex++] = vertexIndex + 3;

            vertexIndex += 4;
        }
        

        groundBorderMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        groundBorderMesh.vertices = vertices;
        groundBorderMesh.triangles = triangles;
        groundBorderMesh.uv = uv;
        groundBorderMesh.RecalculateNormals();

        transform.Find("Ground Border Tiles").GetComponent<MeshFilter>().mesh = groundBorderMesh;
    }
    
    private void DrawCorners()
    {
        groundCornerMesh = new Mesh();

        int[] triangles = new int[24];

        Vector3[] vertices = new Vector3[16]
        {
            new Vector3(-1, -1),
            new Vector3(-1, 0),
            new Vector3(0, 0),
            new Vector3(0, -1),

            new Vector3(-1, currentGridSize),
            new Vector3(-1, currentGridSize + 1),
            new Vector3(0, currentGridSize + 1),
            new Vector3(0, currentGridSize),

            new Vector3(currentGridSize, currentGridSize),
            new Vector3(currentGridSize, currentGridSize + 1),
            new Vector3(currentGridSize + 1, currentGridSize + 1),
            new Vector3(currentGridSize + 1, currentGridSize),

            new Vector3(currentGridSize, -1),
            new Vector3(currentGridSize, 0),
            new Vector3(currentGridSize + 1, 0),
            new Vector3(currentGridSize + 1, -1)
        };
        
        Vector2[] uv = new Vector2[16]
        {
            new Vector2(0,1),
            new Vector2(1,1),
            new Vector2(1,0),
            new Vector2(0,0),

            new Vector2(0,0),
            new Vector2(0,1),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(1,0),
            new Vector2(0,0),
            new Vector2(0,1),
            new Vector2(1,1),

            new Vector2(1,1),
            new Vector2(1,0),
            new Vector2(0,0),
            new Vector2(0,1)
        };

        int triangleIndex = 0;
        for(int i = 0; i < 4; i++)
        {
            triangles[triangleIndex++] = (i * 4) + 0;
            triangles[triangleIndex++] = (i * 4) + 1;
            triangles[triangleIndex++] = (i * 4) + 2;
            triangles[triangleIndex++] = (i * 4) + 0;
            triangles[triangleIndex++] = (i * 4) + 2;
            triangles[triangleIndex++] = (i * 4) + 3;
        }

        groundCornerMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        groundCornerMesh.vertices = vertices;
        groundCornerMesh.triangles = triangles;
        groundCornerMesh.uv = uv;
        groundCornerMesh.RecalculateNormals();

        transform.Find("Ground Corner Tiles").GetComponent<MeshFilter>().mesh = groundCornerMesh;
    }
  
    private void DrawSea()
    {
        seaMesh = new Mesh();

        float height = currentGridSize * 2.0f;
        float width = (height / 9.0f) * 16.0f;
        int yAddOn = Mathf.RoundToInt((height - currentGridSize) / 2);
        int xAddOn = Mathf.RoundToInt((width - currentGridSize) / 2);

        int noOfSquares = (2 * xAddOn + currentGridSize) * (2 * yAddOn + currentGridSize);
        Vector3[] vertices = new Vector3[noOfSquares * 4];
        Vector2[] uv = new Vector2[noOfSquares * 4];
        int[] triangles = new int[noOfSquares * 6];

        int vertexIndex = 0;
        int triangleIndex = 0;

        for (int x = -xAddOn; x < currentGridSize + xAddOn; x++)
        {
            for (int y = -yAddOn; y < currentGridSize + yAddOn; y++)
            {
                vertices[vertexIndex] = new Vector3(x, y, 0);
                vertices[vertexIndex + 1] = new Vector3(x, y + 1, 0);
                vertices[vertexIndex + 2] = new Vector3(x + 1, y + 1, 0);
                vertices[vertexIndex + 3] = new Vector3(x + 1, y, 0);

                uv[vertexIndex] = new Vector2(0, 0);
                uv[vertexIndex + 1] = new Vector2(0, 1);
                uv[vertexIndex + 2] = new Vector2(1, 1);
                uv[vertexIndex + 3] = new Vector2(1, 0);

                triangles[triangleIndex++] = vertexIndex;
                triangles[triangleIndex++] = vertexIndex + 1;
                triangles[triangleIndex++] = vertexIndex + 2;
                triangles[triangleIndex++] = vertexIndex;
                triangles[triangleIndex++] = vertexIndex + 2;
                triangles[triangleIndex++] = vertexIndex + 3;

                vertexIndex += 4;
            }
        }

        seaMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        seaMesh.vertices = vertices;
        seaMesh.triangles = triangles;
        seaMesh.uv = uv;
        seaMesh.RecalculateNormals();

        transform.Find("Sea Tiles").GetComponent<MeshFilter>().mesh = seaMesh;

    }

    public int GetArenaSize()
    {
        return gridMapSize;
    }
}
