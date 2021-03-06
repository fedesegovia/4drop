﻿using UnityEngine;
using System.Collections;
using InControl;

namespace Drop
{ 
    public class GameController : MonoBehaviour {
        public GameObject PlayerPrefab;

	    void Start () 
		{
            SetNumberOfPlayersFromConnectedControllers ();
            CreatePlayerAvatars();
	    }

        void SetNumberOfPlayersFromConnectedControllers()
        {
            Game.Instance.PlayerCount = InputManager.Devices.Count;
        }

        void CreatePlayerAvatars()
        {
			Game.Instance.Players = new GameObject[Game.Instance.PlayerCount];

            for (int i = 0; i < Game.Instance.PlayerCount; i++ )
            {
                GameObject player = Instantiate(PlayerPrefab) as GameObject;
				player.transform.position = new Vector3(0, i * 10, 0);
                player.GetComponent<PlayerController>().PlayerId = i;
				Game.Instance.Players[i] = player;
            }
        }

    }
}