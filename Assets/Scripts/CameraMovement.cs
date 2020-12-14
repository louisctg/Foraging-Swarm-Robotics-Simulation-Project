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

    private float zoomSpeed = 0.25f;

    private float minCameraSize = 5.0f;
    private float maxCameraSize = 5.0f;

    private float cameraOffset;


    private void FixedUpdate()
    {
        arenaSize = arenaManager.GetArenaSize();

        // This is an arbitrary size to provide a good view of the arena
        maxCameraSize = Mathf.RoundToInt(arenaSize * 1.5f / 2);

        // Work out the limits for the camera
        // This means the camera will be confined closer to the centre the bigger it gets
        float oldRange = minCameraSize - maxCameraSize;
        float newRange = (arenaSize / 2) - 0;
        float oldValue = Camera.main.orthographicSize;
        float newValue = (((oldValue - maxCameraSize) * newRange) / oldRange) + 0;

        // Since the arena is square the offset is the same in all 4 directions
        cameraOffset = newValue;

        // Drag in direction to move
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x, pos.y, 0) * dragSpeed;

            transform.Translate(move, Space.World);
        }

        // Zoom In and Out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize += zoomSpeed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.orthographicSize -= zoomSpeed;
        }

        // Max size should be just over half the tilemap size
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minCameraSize, Mathf.RoundToInt(arenaSize * 1.5f/2) );

        CheckCameraBounds();
    }

    private void CheckCameraBounds()
    {
        // The camera is confined to the arena
        // It is pulled closer to the centre as it zooms out to prevent view of empty areas
        float xyMin = (arenaSize / 2) - cameraOffset;
        float xyMax = (arenaSize / 2) + cameraOffset;

        // Checks if the camera is out of bounds and moves it back
        if (transform.position.x < xyMin)
            transform.position = new Vector3(xyMin, transform.position.y, cameraDistance);
        else if (transform.position.x > xyMax)
            transform.position = new Vector3(xyMax, transform.position.y, cameraDistance);

        if (transform.position.y < xyMin)
            transform.position = new Vector3(transform.position.x, xyMin, cameraDistance);
        else if (transform.position.y > xyMax)
            transform.position = new Vector3(transform.position.x, xyMax, cameraDistance);
    }
}
