using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
	[Range(0,1000)]
	public float viewRadius;

	[Range(0, 360)]
	public float viewAngle;

	[HideInInspector]
	public List<Transform> visibleObjects = new List<Transform>();

	public MeshFilter fieldOfViewMeshFilter;
	Mesh fieldOfViewMesh;

	[SerializeField]
	private float raysPerDegree;

	private void Start()
    {
		// Create new mesh for rendering the field of view
		fieldOfViewMesh = new Mesh();
		fieldOfViewMeshFilter.mesh = fieldOfViewMesh;

		StartCoroutine("FindObjectsWithDelay", 0.25f);
	}

    private void LateUpdate()
    {
		DrawFieldOfView();
    }

    void FindVisibleObjects()
	{
		visibleObjects.Clear();

		// Get all object (with colliders) within the given radius
		Collider2D[] objectsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius);

		// Check the objects are within the field of view angle
		for (int i = 0; i < objectsInViewRadius.Length; i++)
		{
			Transform obj = objectsInViewRadius[i].transform;
			Vector2 directionToObject = (obj.position - transform.position).normalized;

			if (Vector2.Angle(transform.right, directionToObject) < viewAngle / 2)
			{
				float distanceToObject = Vector3.Distance(transform.position, obj.position);

				if (!Physics.Raycast(transform.position, directionToObject, distanceToObject))
				{
					visibleObjects.Add(obj);
				}
			}
		}
	}

	IEnumerator FindObjectsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleObjects();
		}
	}

	void DrawFieldOfView()
	{
		int steps = 40;
		float stepAngle = viewAngle / steps;
		
		Vector3[] vertices = new Vector3[steps + 2];
		Vector3[] normals = new Vector3[steps + 2];
		int[] triangles = new int[steps * 3];
		
		for (int step = 0; step <= steps; step++)
		{
			float angle = viewAngle / 2 - stepAngle * step;
			Vector3 direction = DirectionFromAngle(angle, transform.forward, transform.right);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, viewRadius);
			//Debug.DrawRay(transform.position, (direction * viewRadius), Color.blue);
			vertices[step + 1] = new Vector3(0,0,-0.1f) + transform.InverseTransformDirection(direction * viewRadius);
		}

		for (int triangle = 0; triangle < steps; triangle++)
		{
			triangles[triangle * 3] = 0;
			triangles[triangle * 3 + 1] = triangle + 1;
			triangles[triangle * 3 + 2] = triangle + 2;
		}

		fieldOfViewMesh.vertices = vertices;
		fieldOfViewMesh.normals = normals;
		fieldOfViewMesh.triangles = triangles;
		fieldOfViewMesh.RecalculateNormals();
	}

	public Vector3 DirectionFromAngle(float angle, Vector3 axis, Vector3 zeroDirection)
	{
		return Quaternion.AngleAxis(angle, axis) * zeroDirection;
	}
}
