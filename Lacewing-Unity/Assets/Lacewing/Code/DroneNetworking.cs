using System;
using System.Collections.Generic;
using System.Linq;
using Lacewing.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Lacewing
{
	[RequireComponent(typeof(DroneMovement))]
	[RequireComponent(typeof(DroneLidars))]
	public class DroneNetworking : UdpNetworking
	{
		private DroneMovement droneMovement;
		private DroneLidars droneLidars;

		protected override void Start()
		{
			base.Start();
			droneMovement = GetComponent<DroneMovement>();
			droneLidars = GetComponent<DroneLidars>();
			onMessageReceived.AddListener(OnMessageReceived);
		}

		private void OnMessageReceived(string message)
		{
			if (string.IsNullOrWhiteSpace(message))
				return;
			try
			{
				var command = JsonConvert.DeserializeObject<DroneCommand>(message);
				var velocity = command.velocity.AsVector3();
				var orientation = command.orientation.AsVector3();
				droneMovement.CommandReceived(velocity, orientation);
			}
			catch (JsonSerializationException e)
			{
				Debug.LogError(e);
			}
		}

		protected void FixedUpdate()
		{
			var eulerAngles = transform.eulerAngles;
			var orientation = new OrientationVector(eulerAngles);
			var position = new PositionVector(transform.position);

			var lidars = droneLidars.FireLidars();

			var message = JsonConvert.SerializeObject(new DroneTelemetry(position, orientation, lidars));
			SendUdpMessage(message);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}
	}
}