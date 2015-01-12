using UnityEngine;
using System.Collections;

namespace Drop{
	[RequireComponent (typeof (Rigidbody))]
	public class SlideableBlock : MonoBehaviour {
		public enum SlideableBlockDirection{
			Up,
			Down,
			Left,
			Right
		}

		public enum CurrentRelativePosition{
			Up,
			Down,
			Left,
			Right,
			GoingUp,
			GoingDown,
			GoingLeft,
			GoingRight
		}

		public SlideableBlockDirection direction = SlideableBlockDirection.Left;
		public int distanceInBlocks = 1;
		public float speed = 650.0f;
		public float delayInSeconds = 1.3f;
		public RigidbodyConstraints rigidBodyConstraints;
		CurrentRelativePosition currentRelativePos = CurrentRelativePosition.Right;
		Vector3 originalPosition;
		float startedCountingTime = 0.0f;
		Vector3 positionDestination;

		void Start(){
			originalPosition = transform.position;

			InitializeRigidBody();

			InitializeCurrentRealitvePosition();

			StartCountingTime();
		}

		void Update(){
			switch (currentRelativePos) {
				case CurrentRelativePosition.GoingUp:
				case CurrentRelativePosition.GoingDown:
				case CurrentRelativePosition.GoingLeft:
				case CurrentRelativePosition.GoingRight:
					Move();
					break;
				default:
					if(IsTimeToMove())
						StartMoving();
					break;
			}
		}

		void StartCountingTime(){
			startedCountingTime = Time.timeSinceLevelLoad;
		}

		bool IsTimeToMove(){
			return (Time.timeSinceLevelLoad - startedCountingTime >= delayInSeconds);
		}

		void InitializeCurrentRealitvePosition(){
			switch (direction) {
			case SlideableBlockDirection.Up:
				currentRelativePos = CurrentRelativePosition.Down;
				break;
			case SlideableBlockDirection.Down:
				currentRelativePos = CurrentRelativePosition.Up;
				break;
			case SlideableBlockDirection.Left:
				currentRelativePos = CurrentRelativePosition.Right;
				break;
			case SlideableBlockDirection.Right:
				currentRelativePos = CurrentRelativePosition.Left;
				break;
			}
		}

		void InitializeRigidBody(){
			rigidbody.useGravity = false;
			rigidbody.isKinematic = true;
			rigidbody.constraints = rigidBodyConstraints;
		}

		void StartMoving(){
			switch (currentRelativePos) {
				case CurrentRelativePosition.Up:
					currentRelativePos = CurrentRelativePosition.GoingDown;
					break;
				case CurrentRelativePosition.Down:
					currentRelativePos = CurrentRelativePosition.GoingUp;
					break;
				case CurrentRelativePosition.Left:
					currentRelativePos = CurrentRelativePosition.GoingRight;
					break;
				case CurrentRelativePosition.Right:
					currentRelativePos = CurrentRelativePosition.GoingLeft;
					break;
			}

			rigidbody.isKinematic = false;

			Move();
		}

		void StopMoving(){
			rigidbody.velocity = Vector3.zero;
			rigidbody.isKinematic = true;
			originalPosition = transform.position;

			switch (currentRelativePos) {
				case CurrentRelativePosition.GoingUp:
					currentRelativePos = CurrentRelativePosition.Up;
					break;
				case CurrentRelativePosition.GoingDown:
					currentRelativePos = CurrentRelativePosition.Down;
					break;
				case CurrentRelativePosition.GoingLeft:
					currentRelativePos = CurrentRelativePosition.Left;
					break;
				case CurrentRelativePosition.GoingRight:
					currentRelativePos = CurrentRelativePosition.Right;
					break;
			}

			StartCountingTime();
		}

		void Move(){
			switch (currentRelativePos) {
			case CurrentRelativePosition.GoingUp:
				MoveUp();
				break;
			case CurrentRelativePosition.GoingDown:
				MoveDown();
				break;
			case CurrentRelativePosition.GoingLeft:
				MoveLeft();
				break;
			case CurrentRelativePosition.GoingRight:
				MoveRight();
				break;
			}

			StopIfReachedDestination();
		}

		void SetHorizontalPositionDestination(Vector3 directionVector){
			positionDestination = originalPosition + Vector3.ClampMagnitude(directionVector, transform.localScale.x * distanceInBlocks);
		}

		void SetVerticalPositionDestination(Vector3 directionVector){
			positionDestination = originalPosition + Vector3.ClampMagnitude(directionVector, transform.localScale.z * distanceInBlocks);
		}

		bool ReachedVerticalPositionDestination(){
			return Mathf.Sqrt((transform.position - originalPosition).sqrMagnitude) > transform.localScale.z * distanceInBlocks;
		}

		bool ReachedHorizontalPositionDestination(){
			return Mathf.Sqrt((transform.position - originalPosition).sqrMagnitude) > transform.localScale.x * distanceInBlocks;
		}

		void MoveUp(){
			rigidbody.AddForce(transform.up * speed * Time.deltaTime);
			SetVerticalPositionDestination(transform.up);
		}

		void MoveDown(){
			rigidbody.AddForce(-transform.up * speed * Time.deltaTime);
			SetVerticalPositionDestination(-transform.up);
		}

		void MoveLeft(){
			rigidbody.AddForce(-transform.right * speed * Time.deltaTime);
			SetHorizontalPositionDestination(-transform.right);
		}

		void MoveRight(){
			rigidbody.AddForce(transform.right * speed * Time.deltaTime);
			SetHorizontalPositionDestination(transform.right);
		}

		void StopIfReachedDestination(){
			switch (currentRelativePos) {
				case CurrentRelativePosition.GoingUp:
				case CurrentRelativePosition.GoingDown:
					if(ReachedVerticalPositionDestination()){
						//transform.position = positionDestination;
						
						StopMoving();
					}
					break;
				case CurrentRelativePosition.GoingLeft:
				case CurrentRelativePosition.GoingRight:
					if(ReachedHorizontalPositionDestination()){
						//transform.position = positionDestination;
						
						StopMoving();
					}
					break;
			}
		}
	}
}