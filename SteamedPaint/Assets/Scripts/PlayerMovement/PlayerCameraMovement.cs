
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour 
{
	public Vector3 cameraPosition = Vector3.zero;
	public Transform target;
	[SerializeField]
	float smoothing = 0.3f;

	void FixedUpdate()
	{
		cameraPosition = new Vector3
		(
		Mathf.SmoothStep(transform.position.x, target.transform.position.x, smoothing),
		Mathf.SmoothStep(transform.position.y, target.transform.position.y, smoothing)
		);
	}

	void LateUpdate()
	{
		transform.position = cameraPosition + Vector3.forward * -10f;
	}
	
	
}
