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

    private void Start()
    {
		StartCoroutine("FindObjectsWithDelay", 0.25f);
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

	public Vector3 DirectionFromAngle(float angle, Vector3 axis, Vector3 zeroDirection)
	{
		return Quaternion.AngleAxis(angle, axis) * zeroDirection;
	}
}
