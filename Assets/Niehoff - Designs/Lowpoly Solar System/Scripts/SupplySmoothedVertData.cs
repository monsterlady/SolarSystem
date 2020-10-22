using UnityEngine;
using System.Collections;

// Small helper script to supply smoothed vertex data of the parent object to the parent shader
public class SupplySmoothedVertData : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		// Get the parents mesh object
		Mesh targetMesh = this.gameObject.GetComponent<MeshFilter>().mesh;

		// save its default normals
		Vector3[] normals = targetMesh.normals;

		// smooth the mesh normals
		targetMesh.RecalculateNormals( 90 );

		// afterwards get its new normals
		Vector3[] smoothNormals = targetMesh.normals;

		// and re-assign the array of default normals to the mesh
		targetMesh.normals = normals;

        
		// get the objects material
		Material material = this.gameObject.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
