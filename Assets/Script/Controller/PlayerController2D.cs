using UnityEngine;
using System.Collections;
using InControl;

namespace Drop
{ 
	public class PlayerController2D : PlayerController {
		protected override void UpdateVelocity ()
		{
			if(PlayerInputDevice != null)
				rigidbody2D.velocity = PlayerInputDevice.Direction.Vector * speed;
			else
				rigidbody2D.velocity = transform.up * Input.GetAxis("Vertical") * speed;
		}

		public override void GhostMode(){
			CircleCollider2D collider = GetComponent<CircleCollider2D> ();
			collider.enabled = false;
			StartCoroutine (EndGhostMode(collider));
		}
	}
}