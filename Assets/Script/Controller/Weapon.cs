using UnityEngine;
using System.Collections;

namespace Drop
{
	public class Weapon : MonoBehaviour {
		public GameObject bulletPrefab;
		public int maxBullets = 100;
		public float minimumDelay = 0.3f;
		GameObject[] bullets;
		int remainingBullets = 0;
		float lastShotTime;
		BulletController currentBulletController = null;

		void Start () {
			InstantiateBullets();
			lastShotTime = Time.timeSinceLevelLoad;
		}

		public void Shoot (Vector3 direction) {
			if(Time.timeSinceLevelLoad - lastShotTime > minimumDelay){
				if(remainingBullets > 0){
					remainingBullets--;

					currentBulletController = bullets[remainingBullets].GetComponent<BulletController>();

					currentBulletController.direction = direction;
					currentBulletController.parentWeapon = this;
					currentBulletController.transform.position = transform.position;
					currentBulletController.gameObject.SetActive(true);

					lastShotTime = Time.timeSinceLevelLoad;
				}
			}
		}

		void InstantiateBullets(){
			bullets = new GameObject[maxBullets];
			remainingBullets = maxBullets;

			for(int i = 0; i < maxBullets; i++){
				bullets[i] = GameObject.Instantiate((Object)bulletPrefab) as GameObject;
				bullets[i].SetActive(false);
			}
		}

		public void LoadBullet(BulletController bullet){
			if(remainingBullets < maxBullets){
				bullets[remainingBullets] = bullet.gameObject;
				bullets[remainingBullets].SetActive(false);

				remainingBullets++;
			}
		}
	}
}