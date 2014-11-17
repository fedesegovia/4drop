using UnityEngine;
using System.Collections;
using InControl;

namespace Drop
{ 
    public class PlayerController : MonoBehaviour {
        public int playerId = 0;
        public int health = 50;
        public float speed = 50.0f; // Speed is messured in units per second. One Unity unit equals one meter.

		InputDevice PlayerInputDevice;

		Vector3 movementDirection = Vector3.zero;
		Vector3 shootingDirection = Vector3.zero;
		Vector3 deltaToNewPosition = Vector3.zero;
		Weapon weapon;

		void Start()
        {
            PlayerInputDevice = InputManager.Devices[playerId];
			weapon = GetComponentInChildren<Weapon>();
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
			rigidbody2D.velocity = PlayerInputDevice.LeftStick.Vector * speed;
		}

		void RotateToFaceCorrectDirection ()
		{
			movementDirection.Set (PlayerInputDevice.LeftStick.X, PlayerInputDevice.LeftStick.Y, 0);
			if ( movementDirection.magnitude > Config.Instance.InputControllerSensitivity )
			{
				deltaToNewPosition = movementDirection - transform.position;
				float eulerRotationInZ = Mathf.Atan2 (movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler (0, 0, eulerRotationInZ - 90);
			}
		}

		void Shoot()
		{
			shootingDirection.Set(PlayerInputDevice.RightStick.X, PlayerInputDevice.RightStick.Y, 0);

			if(weapon && shootingDirection.magnitude > Config.Instance.InputControllerSensitivity){
				weapon.Shoot(shootingDirection);
			}
        }

		public void AbsorbDamage(int damage){
			health -= damage;

			if(health <= 0){
				health = 0;

				Die();
			}
		}

		void Die(){

		}
	}
}