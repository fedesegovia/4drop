using UnityEngine;
using System.Collections;
using InControl;

namespace Drop
{ 
    public class PlayerController : MonoBehaviour {
        public int PlayerId = 0;
        public float speed = 50; // Speed is messured in units per second. One Unity unit equals one meter.

		InputDevice PlayerInputDevice;

		public Sprite[] PlayerSprites;

		Vector3 direction = Vector3.zero;
		Vector3 deltaToNewPosition = Vector3.zero;

		GameObject capturedFlag = null;
		bool flagIsAvailable = false;

		void Start()
        {
			UpdatePlayerInput ();
			UpdatePlayerSprite ();
		}

		void UpdatePlayerInput ()
		{
			PlayerInputDevice = InputManager.Devices[PlayerId];
		}

		void UpdatePlayerSprite ()
		{
			GetComponent<SpriteRenderer> ().sprite = PlayerSprites [PlayerId];
		}

	    void Update () {
            PlayerInputDevice = (InputManager.Devices.Count > PlayerId) ? InputManager.Devices[PlayerId] : null;
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

		public void AttachFlag(GameObject flag){
			capturedFlag = flag;
			flag.transform.parent = null;

			flag.transform.position = transform.position;

			flag.GetComponent<FlagController>().FollowPlayer(this);

			StartCoroutine (WaitBeforeFlagIsAvailable ());
		}

		public void LoseFlag(){
			capturedFlag = null;
			flagIsAvailable = true;
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

		public bool HasFlag(){
			if (capturedFlag != null)
				return true;
			return false;
		}

		IEnumerator WaitBeforeFlagIsAvailable(){
			yield return new WaitForSeconds (2);
			flagIsAvailable = true;
		}

		void OnTriggerEnter2D(Collider2D collider){
			if (capturedFlag != null && flagIsAvailable && CollisionIsPlayer(collider)) {
				PlayerController player = GetPlayerController(collider.gameObject);

				player.AttachFlag(capturedFlag);

				LoseFlag();
			}
		}

		bool CollisionIsPlayer(Collider2D collider){
			PlayerController player = GetPlayerController (collider.gameObject);
			if (player != null)
				return true;
			return false;
		}

		PlayerController GetPlayerController(GameObject player){
			return player.GetComponent<PlayerController> ();
		}

		public void GhostMode(){
			CircleCollider2D collider = GetComponent<CircleCollider2D> ();
			collider.enabled = false;
			StartCoroutine (EndGhostMode(collider));
		}

		IEnumerator EndGhostMode(CircleCollider2D collider){
			yield return new WaitForSeconds (3);
			collider.enabled = true;
		}

		public void SpeedUp(){
			speed = speed * 1.5f;
			StartCoroutine (SlowDown());
		}

		IEnumerator SlowDown(){
			yield return new WaitForSeconds (1);
			speed = speed / 1.5f;
		}
	}
}