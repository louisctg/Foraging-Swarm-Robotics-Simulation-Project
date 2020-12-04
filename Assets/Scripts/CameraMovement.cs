using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float dragSpeed = 1;
    private Vector3 dragOrigin;

    [SerializeField]
    private GameObject arenaGrid;
    [SerializeField]
    private Arena_Manager arenaManager;
    private float arenaSize;

    private float cameraDistance = -10;

    void Update()
    {
        arenaSize = arenaManager.GetArenaSize();

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x, pos.y, 0) * dragSpeed;

        transform.Translate(move, Space.World);
    }

    private void LateUpdate()
    {
        CheckCameraBounds();
    }

    private void CheckCameraBounds()
    {
        // Checks if the camera is out of bounds and moves it back
        if (transform.position.x < 1)
            transform.position = new Vector3(1, transform.position.y, cameraDistance);
        else if (transform.position.x > arenaSize - 1)
            transform.position = new Vector3(arenaSize - 1, transform.position.y, cameraDistance);

        if (transform.position.y < 1)
            transform.position = new Vector3(transform.position.x, 1, cameraDistance);
        else if (transform.position.y > arenaSize - 1)
            transform.position = new Vector3(transform.position.x, arenaSize - 1, cameraDistance);
    }
}
