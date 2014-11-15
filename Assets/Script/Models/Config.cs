using UnityEngine;
using System.Collections;

namespace Drop
{
	public class Config {
		//-------------------------------//
		// Singleton                     //
		//-------------------------------//
		static Config instance;
		
		public static Config Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Config();
				}
				
				return instance;
			}
		}

		public float InputControllerSensitivity = 0.3f;

	}
}