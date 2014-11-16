using UnityEngine;
using System.Collections;

namespace Drop
{ 
    public class BulletController : MonoBehaviour {
		public Weapon parentWeapon = null;
		public Vector3 direction;
        public float speed = 60f;
		public float damage = 3.0f;

	    void FixedUpdate () {
			transform.position += direction * speed * Time.deltaTime;
	    }

		void OnEnable(){
			transform.up = direction;
		}

		void OnBecameInvisible(){
			RecycleBullet();
		}

		void RecycleBullet(){
			if(parentWeapon){
				parentWeapon.LoadBullet(this);
			}
		}
    }
}