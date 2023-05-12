using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace Lacewing.Data
{
	public readonly struct PositionVector
	{
		[DataMember] public readonly float right;
		[DataMember] public readonly float up;
		[DataMember] public readonly float forward;

		[JsonConstructor]
		public PositionVector(float right, float up, float forward)
		{
			this.right = right;
			this.up = up;
			this.forward = forward;
		}

		public PositionVector(Vector3 position)
		{
			this.right = position.x;
			this.up = position.y;
			this.forward = position.z;
		}

		public Vector3 AsVector3() => new Vector3(this.right, this.up, this.forward);
	}
}