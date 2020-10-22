using UnityEngine;
using System.Collections;

public class PlanetRotation : MonoBehaviour 
{
    [Tooltip("The time in hours, that the planet needs for a full rotation (eg. 24 for the Earth).")]
	public float rotationTime = 0.05f;

	// Update is called once per frame
	void LateUpdate() 
	{
        this.gameObject.transform.Rotate(0, (360 / (rotationTime * 60 * 60)) * Time.deltaTime, 0, Space.Self);
	}
}
