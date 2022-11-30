using UnityEngine;

public class Rotate : MonoBehaviour
{
	public float rotateSpeed = 10;
	void Update()
	{
		transform.Rotate (new Vector3 (0, Time.deltaTime * rotateSpeed, 0));
	}
}