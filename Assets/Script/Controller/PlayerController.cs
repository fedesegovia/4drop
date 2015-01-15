using UnityEngine;
using System.Collections;
using InControl;

namespace Drop
{ 
    public class PlayerController : MonoBehaviour {
        public int PlayerId = 0;
        public float speed = 50; // Speed is messured in units per second. One Unity unit equals one meter.
		public Sprite[] PlayerSprites;

		protected InputDevice PlayerInputDevice;
		protected Vector3 direction = Vector3.zero;
		protected Vector3 deltaToNewPosition = Vector3.zero;
		protected GameObject capturedFlag = null;
		protected bool flagIsAvailable = false;

		protected virtual void Start()
        {
			UpdatePlayerInput ();
			UpdatePlayerSprite ();
		}

		void UpdatePlayerInput ()
		{
			if(InputManager.Devices.Count > 0)
				PlayerInputDevice = InputManager.Devices[PlayerId];
		}

		void UpdatePlayerSprite ()
		{
			GetComponent<SpriteRenderer> ().sprite = PlayerSprites [PlayerId];
		}

	    void Update () {
            PlayerInputDevice = (InputManager.Devices.Count > PlayerId) ? InputManager.Devices[PlayerId] : null;
            
			Move();
	    }

        void Move()
        {
			UpdateVelocity ();
			RotateToFaceCorrectDirection ();
        }

		protected virtual void UpdateVelocity (){
		}

		void RotateToFaceCorrectDirection ()
		{
			if(PlayerInputDevice != null){
				direction.Set (PlayerInputDevice.Direction.X, PlayerInputDevice.Direction.Y, 0);
				if ( direction.magnitude > Config.Instance.InputControllerSensitivity )
				{
					deltaToNewPosition = direction - transform.position;
					float eulerRotationInZ = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
					transform.rotation = Quaternion.Euler (0, 0, eulerRotationInZ - 90);
				}
			}
			else{
				float rotation = -Input.GetAxis("Horizontal") * 3.0f;
				transform.Rotate(0, 0, rotation);
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

		void OnTriggerEnter(Collider collider){
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

		bool CollisionIsPlayer(Collider collider){
			PlayerController player = GetPlayerController (collider.gameObject);
			if (player != null)
				return true;
			return false;
		}

		PlayerController GetPlayerController(GameObject player){
			return player.GetComponent<PlayerController> ();
		}

		public virtual void GhostMode(){
		}

		protected IEnumerator EndGhostMode(CircleCollider2D collider){
			yield return new WaitForSeconds (3);
			collider.enabled = true;
		}

		protected IEnumerator EndGhostMode(SphereCollider collider){
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