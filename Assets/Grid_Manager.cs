using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Manager : MonoBehaviour
{
    private float gridSize;

    private Mesh mesh;
    private MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        gridSize = 20;
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawQuad()
    {

    }
}
