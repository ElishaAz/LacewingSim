using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lacewing
{
	[RequireComponent(typeof(Rigidbody))]
	public class DroneMovement : MonoBehaviour
	{
		[FormerlySerializedAs("movementSpeed")] [SerializeField]
		private Vector3 velocity;

		[SerializeField] private Quaternion orientation;

		private Rigidbody rb;

		private void Start()
		{
			rb = GetComponent<Rigidbody>();
			orientation = transform.rotation;
		}

		private void FixedUpdate()
		{
			rb.velocity = velocity;
			if (orientation != transform.rotation)
				rb.MoveRotation(orientation);
		}

		// ReSharper disable twice ParameterHidesMember
		public void CommandReceived(Vector3 velocity, Vector3 orientation)
		{
			this.velocity = velocity;
			this.orientation = Quaternion.Euler(orientation);
		}
	}
}