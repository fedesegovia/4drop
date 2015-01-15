using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {
	public enum GravityOrientation{
		X,
		Y,
		Z
	}

	public GravityOrientation orientation = GravityOrientation.Z;
	public float gravityForce = 1.0f;

	void Start () {
		if(orientation == GravityOrientation.Z)
			Physics.gravity = new Vector3(0, 0, gravityForce);
		else if(orientation == GravityOrientation.X)
			Physics.gravity = new Vector3(gravityForce, 0, 0);
		else
			Physics.gravity = new Vector3(0, gravityForce, 0);
	}
}
