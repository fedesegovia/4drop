using UnityEngine;
using System.Collections;

namespace Drop
{
	public class CameraController : MonoBehaviour {
		float minOrtographicSize = 7.0f;
	
		// Update is called once per frame
		void Update () {
			if( Game.Instance.PlayerCount == 1)
			{
				GameObject player = Game.Instance.Players[0];
				Vector3 middlePoint = player.transform.position;
				camera.transform.position = new Vector3(middlePoint.x, middlePoint.y, camera.transform.position.z);
			} 
			else 
			{
				
				// Calculate the maximum distance to set the camera size
				float maxDistance = 0f;
				for(int i = 1; i <= Game.Instance.PlayerCount; i++){
					for(int j = i + 1; j <= Game.Instance.PlayerCount; j++){
						if(i-1 < Game.Instance.PlayerCount && j-1 < Game.Instance.PlayerCount){
							GameObject p1 = (GameObject)Game.Instance.Players[i-1];
							GameObject p2 = (GameObject)Game.Instance.Players[j-1];
							
							float distance = Vector3.Distance(p1.transform.position, p2.transform.position);
							if(distance > maxDistance){ maxDistance = distance; }
						}
					}
				}
				
	//			if( maxDistance < minOrtographicSize ) {
	//				maxDistance = minOrtographicSize;
	//			}
				
				camera.orthographicSize = Mathf.Clamp(maxDistance, 40, 150) ;
				
				// Move camera to the middle point between all players
				Vector3 middlePoint = Vector3.zero;
				for(int i = 0; i < Game.Instance.PlayerCount; i++){
					if(Game.Instance.PlayerCount > i){
						middlePoint += ((GameObject)Game.Instance.Players[i]).transform.position;	
					}
				}
				middlePoint = middlePoint / Game.Instance.PlayerCount;
				camera.transform.position = new Vector3(middlePoint.x, middlePoint.y, camera.transform.position.z);
			}
		}
	}
}