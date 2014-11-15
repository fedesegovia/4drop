//
// Models/Game.cs
//
// Author:
//       Francisco Tufró <francisco@nastycloud.com>
//
// Copyright (c) 2014 Nastycloud S.R.L.

using UnityEngine;
using System.Collections;

namespace Drop
{ 
    public class Game 
    {
        //-------------------------------//
        // Singleton                     //
        //-------------------------------//
        static Game instance;
        
        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game();
                }

                return instance;
            }
        }

        //-------------------------------//
        // Public members                //
        //-------------------------------//
        public int PlayerCount;
		public GameObject[] Players;

    }
}