using UnityEngine;
using System.Collections;
using InControl;

namespace Drop
{ 
	public class PlayerController3D : PlayerController {
		protected override void UpdateVelocity ()
		{
			if(PlayerInputDevice != null)
				rigidbody.velocity = PlayerInputDevice.Direction.Vector * speed;
			else
				rigidbody.velocity = transform.up * Input.GetAxis("Vertical") * speed;
		}
		
		public override void GhostMode(){
			SphereCollider collider = GetComponent<SphereCollider> ();
			collider.enabled = false;
			StartCoroutine (EndGhostMode(collider));
		}
	}
}