using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lacewing
{
	public class DroneLidars : MonoBehaviour
	{
		private DroneLidar[] lidars;

		private void Start()
		{
			lidars = GetComponentsInChildren<DroneLidar>();
		}

		public Dictionary<string, float> FireLidars()
		{
			var results = new Dictionary<string, float>();
			foreach (var lidar in lidars)
			{
				float distance = -1;
				if (Physics.Raycast(lidar.transform.position, lidar.transform.forward, out var hit, lidar.maxDistance))
				{
					distance = hit.distance;
				}

				results.Add(lidar.lidarName, distance);
			}

			return results;
		}
	}
}