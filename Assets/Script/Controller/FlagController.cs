using UnityEngine;
using System.Collections;

namespace Drop
{
	public class FlagController : MonoBehaviour {
		public float followForce = 30.0f;
		public bool following = false;
		public bool followForceActivated = true;
		PlayerController playerToFollow = null;
		FlagPickUpController pickUpController = null;
		Vector3 directionToPlayer;

		void Start(){
			pickUpController = GetComponent<FlagPickUpController>();
		}

		void Update(){
			if(playerToFollow != null){
				FollowPlayer();
				
				if(PlayerToFollowIsTooFarAway())
					StopFollowing();
			}
		}

		public void FollowPlayer(PlayerController player){
			playerToFollow = player;

			pickUpController.DisablePickingUp();

			following = true;
		}

		public void StopFollowing(){
			playerToFollow.LoseFlag();
			playerToFollow = null;

			pickUpController.EnablePickingUp();

			following = false;

			StopFollowForce();
		}

		void FollowPlayer(){
			if(followForceActivated){
				followForce = playerToFollow.speed;
				
				directionToPlayer = playerToFollow.transform.position - transform.position;
				
				directionToPlayer.Normalize();

				ApplyFollowForce(directionToPlayer);
			}
			else
				StopFollowForce();
		}

		void OnCollisionStay2D(Collision2D collision){
			PlayerController playerController = collision.collider.GetComponent<PlayerController>();

			followForceActivated = !(playerController && !collision.collider.isTrigger);
		}

		void OnCollisionExit2D(){
			followForceActivated = true;
		}

		void ApplyFollowForce(Vector2 direction){
			rigidbody2D.velocity = new Vector2(direction.x * followForce, direction.y * followForce);
		}

		void StopFollowForce(){
			rigidbody2D.velocity = Vector2.zero;
		}

		bool PlayerToFollowIsTooFarAway(){
			return Mathf.Sqrt( (playerToFollow.transform.position - transform.position).sqrMagnitude ) > 10.0f;
		}
	}
}