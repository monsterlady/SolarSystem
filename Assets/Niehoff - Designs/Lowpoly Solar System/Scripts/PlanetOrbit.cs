using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class PlanetOrbit : MonoBehaviour 
{
    [Header("Orbit Simulation")]
    [Tooltip("Defines the Center of Gravity for this Object, which is used as Reference for our Orbit Simulation.")]
	public GameObject centerOfGravity;
    [Tooltip("Allows you to set the rotaion axis of the objects orbit")]
	public Vector3 orbitAxis = Vector3.up;
    [Tooltip("Defines the distance between the center of gravity and this GameObject")]
	public float orbitRadius = 5.0f;
    [Tooltip("Defines the velocity at which this GameObject will move around the center of gravity")]
	public float orbitSpeed = 80.0f;
    [Tooltip("Defines momentum change, if the orbit radius, or Center of Gravity changes. This allows you to define how quick, this GameObject will move graduly into its new orbit.")]
	public float gravityChange = 0.5f;
	// Defines the desired position, to gradually move into a new orbit, if the orbitRadius gets changed
	private Vector3 desiredPosition;


    [Header("Trajectory Visualization")]
    [Tooltip("Enables a 3D Visulazation of our GameObjects Trajectory")]
	public bool showOrbit = true;
    [Tooltip("Defines the color of the rendered circle for the trajectory.")]
	public Color lineColor = Color.red;
    [Tooltip("Defines the line width of the rendered circle for the trajectory.")]
	public float lineWidth = 0.05f;

	private int segments = 128;
	private LineRenderer line;


	void Start() 
	{
		// Set the initial position of the object
		transform.position  = (transform.position - centerOfGravity.transform.position).normalized * orbitRadius;

		// Adjust the radius to the center of gravity if needed
		desiredPosition     = (transform.position - centerOfGravity.transform.position).normalized * orbitRadius + centerOfGravity.transform.position;
		transform.position  = desiredPosition;

		if(showOrbit)
		{
			// Setup our line renderer
			line            = gameObject.GetComponent<LineRenderer>();
			// create a new material for it
			line.material   = new Material(Shader.Find("Particles/Alpha Blended"));
			// and color it
			line.SetColors( lineColor, lineColor);
			// define its size
			line.SetWidth( lineWidth, lineWidth);
			line.SetVertexCount (segments + 1);
			// and make sure its in world space
			line.useWorldSpace = true;

			// Afterwards we can initilize our circle
			CreatePoints ();
		}
	}
	
	void LateUpdate() 
	{
		// Start to rotate our object around its center of gravity
		transform.RotateAround (centerOfGravity.transform.position, orbitAxis, orbitSpeed * Time.deltaTime);
		// Adjust the radius to the center of gravity if needed
		desiredPosition     = (transform.position - centerOfGravity.transform.position).normalized * orbitRadius + centerOfGravity.transform.position;
		// Set the final position for this update
		transform.position  = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * orbitRadius);

        if (showOrbit)
        {
            line.SetColors(lineColor, lineColor);
            CreatePoints();
        }
	}


	void CreatePoints ()
	{
		Vector3 currentPosition = transform.position;
		// Loops through the amount of segments, our circle should have and set the line position for each segment
		for (int i = 0; i < (segments + 1); i++)
		{
			transform.RotateAround (centerOfGravity.transform.position, orbitAxis, i * 0.04375f); // i * 0.04375f
			Vector3 linePos = transform.position;
			line.SetPosition ( i, linePos );			
		}

		transform.position = currentPosition;
	}
}
