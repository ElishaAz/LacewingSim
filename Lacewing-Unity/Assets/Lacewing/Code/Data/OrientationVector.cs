using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace Lacewing.Data
{
	public readonly struct OrientationVector
	{
		[DataMember] public readonly float roll, pitch, yaw;

		[JsonConstructor]
		public OrientationVector(float roll, float pitch, float yaw)
		{
			this.roll = roll;
			this.pitch = pitch;
			this.yaw = yaw;
		}

		public OrientationVector(Vector3 eulerAngles)
		{
			this.roll = -eulerAngles.z;
			this.pitch = eulerAngles.x;
			this.yaw = eulerAngles.y;
		}

		public Vector3 AsVector3() => new Vector3(pitch, yaw, -roll);
	}
}