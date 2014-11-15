using UnityEngine;
using System.Collections;
using InControl;

namespace Drop
{ 
    public class PlayerController : MonoBehaviour {
        public int playerId = 0;
        public float speed = 50; // Speed is messured in units per second. One Unity unit equals one meter.

		InputDevice PlayerInputDevice;



		Vector3 direction = Vector3.zero;
		Vector3 deltaToNewPosition = Vector3.zero;


		void Start()
        {
            PlayerInputDevice = InputManager.Devices[playerId];
		}

	    void Update () {
            PlayerInputDevice = (InputManager.Devices.Count > playerId) ? InputManager.Devices[playerId] : null;
            if (PlayerInputDevice == null)
            {
                // No Controller for this player....
            }
            else
            {
                Move();
                Shoot();
            }
	    }

        void Move()
        {
			UpdateVelocity ();
			RotateToFaceCorrectDirection ();
        }

		void UpdateVelocity ()
		{
			rigidbody2D.velocity = PlayerInputDevice.Direction.Vector * speed;
		}

		void RotateToFaceCorrectDirection ()
		{
			direction.Set (PlayerInputDevice.Direction.X, PlayerInputDevice.Direction.Y, 0);
			if ( direction.magnitude > Config.Instance.InputControllerSensitivity )
			{
				deltaToNewPosition = direction - transform.position;
				float eulerRotationInZ = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler (0, 0, eulerRotationInZ - 90);
			}
		}

		void Shoot()
		{
//            TwoAxisInputControl rightStick = PlayerInputDevice.RightStick;
//            Vector2 direction = rightStick.Vector;
//
//			Debug.Log (direction);
//
//			if (Mathf.Abs (direction.x) > .7f)
//				direction = (Vector2.right * direction.x).normalized;
//			
//			if (Mathf.Abs (direction.y) > .7f)
//				direction = (Vector2.up * direction.y).normalized;
//
//			if (direction.magnitude > .7f) 
//			{
//				if(transform.childCount > 0)
//				{
//					voxelTransform = transform.GetChild(0);
//					
//					if (voxelTransform != null)
//					{
//						voxelTransform.parent = null;
//						PutBulletControllerOnVoxel(voxelTransform.gameObject);
//						
//						voxelTransform.GetComponent<BulletController>().Direction.Set(direction.x, 0, direction.y);
//						voxelsCount--;
//					}
//				}
//			}
        }
	}
}