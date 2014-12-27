using UnityEngine;
using System.Collections;

namespace Drop{
	public class VulnerableDoor : MonoBehaviour {

		public float secondsToDestroy = 1;
		BoxCollider2D collider;
		SpriteRenderer sprite;

		// Use this for initialization
		void Start () {
			collider = GetComponent<BoxCollider2D> ();
			sprite = GetComponent<SpriteRenderer> ();
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnCollisionStay2D(){
			Debug.Log (secondsToDestroy);
			secondsToDestroy -= Time.deltaTime;
			Color materialColor = sprite.material.color;
			materialColor.a -= Time.deltaTime;
			sprite.material.color = materialColor;
			if (secondsToDestroy < 0) {
				collider.enabled = false;
			}
		}
	}
}
